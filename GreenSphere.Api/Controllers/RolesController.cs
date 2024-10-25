using GreenSphere.Api.Controllers.Base;
using GreenSphere.Application.Abstractions;
using GreenSphere.Application.Features.Roles.Requests.Commands;
using GreenSphere.Application.Features.Roles.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GreenSphere.Api.Controllers;
[Route("api/v{ver:apiVersion}/roles")]
[ApiController]
public class RolesController(IMediator mediator) : BaseApiController(mediator)
{
    [HttpPost("addClaimToRole")]
    [ProducesResponseType(typeof(SuccessResult<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(FailedResult<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(FailedResult<string>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Result<string>>> AddClaimToRoleAsync(AddClaimToRoleCommand command)
        => CustomResult(await _mediator.Send(command));

    [HttpPost("assignClaimToUser")]
    [ProducesResponseType(typeof(SuccessResult<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(FailedResult<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(FailedResult<string>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Result<string>>> AssignClaimToUserAsync(AssignClaimToUserCommand command)
        => CustomResult(await _mediator.Send(command));

    [HttpPost("assignRoleToUser")]
    [ProducesResponseType(typeof(SuccessResult<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(FailedResult<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(FailedResult<string>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Result<string>>> AssignRoleToUserAsync(AssignRoleToUserCommand command)
        => CustomResult(await _mediator.Send(command));

    [HttpPost("createRole")]
    [ProducesResponseType(typeof(SuccessResult<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(FailedResult<string>), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Result<string>>> CreateRoleAsync(CreateRoleCommand command)
        => CustomResult(await _mediator.Send(command));

    [HttpPost("deleteRole")]
    [ProducesResponseType(typeof(SuccessResult<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(FailedResult<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(FailedResult<string>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Result<string>>> DeleteRoleAsync(DeleteRoleCommand command)
        => CustomResult(await _mediator.Send(command));

    [HttpPost("editRole")]
    [ProducesResponseType(typeof(SuccessResult<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(FailedResult<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(FailedResult<string>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Result<string>>> EditRoleAsync(EditRoleCommand command)
        => CustomResult(await _mediator.Send(command));

    [HttpGet("getAllRoles")]
    [ProducesResponseType(typeof(SuccessResult<IEnumerable<string>>), StatusCodes.Status200OK)]
    public async Task<ActionResult<Result<IEnumerable<string>>>> GetAllRolesAsync()
        => CustomResult(await _mediator.Send(new GetAllRolesQuery()));

    [HttpGet("getRoleClaims")]
    [ProducesResponseType(typeof(SuccessResult<IEnumerable<string>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(FailedResult<IEnumerable<string>>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Result<IEnumerable<string>>>> GetRoleClaimsAsync(GetRoleClaimsQuery query)
        => CustomResult(await _mediator.Send(query));

    [HttpGet("getUserClaims")]
    [ProducesResponseType(typeof(SuccessResult<IEnumerable<string>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(FailedResult<IEnumerable<string>>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Result<IEnumerable<string>>>> GetUserClaimsAsync(GetUserClaimsQuery query)
        => CustomResult(await _mediator.Send(query));

    [HttpGet("getUserRoles")]
    [ProducesResponseType(typeof(SuccessResult<IEnumerable<string>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(FailedResult<IEnumerable<string>>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Result<IEnumerable<string>>>> GetUserRolesAsync(GetUserRolesQuery query)
        => CustomResult(await _mediator.Send(query));
}
