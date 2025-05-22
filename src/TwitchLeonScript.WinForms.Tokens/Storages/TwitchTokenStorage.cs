using TwitchLeonScript.WinForms.Tokens.Infrastructure;
using TwitchLeonScript.WinForms.Tokens.StoredTokens;

namespace TwitchLeonScript.WinForms.Tokens.Storages
{
    public sealed class TwitchTokenStorage : BaseTokenStorage<StoredTwitchToken>, ITwitchTokenStorage
    {
        public TwitchTokenStorage()
        {
            this.filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "TwitchLeonScript", "twitch.dat");
        }
    }
}
