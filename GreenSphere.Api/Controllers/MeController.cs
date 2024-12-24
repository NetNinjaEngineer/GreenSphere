using Asp.Versioning;
using GreenSphere.Api.Controllers.Base;
using GreenSphere.Application.Attributes;
using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Users;
using GreenSphere.Application.Features.Users.Queries.GetLoggedInUserPrivacySettings;
using GreenSphere.Application.Features.Users.Queries.GetLoggedInUserProfile;
using GreenSphere.Application.Helpers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GreenSphere.Api.Controllers;
[ApiVersion(1.0)]
[Route("api/v{ver:apiVersion}/me")]
public class MeController(IMediator mediator) : BaseApiController(mediator)
{
    [HttpGet("profile")]
    [Guard(roles: [Constants.Roles.User])]
    [ProducesResponseType(typeof(Result<UserProfileDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<UserProfileDto>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<Result<UserProfileDto>>> GetUserProfileAsync()
        => CustomResult(await Mediator.Send(new GetLoggedInUserProfileQuery()));

    [Guard(roles: [Constants.Roles.User])]
    [HttpGet("privacy-settings")]
    [ProducesResponseType(typeof(Result<PrivacySettingListDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<PrivacySettingListDto>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<Result<PrivacySettingListDto>>> GetUserPrivacySettingsAsync()
        => CustomResult(await Mediator.Send(new GetLoggedInUserPrivacySettingsQuery()));
}
