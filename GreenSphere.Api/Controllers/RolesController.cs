﻿using Asp.Versioning;
using GreenSphere.Api.Controllers.Base;
using GreenSphere.Application.Abstractions;
using GreenSphere.Application.Features.Roles.Commands.AddClaimToRole;
using GreenSphere.Application.Features.Roles.Commands.AssignClaimToUser;
using GreenSphere.Application.Features.Roles.Commands.AssignRoleToUser;
using GreenSphere.Application.Features.Roles.Commands.CreateRole;
using GreenSphere.Application.Features.Roles.Commands.DeleteRole;
using GreenSphere.Application.Features.Roles.Commands.EditRole;
using GreenSphere.Application.Features.Roles.Queries.GetAllRoles;
using GreenSphere.Application.Features.Roles.Queries.GetRoleClaims;
using GreenSphere.Application.Features.Roles.Queries.GetUserClaims;
using GreenSphere.Application.Features.Roles.Queries.GetUserRoles;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GreenSphere.Api.Controllers;
[ApiVersion(1.0)]
[Route("api/v{ver:apiVersion}/roles")]
public class RolesController(IMediator mediator) : BaseApiController(mediator)
{
    [HttpPost("addClaimToRole")]
    [ProducesResponseType(typeof(SuccessResult<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(FailedResult<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(FailedResult<string>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Result<string>>> AddClaimToRoleAsync(AddClaimToRoleCommand command)
        => CustomResult(await Mediator.Send(command));

    [HttpPost("assignClaimToUser")]
    [ProducesResponseType(typeof(SuccessResult<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(FailedResult<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(FailedResult<string>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Result<string>>> AssignClaimToUserAsync(AssignClaimToUserCommand command)
        => CustomResult(await Mediator.Send(command));

    [HttpPost("assignRoleToUser")]
    [ProducesResponseType(typeof(SuccessResult<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(FailedResult<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(FailedResult<string>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Result<string>>> AssignRoleToUserAsync(AssignRoleToUserCommand command)
        => CustomResult(await Mediator.Send(command));

    [HttpPost("createRole")]
    [ProducesResponseType(typeof(SuccessResult<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(FailedResult<string>), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Result<string>>> CreateRoleAsync(CreateRoleCommand command)
        => CustomResult(await Mediator.Send(command));

    [HttpPost("deleteRole")]
    [ProducesResponseType(typeof(SuccessResult<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(FailedResult<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(FailedResult<string>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Result<string>>> DeleteRoleAsync(DeleteRoleCommand command)
        => CustomResult(await Mediator.Send(command));

    [HttpPost("editRole")]
    [ProducesResponseType(typeof(SuccessResult<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(FailedResult<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(FailedResult<string>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Result<string>>> EditRoleAsync(EditRoleCommand command)
        => CustomResult(await Mediator.Send(command));

    [HttpGet("getAllRoles")]
    [ProducesResponseType(typeof(SuccessResult<IEnumerable<string>>), StatusCodes.Status200OK)]
    public async Task<ActionResult<Result<IEnumerable<string>>>> GetAllRolesAsync()
        => CustomResult(await Mediator.Send(new GetAllRolesQuery()));

    [HttpGet("getRoleClaims")]
    [ProducesResponseType(typeof(SuccessResult<IEnumerable<string>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(FailedResult<IEnumerable<string>>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Result<IEnumerable<string>>>> GetRoleClaimsAsync(
        [FromQuery] GetRoleClaimsQuery query)
        => CustomResult(await Mediator.Send(query));

    [HttpGet("getUserClaims")]
    [ProducesResponseType(typeof(SuccessResult<IEnumerable<string>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(FailedResult<IEnumerable<string>>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Result<IEnumerable<string>>>> GetUserClaimsAsync(
        [FromQuery] GetUserClaimsQuery query)
        => CustomResult(await Mediator.Send(query));

    [HttpGet("getUserRoles")]
    [ProducesResponseType(typeof(SuccessResult<IEnumerable<string>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(FailedResult<IEnumerable<string>>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Result<IEnumerable<string>>>> GetUserRolesAsync([FromQuery] GetUserRolesQuery query)
        => CustomResult(await Mediator.Send(query));
}