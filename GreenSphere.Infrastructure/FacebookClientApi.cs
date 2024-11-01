using GreenSphere.Application.Bases;
using GreenSphere.Application.Helpers;
using GreenSphere.Application.Interfaces.Identity.Entities;
using GreenSphere.Application.Interfaces.Infrastructure;
using GreenSphere.Application.Interfaces.Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using System.Net;
using System.Text.Json;

namespace GreenSphere.Infrastructure;
public sealed class FacebookClientApi(
    HttpClient client,
    UserManager<ApplicationUser> userManager,
    SignInManager<ApplicationUser> signInManager) : IFacebookClientApi
{
    private readonly JsonSerializerOptions _jsonSerializerOptions = new() { PropertyNameCaseInsensitive = true };
    public async Task<Result<bool>> AuthenticateAsync(string accessToken)
        => (await GetFacebookUserAsync(accessToken))
            .Bind(authenticationResponse => CreateFacebookUser(
                authenticationResponse,
                GenerateUserName(authenticationResponse)))
            .Bind(facebookUser => RegisterToLocalDatabase(facebookUser).Result)
            .Bind(facebookUser => AddUserLogin(facebookUser).Result)
            .Map(userAdded => userAdded);

    private async Task<Result<bool>> AddUserLogin(ApplicationUser facebookUser)
    {
        var loginInfo = new UserLoginInfo("Facebook", facebookUser.Id, "Facebook");
        await userManager.AddLoginAsync(facebookUser, loginInfo);
        await signInManager.SignInAsync(facebookUser, isPersistent: false);
        return Result<bool>.Success(true);
    }

    private async Task<Result<ApplicationUser>> RegisterToLocalDatabase(ApplicationUser facebookUser)
    {
        var createResult = await userManager.CreateAsync(facebookUser);
        return !createResult.Succeeded ?
            Result<ApplicationUser>.Failure(
                HttpStatusCode.BadRequest,
                DomainErrors.User.CannotCreateFBUser,
                createResult.Errors.Select(e => e.Description).ToList()) :
            Result<ApplicationUser>.Success(facebookUser);
    }

    private static Result<ApplicationUser> CreateFacebookUser(FacebookAuthenticationResponseDto authenticationResponse, string generatedUsername)
    {
        return Result<ApplicationUser>.Success(new ApplicationUser
        {
            UserName = generatedUsername,
            FirstName = authenticationResponse.Name.Split(' ')[0],
            LastName = authenticationResponse.Name.Split(' ')[1],
            Email = string.IsNullOrEmpty(authenticationResponse.Email)
                ? $"{authenticationResponse}@example.com"
                : authenticationResponse.Email,
            EmailConfirmed = true
        });
    }

    private static string GenerateUserName(FacebookAuthenticationResponseDto authenticationResponse)
        => string.Concat(authenticationResponse.Name.Split(' ', StringSplitOptions.RemoveEmptyEntries)) + authenticationResponse.Id;

    private async Task<Result<FacebookAuthenticationResponseDto>> GetFacebookUserAsync(string accessToken)
    {
        var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"me?access_token={accessToken}&fields=id,name,picture");
        var response = await client.SendAsync(requestMessage, HttpCompletionOption.ResponseHeadersRead);
        if (!response.IsSuccessStatusCode)
            return Result<FacebookAuthenticationResponseDto>.Failure(
                HttpStatusCode.BadRequest,
                DomainErrors.User.FBFailedAuthentication);

        var responseBody = await response.Content.ReadAsStreamAsync();
        return Result<FacebookAuthenticationResponseDto>.Success(JsonSerializer.Deserialize<FacebookAuthenticationResponseDto>(responseBody, _jsonSerializerOptions)!);
    }
}
