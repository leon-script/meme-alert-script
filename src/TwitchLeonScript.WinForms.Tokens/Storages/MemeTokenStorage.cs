using TwitchLeonScript.WinForms.Tokens.Infrastructure;
using TwitchLeonScript.WinForms.Tokens.StoredTokens;

namespace TwitchLeonScript.WinForms.Tokens.Storages
{
    public sealed class MemeTokenStorage : BaseTokenStorage<StoredMemeToken>, IMemeTokenStorage
    {
        public MemeTokenStorage()
        {
            this.filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "TwitchLeonScript", "memealerts.dat");
        }
    }
}
