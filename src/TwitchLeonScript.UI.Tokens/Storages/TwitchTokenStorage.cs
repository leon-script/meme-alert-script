using TwitchLeonScript.UI.Tokens.Infrastructure;
using TwitchLeonScript.UI.Tokens.StoredTokens;

namespace TwitchLeonScript.UI.Tokens.Storages
{
    public sealed class TwitchTokenStorage : BaseTokenStorage<StoredTwitchToken>, ITwitchTokenStorage
    {
        public TwitchTokenStorage()
        {
            this.FilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "TwitchLeonScript", "twitch.dat");
        }
    }
}
