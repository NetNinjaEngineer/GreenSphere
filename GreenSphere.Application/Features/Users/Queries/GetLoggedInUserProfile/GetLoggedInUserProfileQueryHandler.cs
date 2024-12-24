using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Users;
using GreenSphere.Application.Interfaces.Identity;
using MediatR;

namespace GreenSphere.Application.Features.Users.Queries.GetLoggedInUserProfile;

public class GetLoggedInUserProfileQueryHandler(
    ICurrentUser currentUser,
    IUserPrivacyService userPrivacy
    ) : IRequestHandler<GetLoggedInUserProfileQuery, Result<UserProfileDto>>
{
    public async Task<Result<UserProfileDto>> Handle(GetLoggedInUserProfileQuery request, CancellationToken cancellationToken)
        => await userPrivacy.GetUserProfileAsync(currentUser.Id);
}