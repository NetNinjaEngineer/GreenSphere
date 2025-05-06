using System.Net;
using System.Text;
using AutoMapper;
using FluentValidation;
using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Users;
using GreenSphere.Application.Features.Users.Commands.ChangeUserEmail;
using GreenSphere.Application.Features.Users.Commands.ChangeUserPassword;
using GreenSphere.Application.Features.Users.Commands.EditUserProfile;
using GreenSphere.Application.Features.Users.Commands.VerifyChangeUserEmail;
using GreenSphere.Application.Helpers;
using GreenSphere.Application.Interfaces.Services;
using GreenSphere.Application.Interfaces.Services.Models;
using GreenSphere.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace GreenSphere.Services.Services;
public sealed class UserService(
    ICurrentUser currentUser,
    IMailService mailService,
    IConfiguration configuration,
    IMapper mapper,
    IStringLocalizer<UserService> localizer,
    UserManager<ApplicationUser> userManager,
    ILogger<UserService> logger) : IUserService
{
    public async Task<Result<UserProfileDto>> GetUserProfileAsync()
    {
        var user = await userManager.FindByIdAsync(currentUser.Id);
        if (user is null)
            return Result<UserProfileDto>.Failure(HttpStatusCode.NotFound, localizer["UnknownUser"]);

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
            minutes: Convert.ToDouble(configuration[Constants.AuthCodeExpireKey])
        );

        user.Email = command.NewEmail;
        user.EmailConfirmed = false;

        var updateResult = await userManager.UpdateAsync(user);
        if (!updateResult.Succeeded)
        {
            var errors = updateResult.Errors.Select(e => e.Description).ToList();
            return Result<bool>.Failure(HttpStatusCode.BadRequest, errors: errors);
        }
        await mailService.SendEmailAsync(new MailkitEmail
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
                           This code will expire in <strong>{configuration[Constants.AuthCodeExpireKey]} minutes</strong>.
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

        var storedCode = Encoding.UTF8.GetString(Convert.FromBase64String(user.Code!));

        if (storedCode != command.Code || user.CodeExpiration < DateTimeOffset.Now)
            return Result<bool>.Failure(HttpStatusCode.BadRequest, DomainErrors.User.CodeExpired);

        user.EmailConfirmed = true;

        var updateResult = await userManager.UpdateAsync(user);

        return !updateResult.Succeeded ? Result<bool>.Failure(HttpStatusCode.BadRequest, DomainErrors.User.FailedToChangeEmail) : Result<bool>.Success(true);
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
        if (changePasswordResult.Succeeded) return Result<bool>.Success(true);

        var errors = changePasswordResult.Errors.Select(e => $"{e.Code}: {e.Description}").ToList();
        return Result<bool>.Failure(HttpStatusCode.BadRequest, errors: errors);

    }

    public async Task<Result<bool>> DeleteMyAccountAsync()
    {
        var user = await userManager.FindByEmailAsync(currentUser.Email);
        if (user == null) return Result<bool>.Failure(HttpStatusCode.BadRequest);
        var result = await userManager.DeleteAsync(user);
        if (result.Succeeded) return Result<bool>.Success(true);
        var error = result.Errors.Select(e => e.Description).FirstOrDefault();
        logger.LogError($"Failed to delete the user account: {error}");
        return Result<bool>.Failure(HttpStatusCode.BadRequest);

    }
}