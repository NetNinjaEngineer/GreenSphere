using Asp.Versioning;
using GreenSphere.Api.Controllers.Base;
using GreenSphere.Application.Bases;
using GreenSphere.Application.Features.Users.DTOs;
using GreenSphere.Application.Features.Users.Requests.Commands;
using GreenSphere.Application.Features.Users.Requests.Queries;
using GreenSphere.Application.Helpers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GreenSphere.Api.Controllers;
[Authorize]
[ApiVersion(1.0)]
[Route("api/v{ver:apiVersion}/users")]
[ApiController]
public class UsersController(IMediator mediator) : BaseApiController(mediator)
{
    [Authorize(Roles = Constants.Roles.User)]
    [HttpPost("{userId}/privacy-settings/assign")]
    [ProducesResponseType(typeof(Result<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<string>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Result<string>), StatusCodes.Status409Conflict)]
    public async Task<ActionResult<Result<string>>> AssignPrivacySettingAsync(string userId,
        AssignPrivacySettingsRequestDto privacySettingRequest)
        => CustomResult(await _mediator.Send(new AssignUserPrivacyCommand { UserId = userId, AssignPrivacySettingsRequest = privacySettingRequest }));

    [Authorize(Roles = $"{Constants.Roles.User}, {Constants.Roles.Admin}")]
    [HttpGet("{userId}/privacy-settings")]
    [ProducesResponseType(typeof(Result<PrivacySettingListDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<PrivacySettingListDto>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Result<PrivacySettingListDto>>> GetUserPrivacySettingsAsync(string userId)
        => CustomResult(await _mediator.Send(new GetUserPrivacySettingQuery { UserId = userId }));

    [Authorize(Policy = Constants.User.CanViewProfilePolicy)]
    [HttpGet("{userId}/profile")]
    [ProducesResponseType(typeof(Result<UserProfileDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<UserProfileDto>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<Result<PrivacySettingListDto>>> GetUserProfileAsync(string userId)
       => CustomResult(await _mediator.Send(new GetUserProfileQuery { UserId = userId }));

    [HttpGet("loggedInUser/profile")]
    [ProducesResponseType(typeof(Result<UserProfileDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<UserProfileDto>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<Result<UserProfileDto>>> GetUserProfileAsync()
    => CustomResult(await _mediator.Send(new GetLoggedInUserProfileQuery()));

    [HttpGet("loggedInUser/privacy-settings")]
    [ProducesResponseType(typeof(Result<PrivacySettingListDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<PrivacySettingListDto>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<Result<PrivacySettingListDto>>> GetUserPrivacySettingsAsync()
        => CustomResult(await _mediator.Send(new GetLoggedInUserPrivacySettingsQuery()));
}
