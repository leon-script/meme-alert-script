using MemeAlertsScript.Core.Extensions;
using Microsoft.Extensions.Logging;
using TwitchLib.Api;
using TwitchLib.Api.Core.Enums;
using TwitchLib.EventSub.Core.SubscriptionTypes.Channel;
using TwitchLib.EventSub.Websockets;
using TwitchLib.EventSub.Websockets.Core.EventArgs;
using TwitchLib.EventSub.Websockets.Core.EventArgs.Channel;

namespace MemeAlertsScript.Twitch
{
    public class TwitchEventSubListener
    {
        private readonly EventSubWebsocketClient _eventSubWebsocketClient;
        private readonly TwitchAPI _twitchApi;
        private readonly string _broadcasterId;
        private readonly string _oauthToken;
        private readonly ILogger _logger;

        public event Action<ChannelPointsCustomRewardRedemption> OnRewardRedeemed;

        public TwitchEventSubListener(string appId, string appToken, string broadcasterId, string oauthToken, ILogger logger)
        {
            _eventSubWebsocketClient = new EventSubWebsocketClient();
            _eventSubWebsocketClient.WebsocketConnected += OnWebsocketConnected;
            _eventSubWebsocketClient.WebsocketDisconnected += OnWebsocketDisconnected;
            _eventSubWebsocketClient.WebsocketReconnected += OnWebsocketReconnected;
            _eventSubWebsocketClient.ErrorOccurred += OnErrorOccurred;
            _eventSubWebsocketClient.ChannelPointsCustomRewardRedemptionAdd += OnRedemption;

            _twitchApi = new TwitchAPI();
            _twitchApi.Settings.ClientId = appId;
            _twitchApi.Settings.AccessToken = appToken;

            _broadcasterId = broadcasterId;
            _oauthToken = oauthToken;
            _logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await _eventSubWebsocketClient.ConnectAsync();
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await _eventSubWebsocketClient.DisconnectAsync();
        }

        private async Task OnWebsocketConnected(object sender, WebsocketConnectedArgs e)
        {
            _logger.LogInformation($"Websocket {_eventSubWebsocketClient.SessionId.ToSecretPreview()} connected!");

            if (!e.IsRequestedReconnect)
            {
                var condition = new Dictionary<string, string> { { "broadcaster_user_id", _broadcasterId }, { "moderator_user_id", _broadcasterId } };
                await _twitchApi.Helix.EventSub.CreateEventSubSubscriptionAsync("channel.channel_points_custom_reward_redemption.add", "1", condition, EventSubTransportMethod.Websocket,
                _eventSubWebsocketClient.SessionId, accessToken: _oauthToken); // https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/
            }
        }

        private async Task OnWebsocketDisconnected(object sender, EventArgs e)
        {
            _logger.LogError($"Websocket {_eventSubWebsocketClient.SessionId.ToSecretPreview()} disconnected!");

            // Don't do this in production. You should implement a better reconnect strategy with exponential backoff
            while (!await _eventSubWebsocketClient.ReconnectAsync())
            {
                _logger.LogError("Websocket reconnect failed!");
                await Task.Delay(1000);
            }
        }

        private Task OnWebsocketReconnected(object sender, EventArgs e)
        {
            _logger.LogWarning($"Websocket {_eventSubWebsocketClient.SessionId.ToSecretPreview()} reconnected");
            return Task.CompletedTask;
        }

        private Task OnErrorOccurred(object sender, ErrorOccuredArgs e)
        {
            _logger.LogError($"Websocket {_eventSubWebsocketClient.SessionId.ToSecretPreview()} - Error occurred!");
            return Task.CompletedTask;
        }

        private Task OnRedemption(object sender, ChannelPointsCustomRewardRedemptionArgs e)
        {
            OnRewardRedeemed?.Invoke(e.Notification.Payload.Event);
            return Task.CompletedTask;
        }
    }
}
