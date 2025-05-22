using MediatR;
using MemeAlertsScript.Core.App.Responses.MemeAuth;

namespace MemeAlertsScript.Core.App.Commands.MemeAuth
{
    public class GetMemeSupportersCommand : IRequest<GetMemeSupportersResponse>
    {
        public required string OAuthToken { get; init; }
    }
}
