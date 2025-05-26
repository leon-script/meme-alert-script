using TwitchLeonScript.UI.WinForms.Tokens.Infrastructure;
using TwitchLeonScript.UI.WinForms.Tokens.StorageModels;

namespace TwitchLeonScript.UI.WinForms.Tokens.Storages
{
    public sealed class MemeTokenStorage : BaseTokenStorage<StoredMemeToken>, IMemeTokenStorage
    {
        public MemeTokenStorage()
        {
            FilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "TwitchLeonScript", "memealerts.dat");
        }
    }
}
