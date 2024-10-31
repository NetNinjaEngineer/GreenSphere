using GreenSphere.Application.Abstractions;

namespace GreenSphere.Application.Interfaces.Infrastructure;
public interface IFacebookClientApi
{
    Task<Result<bool>> AuthenticateAsync(string accessToken);
}
