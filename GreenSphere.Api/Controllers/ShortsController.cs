using Asp.Versioning;
using GreenSphere.Api.Controllers.Base;
using GreenSphere.Application.Attributes;
using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Shorts;
using GreenSphere.Application.Features.Shorts.Commands.CreateShort;
using GreenSphere.Application.Features.Shorts.Commands.DeleteShort;
using GreenSphere.Application.Features.Shorts.Queries.GetAllShorts;
using GreenSphere.Application.Features.Shorts.Queries.GetShort;
using GreenSphere.Application.Helpers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GreenSphere.Api.Controllers;

[ApiVersion(1.0)]
[Route("api/v{apiVersion:apiVersion}/shorts")]
public class ShortsController(IMediator mediator) : BaseApiController(mediator)
{
    [Guard(roles: [Constants.Roles.Admin])]
    [HttpPost]
    [ProducesResponseType<Result<Guid>>(StatusCodes.Status200OK)]
    [ProducesResponseType<Result<Guid>>(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateAsync([FromForm] CreateShortCommand command)
        => CustomResult(await Mediator.Send(command));

    [HttpGet]
    [ProducesResponseType<Result<IReadOnlyList<ShortDto>>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllAsync()
        => CustomResult(await Mediator.Send(new GetAllShortsQuery()));

    [HttpGet("{id:guid}")]
    [ProducesResponseType<Result<ShortDto>>(StatusCodes.Status200OK)]
    [ProducesResponseType<Result<ShortDto>>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAsync([FromRoute] Guid id)
        => CustomResult(await Mediator.Send(new GetShortQuery { Id = id }));

    [Guard(roles: [Constants.Roles.Admin])]
    [HttpDelete("{id:guid}")]
    [ProducesResponseType<Result<bool>>(StatusCodes.Status200OK)]
    [ProducesResponseType<Result<bool>>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteShortAsync([FromRoute] Guid id)
        => CustomResult(await Mediator.Send(new DeleteShortCommand() { Id = id }));
}