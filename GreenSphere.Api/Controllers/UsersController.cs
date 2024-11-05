using Asp.Versioning;
using GreenSphere.Api.Controllers.Base;
using GreenSphere.Application.Bases;
using GreenSphere.Application.Features.Users.DTOs;
using GreenSphere.Application.Features.Users.Requests.Commands;
using GreenSphere.Application.Features.Users.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GreenSphere.Api.Controllers;
[ApiVersion(1.0)]
[Route("api/v{ver:apiVersion}/users")]
[ApiController]
public class UsersController(IMediator mediator) : BaseApiController(mediator)
{
    [HttpPost("{userId}/privacy-settings/assign")]
    [ProducesResponseType(typeof(Result<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<string>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Result<string>), StatusCodes.Status409Conflict)]
    public async Task<ActionResult<Result<string>>> AssignPrivacySettingAsync(string userId,
        AssignPrivacySettingsRequestDto privacySettingRequest)
        => CustomResult(await _mediator.Send(new AssignUserPrivacyCommand { UserId = userId, AssignPrivacySettingsRequest = privacySettingRequest }));

    [HttpGet("{userId}/privacy-settings")]
    [ProducesResponseType(typeof(Result<PrivacySettingListDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<PrivacySettingListDto>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Result<PrivacySettingListDto>>> GetUserPrivacySettingsAsync(string userId)
        => CustomResult(await _mediator.Send(new GetUserPrivacySettingQuery { UserId = userId }));
}
