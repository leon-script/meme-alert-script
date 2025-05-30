using AutoMapper;
using MediatR;
using TwitchLeonScript.Domain.Abstractions;
using TwitchLeonScript.Domain.Enums;
using TwitchLeonScript.Domain.Events;
using TwitchLeonScript.Domain.Models;
using TwitchLib.Api;
using TwitchLib.Api.Core.Enums;
using TwitchLib.EventSub.Websockets;
using TwitchLib.EventSub.Websockets.Core.EventArgs;
using TwitchLib.EventSub.Websockets.Core.EventArgs.Channel;

namespace TwitchLeonScript.Infrastructure.Listeners
{
    public class TwitchEventListener : ITwitchEventListener, IDisposable
    {
        private readonly EventSubWebsocketClient _websocketClient;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        private TwitchAPI? _twitchApi;
        private string? _oauthToken;
        private string? _broadcasterId;

        public bool IsRunning { get; private set; } = false;

        public TwitchEventListener(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;

            _websocketClient = new EventSubWebsocketClient();
            _websocketClient.WebsocketConnected += OnWebsocketConnected;
            _websocketClient.WebsocketDisconnected += OnWebsocketDisconnected;
            _websocketClient.WebsocketReconnected += OnWebsocketReconnected;
            _websocketClient.ErrorOccurred += OnErrorOccurred;
            _websocketClient.ChannelPointsCustomRewardRedemptionAdd += OnRedemption;
        }

        public async Task<bool> StartAsync(string appId, string appToken, string oauthToken, string broadcasterId)
        {
            if (IsRunning)
            {
                await StopAsync().ConfigureAwait(false);
            }

            _twitchApi = new TwitchAPI { Settings = { ClientId = appId, AccessToken = appToken }};
            _oauthToken = oauthToken;
            _broadcasterId = broadcasterId;

            await _websocketClient.ConnectAsync().ConfigureAwait(false);

            IsRunning = true;
            return true;
        }

        public async Task<bool> StopAsync()
        {
            if (IsRunning && _twitchApi is not null)
            {
                await _websocketClient.DisconnectAsync().ConfigureAwait(false);
            }

            _twitchApi = null;
            _oauthToken = null;
            _broadcasterId = null;

            IsRunning = false;
            return true;
        }

        private async Task OnWebsocketConnected(object sender, WebsocketConnectedArgs e)
        {
            if (_twitchApi is null || _oauthToken is null || _broadcasterId is null)
            {
                throw new InvalidOperationException("Twitch API is not initialized.");
            }

            if (!e.IsRequestedReconnect)
            {
                var condition = new Dictionary<string, string> 
                { 
                    { "broadcaster_user_id", _broadcasterId },
                    { "moderator_user_id", _broadcasterId }
                };

                await _twitchApi.Helix.EventSub.CreateEventSubSubscriptionAsync(
                    "channel.channel_points_custom_reward_redemption.add", "1",
                    condition,
                    EventSubTransportMethod.Websocket,
                    _websocketClient.SessionId,
                    accessToken: _oauthToken).ConfigureAwait(false);
            }
        }

        private async Task OnWebsocketDisconnected(object sender, EventArgs e)
        {
            // TODO Don't do this in production. You should implement a better reconnect strategy with exponential backoff
            while (!await _websocketClient.ReconnectAsync().ConfigureAwait(false))
            {
                await Task.Delay(1000).ConfigureAwait(false);
            }
        }

        private Task OnWebsocketReconnected(object sender, EventArgs e)
        {
            return Task.CompletedTask;
        }

        private Task OnErrorOccurred(object sender, ErrorOccuredArgs e)
        {
            return Task.CompletedTask;
        }

        private async Task OnRedemption(object sender, ChannelPointsCustomRewardRedemptionArgs e)
        {
            var twitchEvent = e.Notification.Payload.Event;

            var domainEvent = new RedemptionReceived
            {
                Redemption = new TwitchRedemption
                {
                    Id = twitchEvent.Id,
                    Input = twitchEvent.UserInput,
                    Status = _mapper.Map<RedemptionStatus>(twitchEvent.Status),
                    RedeemedAt = twitchEvent.RedeemedAt,
                    Reward = new TwitchReward
                    {
                        Id = twitchEvent.Reward.Id,
                        Title = twitchEvent.Reward.Title,
                        Cost = twitchEvent.Reward.Cost,
                        Prompt = twitchEvent.Reward.Prompt,
                    },
                    Broadcaster = new TwitchBroadcaster
                    {
                        Id = twitchEvent.BroadcasterUserId,
                        Login = twitchEvent.BroadcasterUserLogin,
                    },
                    User = new TwitchUser
                    {
                        Id = twitchEvent.UserId,
                        Login = twitchEvent.UserLogin,
                    }
                }
            };

            await _mediator.Publish(domainEvent).ConfigureAwait(false);
        }

        public void Dispose()
        {
            _websocketClient.WebsocketConnected -= OnWebsocketConnected;
            _websocketClient.WebsocketDisconnected -= OnWebsocketDisconnected;
            _websocketClient.WebsocketReconnected -= OnWebsocketReconnected;
            _websocketClient.ErrorOccurred -= OnErrorOccurred;
            _websocketClient.ChannelPointsCustomRewardRedemptionAdd -= OnRedemption;

            _ = _websocketClient.DisconnectAsync();
            GC.SuppressFinalize(this);
        }
    }
}
