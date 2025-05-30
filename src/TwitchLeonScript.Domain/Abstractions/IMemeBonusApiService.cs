using System.Net;

namespace TwitchLeonScript.Domain.Abstractions
{
    public interface IMemeBonusApiService
    {
        Task<HttpStatusCode> GivePointsAsync(string userId, int value);
    }
}
