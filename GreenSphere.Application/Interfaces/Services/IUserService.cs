using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Users;
using GreenSphere.Application.Features.Users.Commands.ChangeUserEmail;
using GreenSphere.Application.Features.Users.Commands.ChangeUserPassword;
using GreenSphere.Application.Features.Users.Commands.EditUserProfile;
using GreenSphere.Application.Features.Users.Commands.VerifyChangeUserEmail;

namespace GreenSphere.Application.Interfaces.Services;
public interface IUserService
{
    Task<Result<UserProfileDto>> GetUserProfileAsync();
    Task<Result<UserProfileDto>> EditUserProfileAsync(EditUserProfileCommand command);
    Task<Result<bool>> ChangeUserEmailAsync(ChangeUserEmailCommand command);
    Task<Result<bool>> VerifyChangeUserEmailAsync(VerifyChangeUserEmailCommand command);
    Task<Result<bool>> ChangeUserPasswordAsync(ChangeUserPasswordCommand command);
}
