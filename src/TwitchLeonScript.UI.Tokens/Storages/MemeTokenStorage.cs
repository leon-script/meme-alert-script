using TwitchLeonScript.UI.Tokens.Infrastructure;
using TwitchLeonScript.UI.Tokens.StoredTokens;

namespace TwitchLeonScript.UI.Tokens.Storages
{
    public sealed class MemeTokenStorage : BaseTokenStorage<StoredMemeToken>, IMemeTokenStorage
    {
        public MemeTokenStorage()
        {
            this.FilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "TwitchLeonScript", "memealerts.dat");
        }
    }
}
