using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Users;
using GreenSphere.Application.Interfaces.Identity;
using MediatR;

namespace GreenSphere.Application.Features.Users.Queries.GetUserProfile;
public sealed class GetUserProfileQueryHandler(
    IUserService userService,
    ICurrentUser currentUser)
    : IRequestHandler<GetUserProfileQuery, Result<UserProfileDto>>
{
    public async Task<Result<UserProfileDto>> Handle(
        GetUserProfileQuery request,
        CancellationToken cancellationToken)
    {
        return await userService.GetUserProfileAsync(currentUser.Id);
    }
}
