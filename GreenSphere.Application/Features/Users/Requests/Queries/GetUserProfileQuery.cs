using GreenSphere.Application.Bases;
using GreenSphere.Application.Features.Users.DTOs;
using MediatR;

namespace GreenSphere.Application.Features.Users.Requests.Queries;
public sealed class GetUserProfileQuery : IRequest<Result<UserProfileDto>>
{
    public string UserId { get; set; } = null!;
}
