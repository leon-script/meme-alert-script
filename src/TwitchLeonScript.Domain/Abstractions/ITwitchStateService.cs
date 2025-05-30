namespace TwitchLeonScript.Domain.Abstractions
{
    public interface ITwitchStateService
    {
        string GetOAuthState();
        Uri CreateOAuthUrl(string state);
        string GetRedirectUrl();
    }
}
