using TwitchLeonScript.Session.Models;

namespace TwitchLeonScript.Session.Storages
{
    public sealed class TwitchSessionStorage : BaseSessionStorage<TwitchSession>
    {
        public TwitchSessionStorage()
        {
            FilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "TwitchLeonScript", "twitch.dat");
        }
    }
}
