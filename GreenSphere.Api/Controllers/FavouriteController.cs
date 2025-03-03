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
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddItemToFavourite([FromBody] AddItemToFavouriteCommand command)
    {
        var result = await mediator.Send(command);
        if (result.IsSuccess)
        {
            return Ok(new { Value = result.Value, Message = result.Message });
        }
        return BadRequest(new { Errors = result.Errors, Message = result.Message });
    }


    [HttpDelete("remove_item")]
    [ProducesResponseType(typeof(Result<FavouriteDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RemoveItemFromFavourite([FromBody] RemoveItemFromFavouriteCommand command)
    {
        var result = await mediator.Send(command);
        if (result.IsSuccess)
        {
            return Ok(new { Value = result.Value, Message = result.Message });
        }
        return BadRequest(new { Errors = result.Errors, Message = result.Message });
    }

    [HttpDelete("clear")]
    [ProducesResponseType(typeof(Result<FavouriteDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteFavourite()
    {
        var result = await mediator.Send(new ClearFavouriteCommand());
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Errors);
    }

    [HttpDelete("delete-all")]
    [ProducesResponseType(typeof(Result<FavouriteDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteAllFavourite()
    {
        var result = await mediator.Send(new DeleteAllFavouriteCommand());
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Errors);
    }
}
