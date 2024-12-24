using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Users;
using GreenSphere.Application.Features.Users.Commands.AssignUserPrivacy;

namespace GreenSphere.Application.Interfaces.Identity;
public interface IUserPrivacyService
{
    Task<Result<string>> AssignPrivacyToUserAsync(AssignUserPrivacyCommand command);
    Task<Result<PrivacySettingListDto>> GetUserPrivacySettingsAsync(string userId);
    Task<Result<UserProfileDto>> GetUserProfileAsync(string userId);
}
