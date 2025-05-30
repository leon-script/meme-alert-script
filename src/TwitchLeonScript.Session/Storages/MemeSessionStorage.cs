using TwitchLeonScript.Session.Models;

namespace TwitchLeonScript.Session.Storages
{
    public sealed class MemeSessionStorage : BaseSessionStorage<MemeSession>
    {
        public MemeSessionStorage()
        {
            FilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "TwitchLeonScript", "memealerts.dat");
        }
    }
}
