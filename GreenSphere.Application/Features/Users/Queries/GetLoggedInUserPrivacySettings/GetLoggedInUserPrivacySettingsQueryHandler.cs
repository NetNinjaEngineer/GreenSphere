using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Users;
using GreenSphere.Application.Interfaces.Identity;
using MediatR;

namespace GreenSphere.Application.Features.Users.Queries.GetLoggedInUserPrivacySettings;

public class GetLoggedInUserPrivacySettingsQueryHandler(
    ICurrentUser currentUser,
    IUserPrivacyService privacyService
    ) : IRequestHandler<GetLoggedInUserPrivacySettingsQuery, Result<PrivacySettingListDto>>
{
    public async Task<Result<PrivacySettingListDto>> Handle(GetLoggedInUserPrivacySettingsQuery request, CancellationToken cancellationToken)
        => await privacyService.GetUserPrivacySettingsAsync(currentUser.Id);
}