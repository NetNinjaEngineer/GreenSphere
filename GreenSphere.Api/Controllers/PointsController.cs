using Asp.Versioning;
using GreenSphere.Api.Controllers.Base;
using GreenSphere.Application.Attributes;
using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Points;
using GreenSphere.Application.Features.Points.Commands.AddPoints;
using GreenSphere.Application.Features.Points.Commands.SpendPoints;
using GreenSphere.Application.Features.Points.Queries.GetAvailablePoints;
using GreenSphere.Application.Features.Points.Queries.GetPointsHistory;
using GreenSphere.Application.Features.Points.Queries.GetPointsSummary;
using GreenSphere.Application.Helpers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GreenSphere.Api.Controllers;
[ApiVersion(1.0)]
[Route("api/v{version:apiVersion}/points")]
public class PointsController(IMediator mediator) : BaseApiController(mediator)
{
    [Guard(roles: [Constants.Roles.User])]
    [HttpGet("summary")]
    [ProducesResponseType(typeof(Result<PointsSummaryDto?>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPointsSummary()
        => CustomResult(await Mediator.Send(new GetPointsSummaryQuery()));

    [Guard(roles: [Constants.Roles.User])]
    [HttpGet("history")]
    [ProducesResponseType(typeof(Result<IEnumerable<PointsDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPointsHistory()
        => CustomResult(await Mediator.Send(new GetPointsHistoryQuery()));

    [HttpPost("add")]
    [Guard(roles: [Constants.Roles.Admin])]
    [ProducesResponseType(typeof(Result<PointsDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> AddPoints([FromBody] AddPointsCommand command)
        => CustomResult(await Mediator.Send(command));

    [Guard(roles: [Constants.Roles.User])]
    [HttpGet("available")]
    [ProducesResponseType(typeof(Result<long>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAvailablePoints()
        => CustomResult(await Mediator.Send(new GetAvailablePointsQuery()));


    [Guard(roles: [Constants.Roles.User])]
    [HttpPost("spend")]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> SpendPointsAsync([FromBody] SpendPointsCommand command)
        => CustomResult(await Mediator.Send(command));

}
