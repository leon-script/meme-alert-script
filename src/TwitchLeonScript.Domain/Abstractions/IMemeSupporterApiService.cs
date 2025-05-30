using TwitchLeonScript.Domain.Models;

namespace TwitchLeonScript.Domain.Abstractions
{
    public interface IMemeSupporterApiService
    {
        Task<IEnumerable<MemeSupporter>> GetSupportersAsync(string query = "");
    }
}
