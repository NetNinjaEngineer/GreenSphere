﻿using AutoMapper;
using FluentValidation;
using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Users;
using GreenSphere.Application.Features.Users.Commands.AssignUserPrivacy;
using GreenSphere.Application.Features.Users.Commands.ChangeUserEmail;
using GreenSphere.Application.Features.Users.Commands.ChangeUserPassword;
using GreenSphere.Application.Features.Users.Commands.EditUserProfile;
using GreenSphere.Application.Features.Users.Commands.VerifyChangeUserEmail;
using GreenSphere.Application.Helpers;
using GreenSphere.Application.Interfaces.Identity;
using GreenSphere.Application.Interfaces.Services;
using GreenSphere.Application.Interfaces.Services.Models;
using GreenSphere.Domain.Entities;
using GreenSphere.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using System.Net;
using System.Text;

namespace GreenSphere.Services.Services;
public sealed class UserPrivacyService(
    UserManager<ApplicationUser> userManager,
    ApplicationDbContext context,
    ICurrentUser currentUser,
    SignInManager<ApplicationUser> _signInManage,
    IMailService _mailService,
    IConfiguration _configuration,
    IMapper mapper,
    IStringLocalizer<UserPrivacyService> localizer) : IUserPrivacyService
{

    public async Task<Result<string>> AssignPrivacyToUserAsync(AssignUserPrivacyCommand command)
    {
        var user = await userManager.FindByIdAsync(command.UserId);

        if (user is null)
            return Result<string>.Failure(HttpStatusCode.NotFound, localizer["UnkownUser"]);

        if (await context.PrivacySettings.AnyAsync(s => s.UserId == command.UserId))
            return Result<string>.Failure(HttpStatusCode.Conflict, localizer["UserHasPrivacy"]);

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
            return Result<PrivacySettingListDto>.Failure(HttpStatusCode.NotFound, localizer["UnkownUser"]);

        var setting = await context.PrivacySettings.FirstOrDefaultAsync(x => x.UserId == userId);

        if (setting == null)
            return Result<PrivacySettingListDto>.Failure(HttpStatusCode.NotFound, localizer["UserNotHasPrivacySetting"]);

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
            return Result<UserProfileDto>.Failure(HttpStatusCode.NotFound, localizer["UnkownUser"]);

        var mappedUserProfile = mapper.Map<UserProfileDto>(user);

        return Result<UserProfileDto>.Success(mappedUserProfile);
    }

    public async Task<Result<UserProfileDto>> EditUserProfileAsync(EditUserProfileCommand command)
    {
        await new EditUserProfileCommandValidator().ValidateAndThrowAsync(command);

        var user = await userManager.FindByIdAsync(currentUser.Id);
        if (user is null)
            return Result<UserProfileDto>.Failure(HttpStatusCode.NotFound, DomainErrors.User.UnkownUser);

        user.FirstName = command.FirstName;
        user.LastName = command.LastName;
        user.Email = command.Email;
        user.Gender = command.Gender;
        user.DateOfBirth = command.DateOfBirth;

        var result = await userManager.UpdateAsync(user);
        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(e => $"{e.Code}: {e.Description}").ToList();
            return Result<UserProfileDto>.Failure(
                HttpStatusCode.BadRequest, errors: errors);
        }
        var mappedUserProfile = mapper.Map<UserProfileDto>(user);
        return Result<UserProfileDto>.Success(mappedUserProfile);
    }

    public async Task<Result<bool>> ChangeUserEmailAsync(ChangeUserEmailCommand command)
    {
        await new ChangeUserEmailCommandValidation().ValidateAndThrowAsync(command);

        var existingUser = await userManager.FindByEmailAsync(command.NewEmail);
        if (existingUser != null)
            return Result<bool>.Failure(HttpStatusCode.BadRequest, DomainErrors.User.EmailInUse);

        var user = await userManager.FindByIdAsync(currentUser.Id);
        if (user == null)
            return Result<bool>.Failure(HttpStatusCode.NotFound, DomainErrors.User.UserNotFound);

        var isPasswordValid = await userManager.CheckPasswordAsync(user, command.CurrentPassword);
        if (!isPasswordValid)
            return Result<bool>.Failure(HttpStatusCode.Unauthorized, DomainErrors.User.InvalidCurrentPassword);

        var authenticationCode = await userManager.GenerateUserTokenAsync(user, "Email", "Confirm User Email");
        var encodedAuthenticationCode = Convert.ToBase64String(Encoding.UTF8.GetBytes(authenticationCode));

        user.Code = encodedAuthenticationCode;
        user.CodeExpiration = DateTimeOffset.Now.AddMinutes(
            minutes: Convert.ToDouble(_configuration[Constants.AuthCodeExpireKey])
        );

        user.Email = command.NewEmail;
        user.EmailConfirmed = false;

        var updateResult = await userManager.UpdateAsync(user);
        if (!updateResult.Succeeded)
        {
            var errors = updateResult.Errors.Select(e => e.Description).ToList();
            return Result<bool>.Failure(HttpStatusCode.BadRequest, errors: errors);
        }
        await _mailService.SendEmailAsync(new MailkitEmail
        {
            To = command.NewEmail,
            Subject = "Confirm Email",
            Body = @$" <div style='margin-top: 20px;'>
                       <p style='font-size: 16px;'>Hello,</p>
                       <p style='font-size: 14px; line-height: 1.5;'>
                            Please confirm your account using the following code:
                       </p>
                       
                       <div style='text-align: center; margin: 20px 0;'>
                           <span style='font-size: 24px; font-weight: bold; color: #2c7dfa;'>{authenticationCode}</span>
                       </div>

                       <p style='font-size: 14px; line-height: 1.5;'>
                           This code will expire in <strong>{_configuration[Constants.AuthCodeExpireKey]} minutes</strong>.
                       </p>
                       
                       <p style='font-size: 14px; line-height: 1.5; color: #888;'>
                           If you did not request this registration, please ignore this email.
                       </p>
                       
                       <p style='font-size: 14px; line-height: 1.5; color: #333;'>
                           Best regards,<br>
                           <strong>Green Sphere</strong>
                       </p>
                   </div>",
            Provider = "gmail"
        });
        return Result<bool>.Success(true);
    }
    public async Task<Result<bool>> VerifyChangeUserEmailAsync(VerifyChangeUserEmailCommand command)
    {
        await new VerifyChangeUserEmailCommandValidation().ValidateAndThrowAsync(command);

        var user = await userManager.FindByIdAsync(currentUser.Id);
        if (user == null)
            return Result<bool>.Failure(HttpStatusCode.NotFound, DomainErrors.User.UserNotFound);

        var storedCode = Encoding.UTF8.GetString(Convert.FromBase64String(user.Code));

        if (storedCode != command.Code || user.CodeExpiration < DateTimeOffset.Now)
            return Result<bool>.Failure(HttpStatusCode.BadRequest, DomainErrors.User.CodeExpired);

        user.EmailConfirmed = true;

        var updateResult = await userManager.UpdateAsync(user);
        if (!updateResult.Succeeded)
            return Result<bool>.Failure(HttpStatusCode.BadRequest, DomainErrors.User.FailedToChangeEmail);

        return Result<bool>.Success(true);
    }
    public async Task<Result<bool>> ChangeUserPasswordAsync(ChangeUserPasswordCommand command)
    {
        await new ChangeUserPasswordCommandValidation().ValidateAndThrowAsync(command);


        var user = await userManager.FindByIdAsync(currentUser.Id);
        if (user == null)
            return Result<bool>.Failure(HttpStatusCode.NotFound, DomainErrors.User.UserNotFound);

        var isPasswordValid = await userManager.CheckPasswordAsync(user, command.CurrentPassword);
        if (!isPasswordValid)
            return Result<bool>.Failure(HttpStatusCode.BadRequest, DomainErrors.User.InvalidCurrentPassword);


        var changePasswordResult = await userManager.ChangePasswordAsync(user, command.CurrentPassword, command.NewPassword);
        if (!changePasswordResult.Succeeded)
        {
            var errors = changePasswordResult.Errors.Select(e => $"{e.Code}: {e.Description}").ToList();
            return Result<bool>.Failure(HttpStatusCode.BadRequest, errors: errors);
        }

        return Result<bool>.Success(true);
    }

    public async Task LogoutAsync() => await _signInManage.SignOutAsync();

}