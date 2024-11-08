using AutoMapper;
using FluentValidation;
using Google.Apis.Auth;
using GreenSphere.Application.Abstractions;
using GreenSphere.Application.Features.Auth.DTOs;
using GreenSphere.Application.Features.Auth.Requests.Commands;
using GreenSphere.Application.Features.Auth.Validators.Commands;
using GreenSphere.Application.Helpers;
using GreenSphere.Application.Interfaces.Identity;
using GreenSphere.Application.Interfaces.Services;
using GreenSphere.Application.Interfaces.Services.Models;
using GreenSphere.Domain.Identity.Entities;
using GreenSphere.Domain.Identity.Enumerations;
using GreenSphere.Domain.Identity.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace GreenSphere.Identity.Services;
public sealed class AuthService(
    IMapper mapper,
    UserManager<ApplicationUser> userManager,
    ApplicationIdentityDbContext identityDbContext,
    IConfiguration configuration,
    SignInManager<ApplicationUser> signInManager,
    IMailService mailService,
    IOptions<JWT> jwtOptions,
    IHttpContextAccessor contextAccessor) : BaseResponseHandler, IAuthService
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

                var emailMessage = new MailkitEmail()
                {
                    Provider = command.Provider,
                    To = command.Email,
                    Subject = "Email Confirmed",
                    Body = @"
                    <div style='margin-top: 20px; font-size: 16px; color: #333;'>
                        <p>Hello,</p>
                        <p>Your email address has been successfully confirmed. You can now access all features of your account.</p>
                        <p style='font-size: 14px; color: #555;'>If you encounter any issues, feel free to reach out to our support team.</p>
                        <p>Best regards,<br>
                           <strong>Green Sphere</strong>
                        </p>
                    </div>"
                };

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

    public async Task<Result<SignInResponseDto>> LoginAsync(LoginCommand command)
    {
        var loggedInUser = await userManager.FindByEmailAsync(command.Email);

        if (loggedInUser is null)
            return NotFound<SignInResponseDto>(DomainErrors.User.UnkownUser);

        if (!await userManager.IsEmailConfirmedAsync(loggedInUser))
            return BadRequest<SignInResponseDto>(DomainErrors.User.EmailNotConfirmed);

        if (await userManager.GetTwoFactorEnabledAsync(loggedInUser))
        {
            // if two factor is enabled send code to user
            var twoFactorCode = await userManager.GenerateTwoFactorTokenAsync(loggedInUser, "Email");

            await signInManager.TwoFactorSignInAsync("Email", twoFactorCode, false, true);

            contextAccessor.HttpContext!.Response.Cookies.Append(
                "userEmail",
                Convert.ToBase64String(Encoding.UTF8.GetBytes(loggedInUser.Email!)),
                new CookieOptions
                {
                    HttpOnly = true,
                    SameSite = SameSiteMode.Strict
                });

            await mailService.SendEmailAsync(new MailkitEmail
            {
                To = command.Email,
                Provider = "gmail",
                Subject = "2FA Code Required",
                Body = $"2FA code is required to complete login process, Your 2FA code is {twoFactorCode}"
            });

            return BadRequest<SignInResponseDto>(DomainErrors.User.TwoFactorRequired);
        }

        // check account is locked
        if (await userManager.IsLockedOutAsync(loggedInUser))
        {
            return Unauthorized<SignInResponseDto>(
                $"Your account is locked until {loggedInUser.LockoutEnd!.Value.ToLocalTime()}");
        }

        var result = await signInManager.PasswordSignInAsync(
            user: loggedInUser,
            password: command.Password,
            isPersistent: true,
            lockoutOnFailure: true);

        if (result.IsLockedOut)
        {
            TimeZoneInfo timeZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");

            var lockoutEndTime = TimeZoneInfo.ConvertTimeFromUtc(loggedInUser.LockoutEnd!.Value.UtcDateTime, timeZone);

            return Unauthorized<SignInResponseDto>(
              $"Your account is locked until {lockoutEndTime}");
        }

        if (result.Succeeded)
        {
            SignInResponseDto response = await CreateLoginResponseAsync(userManager, loggedInUser);
            return Success(response);
        }

        return Unauthorized<SignInResponseDto>(DomainErrors.User.InvalidCredientials);
    }

    private async Task<SignInResponseDto> CreateLoginResponseAsync(UserManager<ApplicationUser> userManager, ApplicationUser loggedInUser)
    {
        var token = await GenerateJwtToken(loggedInUser);
        var userRoles = await userManager.GetRolesAsync(loggedInUser);
        var response = new SignInResponseDto()
        {
            Email = loggedInUser.Email!,
            UserName = loggedInUser.UserName!,
            IsAuthenticated = true,
            Roles = [.. userRoles],
            Token = new JwtSecurityTokenHandler().WriteToken(token),
        };

        if (loggedInUser.RefreshTokens != null && loggedInUser.RefreshTokens.Any(x => x.IsActive))
        {
            var activeRefreshToken = loggedInUser.RefreshTokens.FirstOrDefault(x => x.IsActive);
            if (activeRefreshToken != null)
            {
                response.RefreshToken = activeRefreshToken.Token;
                response.RefreshTokenExpiration = activeRefreshToken.ExpiresOn;
            }
        }
        else
        {
            // user not have active refresh token
            var refreshToken = GenerateRefreshToken();
            response.RefreshToken = refreshToken.Token;
            response.RefreshTokenExpiration = refreshToken.ExpiresOn;
            loggedInUser.RefreshTokens?.Add(refreshToken);
            await userManager.UpdateAsync(loggedInUser);
        }

        return response;
    }

    public async Task<Application.Bases.Result<SignInResponseDto>> RefreshTokenAsync(RefreshTokenCommand command)
        => (await (await (await (await CheckIfUserHasAssignedToRefreshToken(command.Token))
            .Bind(user => SelectRefreshTokenAssignedToUser(user, command.Token))
            .Bind(CheckIfTokenIsActive)
            .Bind(RevokeUserTokenAndReturnsAppUser)
            .BindAsync(GenerateNewRefreshToken))
            .BindAsync(GenerateNewJwtToken))
            .BindAsync(CreateSignInResponse))
            .Map(authResponse => authResponse);

    public async Task<Application.Bases.Result<bool>> RevokeTokenAsync(RevokeTokenCommand command)
        => (await (await CheckIfUserHasAssignedToRefreshToken(command.Token!))
            .Bind(appUser => SelectRefreshTokenAssignedToUser(appUser, command.Token!))
            .Bind(CheckIfTokenIsActive)
            .Bind(RevokeUserTokenAndReturnsAppUser)
            .BindAsync(UpdateApplicationUser))
            .Map(userUpdated => userUpdated);

    private async Task<Application.Bases.Result<bool>> UpdateApplicationUser(ApplicationUser appUser)
    {
        await userManager.UpdateAsync(appUser);
        return Application.Bases.Result<bool>.Success(true);
    }


    private async Task<Application.Bases.Result<SignInResponseDto>> CreateSignInResponse((ApplicationUser appUser, JwtSecurityToken jwtToken) appUserWithJwt)
    {
        var userRoles = await userManager.GetRolesAsync(appUserWithJwt.appUser);
        var newRefreshToken = appUserWithJwt.appUser.RefreshTokens?.FirstOrDefault(x => x.IsActive);

        var response = new SignInResponseDto
        {
            IsAuthenticated = true,
            UserName = appUserWithJwt.appUser.UserName!,
            Email = appUserWithJwt.appUser.Email!,
            Token = new JwtSecurityTokenHandler().WriteToken(appUserWithJwt.jwtToken),
            Roles = [.. userRoles],
            RefreshToken = newRefreshToken?.Token,
            RefreshTokenExpiration = newRefreshToken!.ExpiresOn
        };

        return Application.Bases.Result<SignInResponseDto>.Success(response);
    }

    private async Task<Application.Bases.Result<(ApplicationUser appUser, JwtSecurityToken jwtToken)>> GenerateNewJwtToken(ApplicationUser appUser)
    {
        var jwtToken = await GenerateJwtToken(appUser);
        return Application.Bases.Result<(ApplicationUser appUser, JwtSecurityToken jwtToken)>.Success((appUser, jwtToken));
    }

    private async Task<Application.Bases.Result<ApplicationUser>> GenerateNewRefreshToken(ApplicationUser appUser)
    {
        var newRefreshToken = GenerateRefreshToken();
        appUser.RefreshTokens?.Add(newRefreshToken);
        await userManager.UpdateAsync(appUser);
        return Application.Bases.Result<ApplicationUser>.Success(appUser);
    }

    private Application.Bases.Result<ApplicationUser> RevokeUserTokenAndReturnsAppUser(RefreshToken userRefreshToken)
    {
        userRefreshToken.RevokedOn = DateTimeOffset.Now;
        var user = userManager.Users.SingleOrDefault(x => x.RefreshTokens != null && x.RefreshTokens.Any(x => x.Token == userRefreshToken.Token));
        return Application.Bases.Result<ApplicationUser>.Success(user!);
    }

    private static Application.Bases.Result<RefreshToken> CheckIfTokenIsActive(RefreshToken userRefreshToken)
    {
        if (!userRefreshToken.IsActive)
            return Application.Bases.Result<RefreshToken>.Failure(HttpStatusCode.BadRequest, "Inactive token");
        return Application.Bases.Result<RefreshToken>.Success(userRefreshToken);
    }

    private static Application.Bases.Result<RefreshToken> SelectRefreshTokenAssignedToUser(ApplicationUser user, string token)
    {
        var refreshToken = user.RefreshTokens?.Single(x => x.Token == token);
        if (refreshToken is not null)
            return Application.Bases.Result<RefreshToken>.Success(refreshToken);
        return Application.Bases.Result<RefreshToken>.Failure(HttpStatusCode.NotFound, "Token not found");
    }

    private async Task<JwtSecurityToken> GenerateJwtToken(ApplicationUser user)
    {
        var userClaims = await userManager.GetClaimsAsync(user);
        var roles = await userManager.GetRolesAsync(user);

        var roleClaims = new List<Claim>();

        for (int i = 0; i < roles.Count; i++)
        {
            roleClaims.Add(new Claim(ClaimTypes.Role, roles[i]));
        }

        var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(CustomClaimTypes.Uid, user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var jwtSecurityToken = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddDays(_jwtSettings.ExpirationInDays),
            signingCredentials: signingCredentials);
        return jwtSecurityToken;
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

            var emailMessage = new MailkitEmail()
            {
                Provider = command.Provider,
                To = command.Email,
                Subject = "Activate Account",
                Body = @$" <div style='margin-top: 20px;'>
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
            };

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

    private static RefreshToken GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using var rng = new RNGCryptoServiceProvider();
        rng.GetBytes(randomNumber);
        return new RefreshToken()
        {
            Token = Convert.ToBase64String(randomNumber),
            ExpiresOn = DateTimeOffset.Now.AddDays(10),
            CreatedOn = DateTimeOffset.Now
        };
    }

    private async Task<Application.Bases.Result<ApplicationUser>> CheckIfUserHasAssignedToRefreshToken(string refreshToken)
    {
        var user = await userManager.Users.SingleOrDefaultAsync(x => x.RefreshTokens != null && x.RefreshTokens.Any(x => x.Token == refreshToken));
        return user is null ? Application.Bases.Result<ApplicationUser>.Failure(HttpStatusCode.NotFound, "Invalid Token") :
            Application.Bases.Result<ApplicationUser>.Success(user);
    }


    public async Task<Result<string>> ForgotPasswordAsync(ForgotPasswordCommand command)
    {
        var user = await userManager.FindByEmailAsync(command.Email);
        if (user == null)
            return NotFound<string>("User not found");

        //code and Expiration 
        var Decoded = await userManager.GenerateUserTokenAsync(user, "Email", "Generate Code");
        var authCode = Convert.ToBase64String(Encoding.UTF8.GetBytes(Decoded));
        user.Code = authCode;
        user.CodeExpiration = DateTimeOffset.Now.AddMinutes(Convert.ToDouble(configuration["AuthCodeExpirationInMinutes"]));

        //Update user
        var updateResult = await userManager.UpdateAsync(user);
        if (!updateResult.Succeeded)
            return BadRequest<string>(DomainErrors.User.UnableToUpdateUser);

        //Send code to email
        var emailMessage = new MailkitEmail
        {
            Provider = command.Provider,
            To = command.Email,
            Subject = "Reset Password Code",
            Body = $@"
                   <div style='margin-top: 20px; font-size: 16px; color: #333;'>
                       <p>Hello,</p>
                       <p>Your password reset code is: <strong>{Decoded}</strong>. This code will expire in {configuration["AuthCodeExpirationInMinutes"]} minutes.</p>
                       <p style='font-size: 14px; color: #555;'>If you did not request a password reset, please ignore this email.</p>
                       <p>Best regards,<br>
                          <strong>Green Sphere</strong>
                       </p>
                   </div>"
        };

        await mailService.SendEmailAsync(emailMessage);

        return Success("Password reset code has been sent to your email.");

    }

    public async Task<Result<string>> ConfirmForgotPasswordCodeAsync(ConfirmForgotPasswordCodeCommand command)
    {
        var user = await userManager.FindByEmailAsync(command.Email);
        if (user == null)
            return NotFound<string>(DomainErrors.User.UserNotFound);

        var decodedAuthCode = Encoding.UTF8.GetString(Convert.FromBase64String(user.Code!));
        if (decodedAuthCode != command.Code)
            return BadRequest<string>(DomainErrors.User.InvalidAuthCode);

        if (DateTimeOffset.Now > user.CodeExpiration)
            return BadRequest<string>(DomainErrors.User.CodeExpired);

        await userManager.RemovePasswordAsync(user);
        var result = await userManager.AddPasswordAsync(user, command.NewPassword);

        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(x => x.Description).FirstOrDefault();
            return UnprocessableEntity<string>(errors);
        }


        return Success<string>("Reset Password Successfuly.");

    }

    public async Task<Result<string>> Enable2FAAsync(Enable2FACommand command)
    {
        var user = await userManager.FindByEmailAsync(command.Email);

        if (user == null)
            return BadRequest<string>(string.Format(DomainErrors.User.UserNotFound, command.Email));

        var code = await userManager.GenerateTwoFactorTokenAsync(user, command.TokenProvider.ToString());

        await signInManager.TwoFactorSignInAsync(code, command.TokenProvider.ToString(), false, true);

        if (command.TokenProvider == TokenProvider.Email)
            // send code via user email
            await mailService.SendEmailAsync(new MailkitEmail
            {
                Provider = "gmail",
                To = command.Email,
                Subject = "Enable 2FA Request",
                Body = $"Your 2FA Authentication Code is {code}"
            });

        else if (command.TokenProvider == TokenProvider.Phone)
        {
            // handle send via phone
        }

        return Success(Constants.TwoFactorCodeSent);
    }

    public async Task<Result<string>> ConfirmEnable2FAAsync(ConfirmEnable2FACommand command)
    {
        var user = await userManager.FindByEmailAsync(command.Email);
        if (user == null)
            return NotFound<string>(DomainErrors.User.UnkownUser);

        // verify 2fa code
        var providers = await userManager.GetValidTwoFactorProvidersAsync(user);

        if (providers.Contains(TokenProvider.Email.ToString()))
        {
            var verified = await userManager.VerifyTwoFactorTokenAsync(user, TokenProvider.Email.ToString(), command.Code);

            if (!verified)
                return BadRequest<string>(DomainErrors.User.Invalid2FACode);

            // code is verified update status of 2FA

            await userManager.SetTwoFactorEnabledAsync(user, true);

            await mailService.SendEmailAsync(new MailkitEmail
            {
                Provider = "gmail",
                Subject = "Setup 2FA complete",
                To = user.Email!,
                Body = "Your 2FA authentication is successfully enabled."
            });

            return Success(Constants.TwoFactorEnabled);
        }

        return BadRequest<string>(DomainErrors.User.InvalidTokenProvider);
    }

    public async Task<Result<SignInResponseDto>> Verify2FACodeAsync(Verify2FACodeCommand command)
    {
        var userEmail = Encoding.UTF8.GetString(Convert.FromBase64String(contextAccessor.HttpContext!.Request.Cookies["userEmail"]!));
        var appUser = await userManager.FindByEmailAsync(userEmail);

        if (appUser == null) return Unauthorized<SignInResponseDto>();

        var verified = await userManager.VerifyTwoFactorTokenAsync(appUser, "Email", command.Code);

        if (!verified)
            return BadRequest<SignInResponseDto>(DomainErrors.User.Invalid2FACode);

        await signInManager.SignInAsync(appUser, isPersistent: true);

        SignInResponseDto response = await CreateLoginResponseAsync(userManager, appUser);

        return Success(response);
    }

    public async Task<Result<string>> Disable2FAAsync(Disable2FACommand command)
    {
        var user = await userManager.FindByEmailAsync(command.Email);

        if (user == null)
            return NotFound<string>(string.Format(DomainErrors.User.UserNotFound, command.Email));

        if (!await userManager.GetTwoFactorEnabledAsync(user))
            return BadRequest<string>(DomainErrors.User.TwoFactorAlreadyDisabled);

        var result = await userManager.SetTwoFactorEnabledAsync(user, false);

        if (!result.Succeeded)
            return BadRequest<string>(DomainErrors.User.Disable2FAFailed);

        await mailService.SendEmailAsync(new MailkitEmail
        {
            Provider = "gmail",
            To = user.Email!,
            Subject = "2FA Disabled",
            Body = "Your two-factor authentication has been successfully disabled."
        });

        return Success(Constants.Disable2FASuccess);
    }
}