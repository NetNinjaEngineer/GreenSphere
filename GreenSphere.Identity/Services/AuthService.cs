using AutoMapper;
using GreenSphere.Application.Abstractions;
using GreenSphere.Application.Features.Auth.DTOs;
using GreenSphere.Application.Features.Auth.Requests.Commands;
using GreenSphere.Application.Helpers;
using GreenSphere.Application.Interfaces.Identity;
using GreenSphere.Application.Interfaces.Services;
using GreenSphere.Application.Interfaces.Services.Models;
using GreenSphere.Identity.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using System.Text;

namespace GreenSphere.Identity.Services;
public sealed class AuthService(
    IMapper mapper,
    UserManager<ApplicationUser> userManager,
    ApplicationIdentityDbContext identityDbContext,
    IConfiguration configuration,
    SignInManager<ApplicationUser> signInManager,
    IMailService mailService,
    IHttpContextAccessor contextAccessor) : BaseResponseHandler, IAuthService
{
    public async Task<Result<string>> ConfirmEmailAsync(string email, string code)
    {
        var transaction = await identityDbContext.Database.BeginTransactionAsync();
        try
        {
            var user = await userManager.FindByEmailAsync(email);

            if (user is null)
                return NotFound<string>(DomainErrors.User.UnkownUser);

            var decodedAuthenticationCode = Encoding.UTF8.GetString(Convert.FromBase64String(user.Code!));

            if (decodedAuthenticationCode == code)
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
                   to: email,
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

    public async Task<Result<string>> LoginWithGoogleAsync()
    {
        var result = await contextAccessor.HttpContext!.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);
        if (!result.Succeeded)
            return Unauthorized<string>();

        var emailClaim = result.Principal.FindFirst(ClaimTypes.Email)?.Value;
        if (emailClaim == null)
            return BadRequest<string>("Google authentication didn't return an email");

        var user = await userManager.FindByEmailAsync(emailClaim);
        if (user == null)
        {
            user = new ApplicationUser
            {
                UserName = emailClaim,
                Email = emailClaim,
                EmailConfirmed = true
            };
            var createResult = await userManager.CreateAsync(user);
            if (!createResult.Succeeded)
                return BadRequest<string>("Failed to create user.");
        }

        await signInManager.SignInAsync(user, isPersistent: false);

        return Success("Google login successful");
    }

    public async Task LogoutAsync() => await signInManager.SignOutAsync();

    public async Task<Result<SignUpResponseDto>> RegisterAsync(RegisterCommand command)
    {
        var user = mapper.Map<ApplicationUser>(command);
        var result = await userManager.CreateAsync(user, command.Password);

        await userManager.AddToRoleAsync(user, Constants.Roles.User);

        return !result.Succeeded ?
            BadRequest<SignUpResponseDto>(DomainErrors.User.UnableToCreateAccount, [result.Errors.Select(e => e.Description).FirstOrDefault()]) :
            Success(SignUpResponseDto.ToResponse(Guid.Parse(user.Id)));
    }

    public async Task<Result<SendCodeConfirmEmailResponseDto>> SendConfirmEmailCodeAsync(string email)
    {
        var transaction = await identityDbContext.Database.BeginTransactionAsync();

        try
        {
            var user = await userManager.FindByEmailAsync(email);

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
                to: email,
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