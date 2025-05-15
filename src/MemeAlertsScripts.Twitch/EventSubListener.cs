using TwitchLib.Api;
using TwitchLib.Api.Core.Enums;
using TwitchLib.EventSub.Websockets;
using TwitchLib.EventSub.Websockets.Core.EventArgs;
using TwitchLib.EventSub.Websockets.Core.EventArgs.Channel;

namespace MemeAlertsScripts.Twitch
{
    public class EventSubListener
    {
        private readonly EventSubWebsocketClient _eventSubWebsocketClient;
        private readonly TwitchAPI _twitchApi;
        private readonly string _userId;
        private readonly string _userToken;

        public event Action<string, string> OnRewardRedeemed;

        public EventSubListener(string clientId, string accessToken, string userId, string userToken)
        {
            _eventSubWebsocketClient = new EventSubWebsocketClient();
            _eventSubWebsocketClient.WebsocketConnected += OnWebsocketConnected;
            _eventSubWebsocketClient.WebsocketDisconnected += OnWebsocketDisconnected;
            _eventSubWebsocketClient.WebsocketReconnected += OnWebsocketReconnected;
            _eventSubWebsocketClient.ErrorOccurred += OnErrorOccurred;
            _eventSubWebsocketClient.ChannelPointsCustomRewardRedemptionAdd += OnRedemption;

            _twitchApi = new TwitchAPI();
            // Get ClientId and ClientSecret by register an Application here: https://dev.twitch.tv/console/apps
            // https://dev.twitch.tv/docs/authentication/register-app/
            _twitchApi.Settings.ClientId = clientId;
            // Get Application Token with Client credentials grant flow.
            // https://dev.twitch.tv/docs/authentication/getting-tokens-oauth/#client-credentials-grant-flow
            _twitchApi.Settings.AccessToken = accessToken;
            
            _userId = userId;
            _userToken = userToken;
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
            // _logger.LogInformation($"Websocket {_eventSubWebsocketClient.SessionId} connected!");

            if (!e.IsRequestedReconnect)
            {
                // subscribe to topics
                // create condition Dictionary
                // You need BOTH broadcaster and moderator values or EventSub returns an Error!
                var condition = new Dictionary<string, string> { { "broadcaster_user_id", _userId }, { "moderator_user_id", _userId } };
                // Create and send EventSubscription
                await _twitchApi.Helix.EventSub.CreateEventSubSubscriptionAsync("channel.channel_points_custom_reward_redemption.add", "1", condition, EventSubTransportMethod.Websocket,
                _eventSubWebsocketClient.SessionId, accessToken: _userToken); // "BROADCASTER_ACCESS_TOKEN_WITH_SCOPES");
                // If you want to get Events for special Events you need to additionally add the AccessToken of the ChannelOwner to the request.
                // https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/
            }
        }

        private async Task OnWebsocketDisconnected(object sender, EventArgs e)
        {
            // _logger.LogError($"Websocket {_eventSubWebsocketClient.SessionId} disconnected!");

            // Don't do this in production. You should implement a better reconnect strategy with exponential backoff
            while (!await _eventSubWebsocketClient.ReconnectAsync())
            {
                //_logger.LogError("Websocket reconnect failed!");
                await Task.Delay(1000);
            }
        }

        private Task OnWebsocketReconnected(object sender, EventArgs e)
        {
            return Task.CompletedTask;
            // _logger.LogWarning($"Websocket {_eventSubWebsocketClient.SessionId} reconnected");
        }

        private Task OnErrorOccurred(object sender, ErrorOccuredArgs e)
        {
            return Task.CompletedTask;
            // _logger.LogError($"Websocket {_eventSubWebsocketClient.SessionId} - Error occurred!");
        }

        private Task OnRedemption(object sender, ChannelPointsCustomRewardRedemptionArgs e)
        {
            string username = e.Notification.Payload.Event.UserName;
            string rewardTitle = e.Notification.Payload.Event.Reward.Title;

            OnRewardRedeemed?.Invoke(username, rewardTitle);
            return Task.CompletedTask;
        }
    }
}
