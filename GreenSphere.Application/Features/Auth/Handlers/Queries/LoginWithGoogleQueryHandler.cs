using GreenSphere.Application.Abstractions;
using GreenSphere.Application.Features.Auth.Requests.Queries;
using GreenSphere.Application.Interfaces.Identity;
using MediatR;

namespace GreenSphere.Application.Features.Auth.Handlers.Queries;
public class LoginWithGoogleQueryHandler(IAuthService authService) : IRequestHandler<LoginWithGoogleQuery, Result<string>>
{
    public Task<Result<string>> Handle(LoginWithGoogleQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
