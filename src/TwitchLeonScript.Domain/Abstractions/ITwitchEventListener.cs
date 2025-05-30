namespace TwitchLeonScript.Domain.Abstractions
{
    public interface ITwitchEventListener
    {
        bool IsRunning { get; }
        Task<bool> StartAsync(string appId, string appToken, string oauthToken, string broadcasterId);
        Task<bool> StopAsync();
    }
}
