using GreenSphere.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GreenSphere.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ValuesController : ControllerBase
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly ILogger<ValuesController> _logger;

    public ValuesController(SignInManager<ApplicationUser> signInManager, ILogger<ValuesController> logger)
    {
        _signInManager = signInManager;
        _logger = logger;
    }

    [HttpGet("login")]
    public IActionResult Login(string provider, string returnUrl = null)
    {
        // Request a redirect to the external login provider.
        var redirectUrl = Url.Action(nameof(ExternalLoginCallback), "ExternalLogin", new { returnUrl });
        var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
        return new ChallengeResult(provider, properties);
    }

    [HttpGet("callback")]
    public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
    {
        returnUrl = returnUrl ?? Url.Content("~/");

        if (remoteError != null)
        {
            return BadRequest($"Error from external provider: {remoteError}");
        }

        var info = await _signInManager.GetExternalLoginInfoAsync();
        if (info == null)
        {
            return BadRequest("Error loading external login information.");
        }

        // Sign in the user with this external login provider if the user already has a login.
        var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false);
        if (result.Succeeded)
        {
            _logger.LogInformation("{Name} logged in with {LoginProvider} provider.", info.Principal.Identity.Name, info.LoginProvider);
            return Ok(new { returnUrl });
        }
        else
        {
            // Handle the case where the user does not have an account
            var emailClaim = info.Principal.FindFirst(ClaimTypes.Email);
            if (emailClaim != null)
            {
                // Here you might want to create a new user or return a sign-up prompt
                return BadRequest("User does not have an account. Please sign up.");
            }
            return BadRequest("External login failed.");
        }
    }
}
