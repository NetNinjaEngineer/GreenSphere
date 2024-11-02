using AutoMapper;
using FluentValidation;
using Google.Apis.Auth;
using GreenSphere.Application.Abstractions;
using GreenSphere.Application.Features.Auth.DTOs;
using GreenSphere.Application.Features.Auth.Requests.Commands;
using GreenSphere.Application.Features.Auth.Validators.Commands;
using GreenSphere.Application.Helpers;
using GreenSphere.Application.Interfaces.Identity;
using GreenSphere.Application.Interfaces.Identity.Entities;
using GreenSphere.Application.Interfaces.Identity.Models;
using GreenSphere.Application.Interfaces.Services;
using GreenSphere.Application.Interfaces.Services.Models;
using Microsoft.AspNetCore.Identity;
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

    public async Task<Result<SignInResponseDto>> LoginAsync(LoginCommand command)
    {
        var loggedInUser = await userManager.FindByEmailAsync(command.Email);

        if (loggedInUser is null)
            return NotFound<SignInResponseDto>(DomainErrors.User.UnkownUser);

        if (!await userManager.IsEmailConfirmedAsync(loggedInUser))
            return BadRequest<SignInResponseDto>(DomainErrors.User.EmailNotConfirmed);

        if (!await userManager.CheckPasswordAsync(loggedInUser, command.Password))
            return BadRequest<SignInResponseDto>(DomainErrors.User.InvalidCredientials);

        await signInManager.PasswordSignInAsync(
            user: loggedInUser,
            password: command.Password,
            isPersistent: true,
            lockoutOnFailure: false);

        var token = await GenerateJwtToken(loggedInUser);
        var userRoles = await userManager.GetRolesAsync(loggedInUser);
        var response = new SignInResponseDto()
        {
            Email = loggedInUser.Email,
            UserName = loggedInUser.UserName,
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
            loggedInUser.RefreshTokens.Add(refreshToken);
            await userManager.UpdateAsync(loggedInUser);
        }

        return Success(response);
    }

    public Application.Bases.Result<SignInResponseDto> RefreshToken(RefreshTokenCommand command)
        => CheckIfUserHasAssignedToRefreshToken(command.Token)
            .Bind(user => SelectRefreshTokenAssignedToUser(user, command.Token))
            .Bind(CheckIfTokenIsActive)
            .Bind(RevokeUserTokenAndReturnsAppUser)
            .Bind(appUser => GenerateNewRefreshToken(appUser).Result)
            .Bind(appUser => GenerateNewJwtToken(appUser).Result)
            .Bind(appUserWithJwt => CreateSignInResponse(appUserWithJwt).Result)
            .Map((authResponse) => authResponse);

    public Application.Bases.Result<bool> RevokeTokenAsync(RevokeTokenCommand command)
        => CheckIfUserHasAssignedToRefreshToken(command.Token)
            .Bind(appUser => SelectRefreshTokenAssignedToUser(appUser, command.Token))
            .Bind(CheckIfTokenIsActive)
            .Bind(RevokeUserTokenAndReturnsAppUser)
            .Bind(appUser => UpdateApplicationUser(appUser).Result)
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

    private Application.Bases.Result<ApplicationUser> CheckIfUserHasAssignedToRefreshToken(string refreshToken)
    {
        var user = userManager.Users.SingleOrDefault(x => x.RefreshTokens != null && x.RefreshTokens.Any(x => x.Token == refreshToken));
        return user is null ? Application.Bases.Result<ApplicationUser>.Failure(HttpStatusCode.NotFound, "Invalid Token") :
            Application.Bases.Result<ApplicationUser>.Success(user);
    }
}