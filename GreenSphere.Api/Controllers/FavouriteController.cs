using Asp.Versioning;
using GreenSphere.Api.Controllers.Base;
using GreenSphere.Application.Attributes;
using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Favourite;
using GreenSphere.Application.Features.Favourite.Commands.AddItemToFavourite;
using GreenSphere.Application.Features.Favourite.Commands.ClearFavourite;
using GreenSphere.Application.Features.Favourite.Commands.DeleteAllFavourite;
using GreenSphere.Application.Features.Favourite.Commands.RemoveItemFromFavourite;
using GreenSphere.Application.Features.Favourite.Queries.GetFavourite;
using GreenSphere.Application.Helpers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GreenSphere.Api.Controllers;

[ApiVersion(1.0)]
[Guard(roles: [Constants.Roles.User])]
[Route("api/v{apiVersion:apiVersion}/favourite")]

public class FavouriteController(IMediator mediator) : BaseApiController(mediator)
{
    [HttpGet("get")]
    [ProducesResponseType(typeof(Result<FavouriteDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<Result<FavouriteDto>>> GetCustomerFavouriteAsync()
      => CustomResult(await Mediator.Send(new GetFavouriteQuery()));

    [HttpPost("add")]
    [ProducesResponseType(typeof(Result<FavouriteDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<FavouriteDto>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddItemToFavourite([FromBody] AddItemToFavouriteCommand command)
        => CustomResult(await Mediator.Send(command));


    [HttpDelete("remove_item")]
    [ProducesResponseType(typeof(Result<FavouriteDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<FavouriteDto>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RemoveItemFromFavourite([FromBody] RemoveItemFromFavouriteCommand command)
        => CustomResult(await Mediator.Send(command));

    [HttpDelete("clear")]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ClearFavourateAsync()
        => CustomResult(await Mediator.Send(new ClearFavouriteCommand()));

    [HttpDelete("delete-all")]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RemoveCustomerFavourateAsync()
        => CustomResult(await Mediator.Send(new DeleteAllFavouriteCommand()));
}
