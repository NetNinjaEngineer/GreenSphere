using GreenSphere.Application.Abstractions;
using GreenSphere.Application.Features.Auth.Requests.Queries;
using GreenSphere.Application.Interfaces.Identity;
using MediatR;

namespace GreenSphere.Application.Features.Auth.Handlers.Queries;
public class LoginWithGoogleQueryHandler(IAuthService authService) : BaseResponseHandler, IRequestHandler<LoginWithGoogleQuery, Result<string>>
{
    public async Task<Result<string>> Handle(LoginWithGoogleQuery request, CancellationToken cancellationToken)
    {
        return await authService.LoginWithGoogleAsync();
    }
}
