using TwitchLeonScript.UI.WinForms.Tokens.Infrastructure;
using TwitchLeonScript.UI.WinForms.Tokens.StorageModels;

namespace TwitchLeonScript.UI.WinForms.Tokens.Storages
{
    public sealed class TwitchTokenStorage : BaseTokenStorage<StoredTwitchToken>, ITwitchTokenStorage
    {
        public TwitchTokenStorage()
        {
            FilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "TwitchLeonScript", "twitch.dat");
        }
    }
}
