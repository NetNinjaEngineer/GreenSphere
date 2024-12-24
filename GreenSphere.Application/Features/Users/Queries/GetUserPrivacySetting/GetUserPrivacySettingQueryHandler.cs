using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Users;
using GreenSphere.Application.Interfaces.Identity;
using MediatR;

namespace GreenSphere.Application.Features.Users.Queries.GetUserPrivacySetting;
public sealed class GetUserPrivacySettingQueryHandler(IUserPrivacyService privacyService) : IRequestHandler<GetUserPrivacySettingQuery, Result<PrivacySettingListDto>>
{
    public async Task<Result<PrivacySettingListDto>> Handle(
        GetUserPrivacySettingQuery request,
        CancellationToken cancellationToken)
        => await privacyService.GetUserPrivacySettingsAsync(request.UserId);
}
