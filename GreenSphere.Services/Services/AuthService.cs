using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using AutoMapper;
using FluentValidation;
using Google.Apis.Auth;
using GreenSphere.Application.Abstractions;
using GreenSphere.Application.DTOs.Auth;
using GreenSphere.Application.Features.Auth.Commands.ConfirmEmail;
using GreenSphere.Application.Features.Auth.Commands.ConfirmEnable2FA;
using GreenSphere.Application.Features.Auth.Commands.ConfirmForgotPasswordCode;
using GreenSphere.Application.Features.Auth.Commands.Disable2Fa;
using GreenSphere.Application.Features.Auth.Commands.Enable2Fa;
using GreenSphere.Application.Features.Auth.Commands.ForgotPassword;
using GreenSphere.Application.Features.Auth.Commands.GoogleLogin;
using GreenSphere.Application.Features.Auth.Commands.Login;
using GreenSphere.Application.Features.Auth.Commands.RefreshToken;
using GreenSphere.Application.Features.Auth.Commands.Register;
using GreenSphere.Application.Features.Auth.Commands.RevokeToken;
using GreenSphere.Application.Features.Auth.Commands.SendConfirmEmailCode;
using GreenSphere.Application.Features.Auth.Commands.ValidateToken;
using GreenSphere.Application.Features.Auth.Commands.Verify2FaCode;
using GreenSphere.Application.Helpers;
using GreenSphere.Application.Interfaces.Services;
using GreenSphere.Application.Interfaces.Services.Models;
using GreenSphere.Domain.Entities;
using GreenSphere.Domain.Entities.Identity;
using GreenSphere.Domain.Enumerations;
using GreenSphere.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
namespace GreenSphere.Services.Services;

public sealed class AuthService : BaseResponseHandler, IAuthService
{
    private const string CacheKeyPrefix = "GoogleToken_";
    private readonly IMapper _mapper;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ApplicationDbContext _dbContext;
    private readonly IConfiguration _configuration;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IMailService _mailService;
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly IMemoryCache _memoryCache;
    private readonly ILogger<AuthService> _logger;
    private readonly ITokenService _tokenService;

    public AuthService(
        IMapper mapper,
        UserManager<ApplicationUser> userManager,
        ApplicationDbContext dbContext,
        IConfiguration configuration,
        SignInManager<ApplicationUser> signInManager,
        IMailService mailService,
        IHttpContextAccessor contextAccessor,
        IMemoryCache memoryCache,
        ILogger<AuthService> logger,
        ITokenService tokenService,
        IStringLocalizer<BaseResponseHandler> localizer) : base(localizer)
    {
        _mapper = mapper;
        _userManager = userManager;
        _dbContext = dbContext;
        _configuration = configuration;
        _signInManager = signInManager;
        _mailService = mailService;
        _contextAccessor = contextAccessor;
        _memoryCache = memoryCache;
        _logger = logger;
        _tokenService = tokenService;
    }

