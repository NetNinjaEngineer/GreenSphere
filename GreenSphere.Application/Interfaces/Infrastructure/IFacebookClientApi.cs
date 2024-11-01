using GreenSphere.Application.Bases;

namespace GreenSphere.Application.Interfaces.Infrastructure;
public interface IFacebookClientApi
{
    Task<Result<bool>> AuthenticateAsync(string accessToken);
}
