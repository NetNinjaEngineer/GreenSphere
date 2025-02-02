﻿using Asp.Versioning;
using GreenSphere.Api.Controllers.Base;
using GreenSphere.Application.Attributes;
using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Users;
using GreenSphere.Application.Features.Users.Commands.AssignUserPrivacy;
using GreenSphere.Application.Features.Users.Commands.EditUserProfile;
using GreenSphere.Application.Features.Users.Queries.GetUserPrivacySetting;
using GreenSphere.Application.Features.Users.Queries.GetUserProfile;
using GreenSphere.Application.Helpers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GreenSphere.Api.Controllers;
[Guard]
[ApiVersion(1.0)]
[Route("api/v{ver:apiVersion}/users")]
[ApiController]
public class UsersController(IMediator mediator) : BaseApiController(mediator)
{
    [Guard(roles: [Constants.Roles.User])]
    [HttpPost("{userId}/privacy-settings/assign")]
    [ProducesResponseType(typeof(Result<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<string>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Result<string>), StatusCodes.Status409Conflict)]
    public async Task<ActionResult<Result<string>>> AssignPrivacySettingAsync(string userId,
        AssignPrivacySettingsRequestDto privacySettingRequest)
        => CustomResult(await Mediator.Send(new AssignUserPrivacyCommand
        { UserId = userId, AssignPrivacySettingsRequest = privacySettingRequest }));

    [Guard(roles: [Constants.Roles.User, Constants.Roles.Admin])]
    [HttpGet("{userId}/privacy-settings")]
    [ProducesResponseType(typeof(Result<PrivacySettingListDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<PrivacySettingListDto>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Result<PrivacySettingListDto>>> GetUserPrivacySettingsAsync(string userId)
        => CustomResult(await Mediator.Send(new GetUserPrivacySettingQuery { UserId = userId }));

    [Guard(policies: [Constants.User.CanViewProfilePolicy])]
    [HttpGet("{userId}/profile")]
    [ProducesResponseType(typeof(Result<UserProfileDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<UserProfileDto>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<Result<PrivacySettingListDto>>> GetUserProfileAsync(string userId)
        => CustomResult(await Mediator.Send(new GetUserProfileQuery { UserId = userId }));

    [Guard]
    [HttpPost("edit-profile")]
    [ProducesResponseType(typeof(Result<UserProfileDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<UserProfileDto>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<Result<UserProfileDto>>> EditUserProfileAsync(
           [FromForm] EditUserProfileCommand request)
    {

        var result = await Mediator.Send(request);

        return CustomResult(result);
    }
}