using GreenSphere.Application.Abstractions;
using GreenSphere.Application.Helpers;
using GreenSphere.Application.Interfaces.Identity.Entities;
using GreenSphere.Application.Interfaces.Infrastructure;
using GreenSphere.Application.Interfaces.Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using System.Text.Json;

namespace GreenSphere.Infrastructure;
public sealed class FacebookClientApi(
    HttpClient client,
    UserManager<ApplicationUser> userManager,
    SignInManager<ApplicationUser> signInManager) : BaseResponseHandler, IFacebookClientApi
{
    private readonly JsonSerializerOptions _jsonSerializerOptions = new() { PropertyNameCaseInsensitive = true };
    public async Task<Result<bool>> AuthenticateAsync(string accessToken)
    {
        var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"me?access_token={accessToken}&fields=id,name,picture");
        var response = await client.SendAsync(requestMessage, HttpCompletionOption.ResponseHeadersRead);
        if (!response.IsSuccessStatusCode)
            return BadRequest<bool>(DomainErrors.User.FBFailedAuthentication);

        var responseBody = await response.Content.ReadAsStringAsync();
        var authenticationResponse = JsonSerializer.Deserialize<FacebookAuthenticationResponseDto>(
            responseBody,
            _jsonSerializerOptions);

        if (authenticationResponse != null)
        {
            var userName = string.Concat(string.Join("", authenticationResponse.Name.Split(' ')), authenticationResponse.Id);
            var facebookUser = new ApplicationUser
            {
                UserName = userName,
                FirstName = authenticationResponse.Name.Split(' ')[0],
                LastName = authenticationResponse.Name.Split(' ')[1],
                Email = string.IsNullOrEmpty(authenticationResponse.Email) ? $"{userName}@example.com" : authenticationResponse.Email,
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(facebookUser);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                return BadRequest<bool>(DomainErrors.User.CannotCreateFBUser, errors);
            }

            var loginInfo = new UserLoginInfo("Facebook", facebookUser.Id, "Facebook");
            await userManager.AddLoginAsync(facebookUser, loginInfo);

            await signInManager.SignInAsync(facebookUser, false);

            return Success(true);
        }

        return BadRequest<bool>(DomainErrors.User.FBFailedAuthentication);
    }
}
