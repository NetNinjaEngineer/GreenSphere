using GreenSphere.Application.Bases;
using GreenSphere.Application.Features.Users.DTOs;
using GreenSphere.Application.Features.Users.Requests.Queries;
using GreenSphere.Application.Interfaces.Identity;
using MediatR;

namespace GreenSphere.Application.Features.Users.Handlers.Queries;

public class GetLoggedInUserProfileQueryHandler(
    ICurrentUser currentUser,
    IUserPrivacyService userPrivacy
    ) : IRequestHandler<GetLoggedInUserProfileQuery, Result<UserProfileDto>>
{
    public async Task<Result<UserProfileDto>> Handle(GetLoggedInUserProfileQuery request, CancellationToken cancellationToken)
        => await userPrivacy.GetUserProfileAsync(currentUser.Id);
}