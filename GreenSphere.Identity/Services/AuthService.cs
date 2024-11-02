using AutoMapper;
using Google.Apis.Auth;
using GreenSphere.Application.Abstractions;
using GreenSphere.Application.Features.Auth.DTOs;
using GreenSphere.Application.Features.Auth.Requests.Commands;
using GreenSphere.Application.Helpers;
using GreenSphere.Application.Interfaces.Identity;
using GreenSphere.Application.Interfaces.Identity.Entities;
using GreenSphere.Application.Interfaces.Identity.Models;
using GreenSphere.Application.Interfaces.Services;
using GreenSphere.Application.Interfaces.Services.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Text;
using FluentValidation;
using GreenSphere.Application.Features.Auth.Validators.Commands;

namespace GreenSphere.Identity.Services;
public sealed class AuthService(
    IMapper mapper,
    UserManager<ApplicationUser> userManager,
    ApplicationIdentityDbContext identityDbContext,
    IConfiguration configuration,
    SignInManager<ApplicationUser> signInManager,
    IMailService mailService,
    IOptions<JWT> jwtOptions) : BaseResponseHandler, IAuthService
{
    private readonly JWT _jwtSettings = jwtOptions.Value;
    public async Task<Result<string>> ConfirmEmailAsync(ConfirmEmailCommand command)
    {
        var validator = new ConfirmEmailCommandValidator();
        await validator.ValidateAndThrowAsync(command);
        
        var transaction = await identityDbContext.Database.BeginTransactionAsync();
        try
        {
            var user = await userManager.FindByEmailAsync(command.Email);

            if (user is null)
                return NotFound<string>(DomainErrors.User.UnkownUser);

            var decodedAuthenticationCode = Encoding.UTF8.GetString(Convert.FromBase64String(user.Code!));

            if (decodedAuthenticationCode == command.Token)
            {
                // check if the token is expired
                if (DateTimeOffset.Now > user.CodeExpiration)
                    return BadRequest<string>(DomainErrors.User.AuthCodeExpired);

                // confirm the email for the user
                user.EmailConfirmed = true;
                var identityResult = await userManager.UpdateAsync(user);

                if (!identityResult.Succeeded)
                {
                    var errors = identityResult.Errors
                   .Select(e => e.Description)
                   .ToList();

                    return BadRequest<string>(DomainErrors.User.UnableToUpdateUser, errors);
                }

                var emailMessage = EmailMessage.Create(
                   to: command.Email,
                   subject: "Email Confirmed",
                   message: @"
                    <div style='margin-top: 20px; font-size: 16px; color: #333;'>
                        <p>Hello,</p>
                        <p>Your email address has been successfully confirmed. You can now access all features of your account.</p>
                        <p style='font-size: 14px; color: #555;'>If you encounter any issues, feel free to reach out to our support team.</p>
                        <p>Best regards,<br>
                           <strong>Green Sphere</strong>
                        </p>
                    </div>"
                );

                await mailService.SendEmailAsync(emailMessage);

                await transaction.CommitAsync();
                return Success(Constants.EmailConfirmed);
            }

            return BadRequest<string>(DomainErrors.User.InvalidAuthCode);
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            return BadRequest<string>(ex.Message);
        }
    }

    public async Task<Result<GoogleAuthResponseDto>> GoogleLoginAsync(GoogleLoginCommand command)
    {
        var validationSettings = new GoogleJsonWebSignature.ValidationSettings
        {
            Audience = [configuration["Authentication:Google:ClientId"]!]
        };

        var payload = await GoogleJsonWebSignature.ValidateAsync(command.IdToken, validationSettings);
        if (payload == null)
        {
            return Unauthorized<GoogleAuthResponseDto>();
        }

        var user = await userManager.FindByEmailAsync(payload.Email);
        if (user != null)
        {
            return await CreateAuthResponseAsync(user);
        }

        user = new ApplicationUser
        {
            FirstName = payload.GivenName,
            LastName = payload.FamilyName,
            Email = payload.Email,
            NormalizedEmail = payload.Email.ToUpper(),
            UserName = payload.GivenName,
            EmailConfirmed = true
        };

        var createResult = await userManager.CreateAsync(user);
        if (!createResult.Succeeded)
        {
            var errors = createResult.Errors.Select(e => e.Description).ToList();
            return BadRequest<GoogleAuthResponseDto>(DomainErrors.User.UnableToCreateAccount, errors);
        }

        var loginInfo = new UserLoginInfo("Google", user.Id, "Google");
        await userManager.AddLoginAsync(user, loginInfo);

        await signInManager.SignInAsync(user, false);
        return await CreateAuthResponseAsync(user);
    }

    private async Task<Result<GoogleAuthResponseDto>> CreateAuthResponseAsync(ApplicationUser user)
    {
        var userRoles = await userManager.GetRolesAsync(user);

        var googleAuthResponse = new GoogleAuthResponseDto
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            UserId = user.Id,
            UserName = user.UserName!,
            Roles = userRoles.ToList(),
        };

        return Success(googleAuthResponse);
    }


    public async Task LogoutAsync() => await signInManager.SignOutAsync();

    public async Task<Result<SignUpResponseDto>> RegisterAsync(RegisterCommand command)
    {
        var registerCommandValidator = new RegisterCommandValidator();
        await registerCommandValidator.ValidateAndThrowAsync(command);
        
        var user = mapper.Map<ApplicationUser>(command);
        var result = await userManager.CreateAsync(user, command.Password);

        await userManager.AddToRoleAsync(user, Constants.Roles.User);

        return !result.Succeeded ?
            BadRequest<SignUpResponseDto>(DomainErrors.User.UnableToCreateAccount, [result.Errors.Select(e => e.Description).FirstOrDefault()]) :
            Success(SignUpResponseDto.ToResponse(Guid.Parse(user.Id)));
    }

    public async Task<Result<SendCodeConfirmEmailResponseDto>> SendConfirmEmailCodeAsync(SendConfirmEmailCodeCommand command)
    {
        var confirmEmailValidator = new SendConfirmEmailCodeCommandValidator();
        await confirmEmailValidator.ValidateAndThrowAsync(command);
        
        var transaction = await identityDbContext.Database.BeginTransactionAsync();

        try
        {
            var user = await userManager.FindByEmailAsync(command.Email);

            if (user is null)
                return NotFound<SendCodeConfirmEmailResponseDto>(DomainErrors.User.UnkownUser);

            if (await userManager.IsEmailConfirmedAsync(user))
                return Conflict<SendCodeConfirmEmailResponseDto>(DomainErrors.User.AlreadyEmailConfirmed);

            var authenticationCode = await userManager.GenerateUserTokenAsync(user, "Email", "Confirm User Email");
            var encodedAuthenticationCode = Convert.ToBase64String(Encoding.UTF8.GetBytes(authenticationCode));

            user.Code = encodedAuthenticationCode;
            user.CodeExpiration = DateTimeOffset.Now.AddMinutes(
                    minutes: Convert.ToDouble(configuration[Constants.AuthCodeExpireKey]!)
                );

            var identityResult = await userManager.UpdateAsync(user);

            if (!identityResult.Succeeded)
            {
                var errors = identityResult.Errors
                    .Select(e => e.Description)
                    .ToList();

                return BadRequest<SendCodeConfirmEmailResponseDto>(DomainErrors.User.UnableToUpdateUser, errors);
            }

            var emailMessage = EmailMessage.Create(
                to: command.Email,
                subject: "Activate Account",
                message: @$" <div style='margin-top: 20px;'>
                      <p style='font-size: 16px;'>Hello,</p>
                      <p style='font-size: 14px; line-height: 1.5;'>
                          Thank you for registering. Please activate your account using the following code:
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
                  </div>"
            );

            await mailService.SendEmailAsync(emailMessage);


            transaction.Commit();
            return Success(
                entity: SendCodeConfirmEmailResponseDto.ToResponse(user.CodeExpiration.Value),
                message: Constants.ConfirmEmailCodeSentSuccessfully
                );
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            return BadRequest<SendCodeConfirmEmailResponseDto>(ex.Message);
        }

    }
}