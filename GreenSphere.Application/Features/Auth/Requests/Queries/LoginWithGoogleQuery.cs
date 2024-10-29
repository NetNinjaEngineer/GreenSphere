using GreenSphere.Application.Abstractions;
using MediatR;

namespace GreenSphere.Application.Features.Auth.Requests.Queries;
public class LoginWithGoogleQuery : IRequest<Result<string>>
{
}
