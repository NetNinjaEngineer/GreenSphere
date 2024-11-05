using GreenSphere.Application.Bases;
using GreenSphere.Application.Features.Users.DTOs;
using GreenSphere.Application.Features.Users.Requests.Queries;
using GreenSphere.Application.Interfaces.Identity;
using MediatR;

namespace GreenSphere.Application.Features.Users.Handlers.Queries;
public sealed class GetUserProfileQueryHandler(
    IUserPrivacyService privacyService)
    : IRequestHandler<GetUserProfileQuery, Result<UserProfileDto>>
{
    public async Task<Result<UserProfileDto>> Handle(
        GetUserProfileQuery request,
        CancellationToken cancellationToken)
    {
        return await privacyService.GetUserProfileAsync(request.UserId);
    }
}
