using AutoMapper;
using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Users;
using GreenSphere.Application.Features.Users.Commands.AssignUserPrivacy;
using GreenSphere.Application.Helpers;
using GreenSphere.Application.Interfaces.Identity;
using GreenSphere.Domain.Entities;
using GreenSphere.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace GreenSphere.Services.Services;
public sealed class UserPrivacyService(
    UserManager<ApplicationUser> userManager,
    ApplicationDbContext context,
    IMapper mapper) : IUserPrivacyService
{
    public async Task<Result<string>> AssignPrivacyToUserAsync(AssignUserPrivacyCommand command)
    {
        var user = await userManager.FindByIdAsync(command.UserId);

        if (user is null)
            return Result<string>.Failure(HttpStatusCode.NotFound, DomainErrors.User.UnkownUser);

        if (await context.PrivacySettings.AnyAsync(s => s.UserId == command.UserId))
            return Result<string>.Failure(HttpStatusCode.Conflict, DomainErrors.User.UserHasPrivacy);

        var privacySetting = new PrivacySetting
        {
            Id = Guid.NewGuid().ToString(),
            UserId = command.UserId,
            SendMessages = command.AssignPrivacySettingsRequest.MessagePermission,
            ViewActivityStatus = command.AssignPrivacySettingsRequest.ActivityStatusVisibility,
            TagInPosts = command.AssignPrivacySettingsRequest.TaggingPermission,
            ViewPosts = command.AssignPrivacySettingsRequest.PostVisibility,
            ViewProfile = command.AssignPrivacySettingsRequest.ProfileVisibility
        };

        context.PrivacySettings.Add(privacySetting);

        await context.SaveChangesAsync();

        return Result<string>.Success(privacySetting.Id);
    }

    public async Task<Result<PrivacySettingListDto>> GetUserPrivacySettingsAsync(string userId)
    {
        var user = await userManager.FindByIdAsync(userId);

        if (user == null)
            return Result<PrivacySettingListDto>.Failure(HttpStatusCode.NotFound, DomainErrors.User.UnkownUser);

        var setting = await context.PrivacySettings.FirstOrDefaultAsync(x => x.UserId == userId);

        if (setting == null)
            return Result<PrivacySettingListDto>.Failure(HttpStatusCode.NotFound, DomainErrors.User.UserNotHasPrivacySetting);

        return Result<PrivacySettingListDto>.Success(
            new PrivacySettingListDto
            {
                SendMessages = setting.SendMessages.ToString(),
                TagInPosts = setting.TagInPosts.ToString(),
                ViewActivityStatus = setting.ViewActivityStatus.ToString(),
                ViewPosts = setting.ViewPosts.ToString(),
                ViewProfile = setting.ViewProfile.ToString()
            });
    }

    public async Task<Result<UserProfileDto>> GetUserProfileAsync(string userId)
    {
        var user = await userManager.FindByIdAsync(userId);

        if (user is null)
            return Result<UserProfileDto>.Failure(HttpStatusCode.NotFound, DomainErrors.User.UnkownUser);

        var mappedUserProfile = mapper.Map<UserProfileDto>(user);

        return Result<UserProfileDto>.Success(mappedUserProfile);
    }
}
