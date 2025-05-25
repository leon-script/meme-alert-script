using MediatR;
using TwitchLeonScript.Core.Common.Options;
using TwitchLeonScript.Core.Twitch.Notifications;
using Microsoft.Extensions.Options;
using TwitchLib.Api;
using TwitchLib.Api.Core.Enums;
using TwitchLib.EventSub.Websockets;
using TwitchLib.EventSub.Websockets.Core.EventArgs;
using TwitchLib.EventSub.Websockets.Core.EventArgs.Channel;

namespace TwitchLeonScript.Core.Twitch.Listeners
{
    public class TwitchWebsocketListener : IDisposable
    {
        private readonly IMediator _mediator;
        private readonly TwitchAPI _twitchApi;
        private readonly EventSubWebsocketClient _eventSubWebsocketClient;

        private string? AppToken { get; set; }
        private string? BroadcasterId { get; set; }
        private string? OauthToken { get; set; }

        public TwitchWebsocketListener(IOptions<TwitchOptions> options, IMediator mediator)
        {
            _mediator = mediator;
            _twitchApi = new TwitchAPI();
            _twitchApi.Settings.ClientId = options.Value.AppId;

            _eventSubWebsocketClient = new EventSubWebsocketClient();
            _eventSubWebsocketClient.WebsocketConnected += OnWebsocketConnected;
            _eventSubWebsocketClient.WebsocketDisconnected += OnWebsocketDisconnected;
            _eventSubWebsocketClient.WebsocketReconnected += OnWebsocketReconnected;
            _eventSubWebsocketClient.ErrorOccurred += OnErrorOccurred;
            _eventSubWebsocketClient.ChannelPointsCustomRewardRedemptionAdd += OnRedemption;
        }

        public async Task<bool> StartAsync(string appToken, string oauthToken, string broadcasterId)
        {
            AppToken = appToken;
            BroadcasterId = broadcasterId;
            OauthToken = oauthToken;

            _twitchApi.Settings.AccessToken = AppToken;
            await _eventSubWebsocketClient.ConnectAsync();

            return true;
        }

        public async Task<bool> StopAsync()
        {
            await _eventSubWebsocketClient.DisconnectAsync();

            return true;
        }

        private async Task OnWebsocketConnected(object sender, WebsocketConnectedArgs e)
        {
            if (!e.IsRequestedReconnect)
            {
                var condition = new Dictionary<string, string> 
                { 
                    { "broadcaster_user_id", BroadcasterId! }, 
                    { "moderator_user_id", BroadcasterId! } 
                };

                // https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/
                await _twitchApi.Helix.EventSub.CreateEventSubSubscriptionAsync(
                    "channel.channel_points_custom_reward_redemption.add", "1",
                    condition,
                    EventSubTransportMethod.Websocket,
                    _eventSubWebsocketClient.SessionId,
                    accessToken: OauthToken!);
            }
        }

        private async Task OnWebsocketDisconnected(object sender, EventArgs e)
        {
            // TODO Don't do this in production. You should implement a better reconnect strategy with exponential backoff
            while (!await _eventSubWebsocketClient.ReconnectAsync())
            {
                await Task.Delay(1000);
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
            var notification = new TwitchRedemptionReceived(e.Notification.Payload.Event);
            await _mediator.Publish(notification);
        }

        public void Dispose()
        {
            _eventSubWebsocketClient.WebsocketConnected -= OnWebsocketConnected;
            _eventSubWebsocketClient.WebsocketDisconnected -= OnWebsocketDisconnected;
            _eventSubWebsocketClient.WebsocketReconnected -= OnWebsocketReconnected;
            _eventSubWebsocketClient.ErrorOccurred -= OnErrorOccurred;
            _eventSubWebsocketClient.ChannelPointsCustomRewardRedemptionAdd -= OnRedemption;
            _eventSubWebsocketClient.DisconnectAsync();
        }
    }
}
