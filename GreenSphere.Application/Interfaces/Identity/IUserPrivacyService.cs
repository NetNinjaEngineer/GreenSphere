using GreenSphere.Application.Bases;
using GreenSphere.Application.Features.Users.DTOs;
using GreenSphere.Application.Features.Users.Requests.Commands;

namespace GreenSphere.Application.Interfaces.Identity;
public interface IUserPrivacyService
{
    Task<Result<string>> AssignPrivacyToUserAsync(AssignUserPrivacyCommand command);
    Task<Result<PrivacySettingListDto>> GetUserPrivacySettingsAsync(string userId);
}