    public async Task<Result<string>> ConfirmEmailAsync(ConfirmEmailCommand command)
    {
        var validator = new ConfirmEmailCommandValidator();
        await validator.ValidateAndThrowAsync(command);

        var transaction = await _dbContext.Database.BeginTransactionAsync();
        try
        {
            var user = await _userManager.FindByEmailAsync(command.Email);

            if (user is null)
                return NotFound<string>(Localizer["UnknownUser"]);

            var decodedAuthenticationCode = Encoding.UTF8.GetString(Convert.FromBase64String(user.Code!));

            if (decodedAuthenticationCode != command.Token)
                return BadRequest<string>(Localizer["InvalidAuthCode"]);

            if (DateTimeOffset.Now > user.CodeExpiration)
                return BadRequest<string>(Localizer["AuthCodeExpired"]);

            user.EmailConfirmed = true;
            var identityResult = await _userManager.UpdateAsync(user);

            if (!identityResult.Succeeded)
            {
                var errors = identityResult.Errors
                    .Select(e => e.Description)
                    .ToList();

                return BadRequest<string>(Localizer["UnableToUpdateUser"], errors);
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

            await _mailService.SendEmailAsync(emailMessage);

            await transaction.CommitAsync();
            return Success<string>(Localizer["EmailConfirmed"]);

        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            return BadRequest<string>(ex.Message);
        }
    }

    public async Task<Application.Bases.Result<GoogleUserProfile?>> GoogleLoginAsync(GoogleLoginCommand command)
    {
        var validator = new GoogleLoginCommandValidator();
        await validator.ValidateAndThrowAsync(command);

        GoogleJsonWebSignature.ValidationSettings validationSettings = new()
        {
            Audience = [_configuration["Authentication:Google:ClientId"]]
        };

        var payload = await GoogleJsonWebSignature.ValidateAsync(command.IdToken, validationSettings);

        var cacheKey = $"{CacheKeyPrefix}{payload.Subject}";
        if (_memoryCache.TryGetValue(cacheKey, out GoogleUserProfile? userProfile))
        {
            _logger.LogInformation($"Get GoogleUser Info From Cache: {JsonSerializer.Serialize(userProfile)}");

            return Application.Bases.Result<GoogleUserProfile?>.Success(userProfile);
        }

        var profile = new GoogleUserProfile(
            Email: payload.Email,
            Name: payload.Name,
            Picture: payload.Picture,
            FirstName: payload.GivenName,
            LastName: payload.FamilyName,
            GoogleId: payload.Subject,
            Locale: payload.Locale,
            EmailVerified: payload.EmailVerified,
            HostedDomain: payload.HostedDomain,
            Expires: TimeSpan.FromSeconds(Convert.ToDouble(payload.ExpirationTimeSeconds)));

        var cacheOptions = new MemoryCacheEntryOptions()
            .SetAbsoluteExpiration(TimeSpan.FromMinutes(30));

        _memoryCache.Set(cacheKey, profile, cacheOptions);

        var existingUser = await _userManager.FindByEmailAsync(payload.Email);

        if (existingUser != null)
        {
            return Application.Bases.Result<GoogleUserProfile?>.Success(profile);
        }

        existingUser = new ApplicationUser()
        {
            Email = payload.Email,
            FirstName = payload.GivenName,
            LastName = payload.FamilyName,
            EmailConfirmed = payload.EmailVerified,
            UserName = payload.Email
        };

        var createResult = await _userManager.CreateAsync(existingUser);

        if (!createResult.Succeeded)
            return Application.Bases.Result<GoogleUserProfile?>.Failure(
                statusCode: HttpStatusCode.UnprocessableEntity,
                message: "One or more errors happened",
                errors: [.. createResult.Errors.Select(e => $"{e.Code}: {e.Description}")]);

        var loginResult = await _userManager.AddLoginAsync(existingUser,
            new UserLoginInfo("Google", existingUser.Email, existingUser.FirstName));

        if (loginResult.Succeeded)
        {
            return Application.Bases.Result<GoogleUserProfile?>.Success(profile);
        }

        return Application.Bases.Result<GoogleUserProfile?>.Failure(
            statusCode: HttpStatusCode.UnprocessableEntity,
            message: "One or more errors happened when tring to login !!!",
            errors: [.. loginResult.Errors.Select(e => $"{e.Code} : {e.Description}")]);
    }

    public async Task<Result<SignInResponseDto>> LoginAsync(LoginCommand command)
    {
        var validator = new LoginCommandValidator();
        await validator.ValidateAsync(command);

        var loggedInUser = await _userManager.FindByEmailAsync(command.Email);

        if (loggedInUser is null)
            return NotFound<SignInResponseDto>(Localizer["UnknownUser"]);

        if (!await _userManager.IsEmailConfirmedAsync(loggedInUser))
            return BadRequest<SignInResponseDto>(Localizer["EmailNotConfirmed"]);

        if (await _userManager.GetTwoFactorEnabledAsync(loggedInUser))
        {
            // if two factor is enabled send code to user
            var twoFactorCode = await _userManager.GenerateTwoFactorTokenAsync(loggedInUser, "Email");

            await _signInManager.TwoFactorSignInAsync("Email", twoFactorCode, false, true);

            _contextAccessor.HttpContext!.Response.Cookies.Append(
                "userEmail",
                Convert.ToBase64String(Encoding.UTF8.GetBytes(loggedInUser.Email!)),
                new CookieOptions
                {
                    HttpOnly = true,
                    SameSite = SameSiteMode.Strict
                });

            await _mailService.SendEmailAsync(new MailkitEmail
            {
                To = command.Email,
                Provider = "gmail",
                Subject = "2FA Code Required",
                Body = $"2FA code is required to complete login process, Your 2FA code is {twoFactorCode}"
            });

            return BadRequest<SignInResponseDto>(Localizer["TwoFactorRequired"]);
        }

        // check account is locked
        if (await _userManager.IsLockedOutAsync(loggedInUser))
        {
            return Unauthorized<SignInResponseDto>(Localizer["AccountLocked", loggedInUser.LockoutEnd!.Value.ToLocalTime()]);
        }

        var result = await _signInManager.PasswordSignInAsync(
            user: loggedInUser,
            password: command.Password,
            isPersistent: true,
            lockoutOnFailure: true);

        if (result.IsLockedOut)
        {
            TimeZoneInfo timeZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");

            var lockoutEndTime = TimeZoneInfo.ConvertTimeFromUtc(loggedInUser.LockoutEnd!.Value.UtcDateTime, timeZone);

            return Unauthorized<SignInResponseDto>(Localizer["AccountLocked", lockoutEndTime]);
        }

        if (result.Succeeded)
        {
            SignInResponseDto response = await CreateLoginResponseAsync(_userManager, loggedInUser);
            return Success(response);
        }

        return Unauthorized<SignInResponseDto>(Localizer["InvalidCredentials"]);
    }

    private async Task<SignInResponseDto> CreateLoginResponseAsync(
        UserManager<ApplicationUser> userManager,
        ApplicationUser loggedInUser)
    {
        var token = await _tokenService.GenerateJwtTokenAsync(loggedInUser);
        var userRoles = await userManager.GetRolesAsync(loggedInUser);
        var response = new SignInResponseDto()
        {
            Email = loggedInUser.Email!,
            UserName = loggedInUser.UserName!,
            IsAuthenticated = true,
            Roles = [.. userRoles],
            Token = token,
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
        await _userManager.UpdateAsync(appUser);
        return Application.Bases.Result<bool>.Success(true);
    }


    private async Task<Application.Bases.Result<SignInResponseDto>> CreateSignInResponse(
        (ApplicationUser appUser, string jwtToken) appUserWithJwt)
    {
        var userRoles = await _userManager.GetRolesAsync(appUserWithJwt.appUser);
        var newRefreshToken = appUserWithJwt.appUser.RefreshTokens?.FirstOrDefault(x => x.IsActive);

        var response = new SignInResponseDto
        {
            IsAuthenticated = true,
            UserName = appUserWithJwt.appUser.UserName!,
            Email = appUserWithJwt.appUser.Email!,
            Token = appUserWithJwt.jwtToken,
            Roles = [.. userRoles],
            RefreshToken = newRefreshToken?.Token,
            RefreshTokenExpiration = newRefreshToken!.ExpiresOn
        };

        return Application.Bases.Result<SignInResponseDto>.Success(response);
    }

    private async Task<Application.Bases.Result<(ApplicationUser appUser, string jwtToken)>>
        GenerateNewJwtToken(ApplicationUser appUser)
    {
        var jwtToken = await _tokenService.GenerateJwtTokenAsync(appUser);
        return Application.Bases.Result<(ApplicationUser appUser, string jwtToken)>
            .Success((appUser, jwtToken));
    }

    private async Task<Application.Bases.Result<ApplicationUser>> GenerateNewRefreshToken(ApplicationUser appUser)
    {
        var newRefreshToken = GenerateRefreshToken();
        appUser.RefreshTokens?.Add(newRefreshToken);
        await _userManager.UpdateAsync(appUser);
        return Application.Bases.Result<ApplicationUser>.Success(appUser);
    }

    private Application.Bases.Result<ApplicationUser> RevokeUserTokenAndReturnsAppUser(RefreshToken userRefreshToken)
    {
        userRefreshToken.RevokedOn = DateTimeOffset.Now;

        var user = _userManager.Users.SingleOrDefault(u =>
            u.RefreshTokens != null && u.RefreshTokens.Any(r => r.Token == userRefreshToken.Token));

        return Application.Bases.Result<ApplicationUser>.Success(user!);
    }

    private Application.Bases.Result<RefreshToken> CheckIfTokenIsActive(RefreshToken userRefreshToken)
    {
        return !userRefreshToken.IsActive ? Application.Bases.Result<RefreshToken>.Failure(HttpStatusCode.BadRequest, Localizer["InactiveToken"]) : Application.Bases.Result<RefreshToken>.Success(userRefreshToken);
    }

    private Application.Bases.Result<RefreshToken> SelectRefreshTokenAssignedToUser(ApplicationUser user,
        string token)
    {
        var refreshToken = user.RefreshTokens?.Single(x => x.Token == token);

        return refreshToken is not null ? Application.Bases.Result<RefreshToken>.Success(refreshToken) : Application.Bases.Result<RefreshToken>.Failure(HttpStatusCode.NotFound, Localizer["TokenNotFound"]);
    }

    public async Task LogoutAsync() => await _signInManager.SignOutAsync();

    public async Task<Result<SignUpResponseDto>> RegisterAsync(RegisterCommand command)
    {
        var registerCommandValidator = new RegisterCommandValidator();
        await registerCommandValidator.ValidateAndThrowAsync(command);

        var user = _mapper.Map<ApplicationUser>(command);
        user.PointsHistory.Add(
            new UserPoints
            {
                Id = Guid.NewGuid(),
                ActivityType = ActivityType.Gift,
                EarnedDate = DateTimeOffset.Now,
                Points = 100,
                IsSpent = false,
                UserId = user.Id
            });

        var createResult = await _userManager.CreateAsync(user, command.Password);

        if (!createResult.Succeeded)
        {
            var creationErrors = createResult.Errors.Select(e => e.Description).ToList();
            return BadRequest<SignUpResponseDto>(Localizer["UnableToCreateAccount"], creationErrors);
        }

        await _userManager.AddToRoleAsync(user, Constants.Roles.User);



        return Success(SignUpResponseDto.ToResponse(Guid.Parse(user.Id)));
    }

    public async Task<Result<SendCodeConfirmEmailResponseDto>> SendConfirmEmailCodeAsync(
        SendConfirmEmailCodeCommand command)
    {
        var confirmEmailValidator = new SendConfirmEmailCodeCommandValidator();
        await confirmEmailValidator.ValidateAndThrowAsync(command);

        var transaction = await _dbContext.Database.BeginTransactionAsync();

        try
        {
            var user = await _userManager.FindByEmailAsync(command.Email);

            if (user is null)
                return NotFound<SendCodeConfirmEmailResponseDto>(Localizer["UnknownUser"]);

            if (await _userManager.IsEmailConfirmedAsync(user))
                return Conflict<SendCodeConfirmEmailResponseDto>(Localizer["AlreadyEmailConfirmed"]);

            var authenticationCode = await _userManager.GenerateUserTokenAsync(user, "Email", "Confirm User Email");
            var encodedAuthenticationCode = Convert.ToBase64String(Encoding.UTF8.GetBytes(authenticationCode));

            user.Code = encodedAuthenticationCode;
            user.CodeExpiration = DateTimeOffset.Now.AddMinutes(
                minutes: Convert.ToDouble(_configuration[Constants.AuthCodeExpireKey]!)
            );

            var identityResult = await _userManager.UpdateAsync(user);

            if (!identityResult.Succeeded)
            {
                var errors = identityResult.Errors
                    .Select(e => e.Description)
                    .ToList();

                return BadRequest<SendCodeConfirmEmailResponseDto>(Localizer["UnableToUpdateUser"], errors);
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
                           This code will expire in <strong>{_configuration[Constants.AuthCodeExpireKey]} minutes</strong>.
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

            await _mailService.SendEmailAsync(emailMessage);

            await transaction.CommitAsync();
            return Success(
                entity: SendCodeConfirmEmailResponseDto.ToResponse(user.CodeExpiration.Value),
                message: Localizer["ConfirmEmailCodeSentSuccessfully"]
            );
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            return BadRequest<SendCodeConfirmEmailResponseDto>(ex.Message);
        }
    }

    private static RefreshToken GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return new RefreshToken()
        {
            Token = Convert.ToBase64String(randomNumber),
            ExpiresOn = DateTimeOffset.Now.AddDays(10),
            CreatedOn = DateTimeOffset.Now
        };
    }

    private async Task<Application.Bases.Result<ApplicationUser>> CheckIfUserHasAssignedToRefreshToken(
        string refreshToken)
    {
        var user = await _userManager.Users.SingleOrDefaultAsync(x =>
            x.RefreshTokens != null && x.RefreshTokens.Any(token => token.Token == refreshToken));
        return user is null
            ? Application.Bases.Result<ApplicationUser>.Failure(HttpStatusCode.NotFound, Localizer["InvalidToken"])
            : Application.Bases.Result<ApplicationUser>.Success(user);
    }


    public async Task<Result<string>> ForgotPasswordAsync(ForgotPasswordCommand command)
    {
        var validator = new ForgotPasswordCommandValidator();
        await validator.ValidateAsync(command);

        var user = await _userManager.FindByEmailAsync(command.Email);
        if (user == null)
            return NotFound<string>(Localizer["UnknownUser"]);

        var decoded = await _userManager.GenerateUserTokenAsync(user, "Email", "Generate Code");
        var authCode = Convert.ToBase64String(Encoding.UTF8.GetBytes(decoded));
        user.Code = authCode;
        user.CodeExpiration =
            DateTimeOffset.Now.AddMinutes(Convert.ToDouble(_configuration["AuthCodeExpirationInMinutes"]));

        var updateResult = await _userManager.UpdateAsync(user);
        if (!updateResult.Succeeded)
            return BadRequest<string>(Localizer["UnableToUpdateUser"]);

        var emailMessage = new MailkitEmail
        {
            Provider = command.Provider,
            To = command.Email,
            Subject = "Reset Password Code",
            Body = $@"
                   <div style='margin-top: 20px; font-size: 16px; color: #333;'>
                       <p>Hello,</p>
                       <p>Your password reset code is: <strong>{decoded}</strong>. This code will expire in {_configuration["AuthCodeExpirationInMinutes"]} minutes.</p>
                       <p style='font-size: 14px; color: #555;'>If you did not request a password reset, please ignore this email.</p>
                       <p>Best regards,<br>
                          <strong>Green Sphere</strong>
                       </p>
                   </div>"
        };

        await _mailService.SendEmailAsync(emailMessage);

        return Success<string>(Localizer["PasswordResetCodeSent"]);
    }

    public async Task<Result<string>> ConfirmForgotPasswordCodeAsync(ConfirmForgotPasswordCodeCommand command)
    {

        var validator = new ConfirmForgotPasswordCodeCommandValidator();

        await validator.ValidateAndThrowAsync(command);

        var user = await _userManager.FindByEmailAsync(command.Email);
        if (user == null)
            return NotFound<string>(Localizer["UserNotFound", command.Email]);

        var decodedAuthCode = Encoding.UTF8.GetString(Convert.FromBase64String(user.Code!));
        if (decodedAuthCode != command.Code)
            return BadRequest<string>(Localizer["InvalidAuthCode"]);

        if (DateTimeOffset.Now > user.CodeExpiration)
            return BadRequest<string>(Localizer["CodeExpired"]);

        await _userManager.RemovePasswordAsync(user);
        var result = await _userManager.AddPasswordAsync(user, command.NewPassword);

        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(x => x.Description).FirstOrDefault();
            return UnprocessableEntity<string>(errors!);
        }

        return Success<string>(Localizer["PasswordResetSuccessfully"]);
    }

    public async Task<Result<string>> Enable2FaAsync(Enable2FaCommand command)
    {
        var validator = new Enable2FaCommandValidator();
        await validator.ValidateAsync(command);

        var user = await _userManager.FindByEmailAsync(command.Email);

        if (user == null)
            return BadRequest<string>(Localizer["UserNotFound", command.Email]);

        var code = await _userManager.GenerateTwoFactorTokenAsync(user, command.TokenProvider.ToString());

        await _signInManager.TwoFactorSignInAsync(code, command.TokenProvider.ToString(), false, true);

        if (command.TokenProvider == TokenProvider.Email)
            // send code via user email
            await _mailService.SendEmailAsync(new MailkitEmail
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

        return Success<string>(Localizer["TwoFactorCodeSent"]);
    }

    public async Task<Result<string>> ConfirmEnable2FaAsync(ConfirmEnable2FaCommand command)
    {
        var validator = new ConfirmEnable2FaCommandValidator();
        await validator.ValidateAndThrowAsync(command);

        var user = await _userManager.FindByEmailAsync(command.Email);
        if (user == null)
            return NotFound<string>(Localizer["UnkownUser"]);

        // verify 2fa code
        var providers = await _userManager.GetValidTwoFactorProvidersAsync(user);

        if (providers.Contains(TokenProvider.Email.ToString()))
        {
            var verified =
                await _userManager.VerifyTwoFactorTokenAsync(user, TokenProvider.Email.ToString(), command.Code);

            if (!verified)
                return BadRequest<string>(Localizer["Invalid2FaCode"]);

            // code is verified update status of 2FA

            await _userManager.SetTwoFactorEnabledAsync(user, true);

            await _mailService.SendEmailAsync(new MailkitEmail
            {
                Provider = "gmail",
                Subject = "Setup 2FA complete",
                To = user.Email!,
                Body = "Your 2FA authentication is successfully enabled."
            });

            return Success<string>(Localizer["TwoFactorEnabled"]);
        }

        return BadRequest<string>(Localizer["InvalidTokenProvider"]);
    }

    public async Task<Result<SignInResponseDto>> Verify2FaCodeAsync(Verify2FaCodeCommand command)
    {
        var validator = new Verify2FaCodeCommandValidator();
        await validator.ValidateAsync(command);

        var userEmail =
            Encoding.UTF8.GetString(
                Convert.FromBase64String(_contextAccessor.HttpContext!.Request.Cookies["userEmail"]!));
        var appUser = await _userManager.FindByEmailAsync(userEmail);

        if (appUser == null) return Unauthorized<SignInResponseDto>();

        var verified = await _userManager.VerifyTwoFactorTokenAsync(appUser, "Email", command.Code);

        if (!verified)
            return BadRequest<SignInResponseDto>(DomainErrors.User.Invalid2FaCode);

        await _signInManager.SignInAsync(appUser, isPersistent: true);

        SignInResponseDto response = await CreateLoginResponseAsync(_userManager, appUser);

        return Success(response);
    }

    public async Task<Result<string>> Disable2FaAsync(Disable2FaCommand command)
    {

        var validator = new Disable2FaCommandValidator();
        await validator.ValidateAndThrowAsync(command);


        var user = await _userManager.FindByEmailAsync(command.Email);

        if (user == null)
            return NotFound<string>(Localizer["UserNotFound", command.Email]);

        if (!await _userManager.GetTwoFactorEnabledAsync(user))
            return BadRequest<string>(Localizer["TwoFactorAlreadyDisabled"]);

        var result = await _userManager.SetTwoFactorEnabledAsync(user, false);

        if (!result.Succeeded)
            return BadRequest<string>(Localizer["Disable2FaFailed"]);

        await _mailService.SendEmailAsync(new MailkitEmail
        {
            Provider = "gmail",
            To = user.Email!,
            Subject = "2FA Disabled",
            Body = "Your two-factor authentication has been successfully disabled."
        });

        return Success<string>(Localizer["Disable2FaSuccess"]);
    }

    public async Task<Application.Bases.Result<ValidateTokenResponseDto>> ValidateTokenAsync(ValidateTokenCommand command)
    {
        var validator = new ValidateTokenCommandValidator();
        await validator.ValidateAsync(command);


        if (string.IsNullOrWhiteSpace(command.JwtToken))
            return Application.Bases.Result<ValidateTokenResponseDto>.Failure(
                HttpStatusCode.BadRequest, Localizer["TokenCanNotBeNullOrEmpty"]);

        var claimsPrincipal = await _tokenService.ValidateToken(command.JwtToken);

        var response = new ValidateTokenResponseDto();
        foreach (var claim in claimsPrincipal.Claims)
            response.Claims.Add(new ClaimsResponse() { ClaimType = claim.Type, ClaimValue = claim.Value });

        return Application.Bases.Result<ValidateTokenResponseDto>.Success(response, Localizer["InvalidToken"]);
    }
}