using Asp.Versioning;
using GreenSphere.Api.Controllers.Base;
using GreenSphere.Application.Attributes;
using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Basket;
using GreenSphere.Application.Features.Basket.Commands.AddItemToBasket;
using GreenSphere.Application.Features.Basket.Commands.ClearBasket;
using GreenSphere.Application.Features.Basket.Commands.DeleteBasket;
using GreenSphere.Application.Features.Basket.Commands.RemoveItemFromBasket;
using GreenSphere.Application.Features.Basket.Commands.UpdateItemQuantity;
using GreenSphere.Application.Features.Basket.Queries.GetBasket;
using GreenSphere.Application.Features.Basket.Queries.GetItemsCount;
using GreenSphere.Application.Helpers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GreenSphere.Api.Controllers;
[ApiVersion(1.0)]
[Guard(roles: [Constants.Roles.User])]
[Route("api/v{apiVersion:apiVersion}/basket")]
public class BasketController(IMediator mediator) : BaseApiController(mediator)
{
    [HttpGet("me")]
    [ProducesResponseType(typeof(Result<BasketDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<Result<BasketDto>>> GetCustomerBasketAsync()
        => CustomResult(await Mediator.Send(new GetBasketQuery()));

    [HttpGet("me/items/count")]
    [ProducesResponseType(typeof(Result<int>), StatusCodes.Status200OK)]
    public async Task<ActionResult<Result<int>>> GetCustomerBasketItemsCountAsync()
        => CustomResult(await Mediator.Send(new GetItemsCountQuery()));

    [HttpPost("me/items")]
    [ProducesResponseType(typeof(Result<BasketDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<BasketDto>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Result<BasketDto>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Result<BasketDto>>> AddItemToBasketAsync(
        [FromBody] AddItemToBasketCommand command)
        => CustomResult(await Mediator.Send(command));

    [HttpDelete("me/items/clear")]
    [ProducesResponseType(typeof(Result<BasketDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<BasketDto>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Result<BasketDto>>> ClearBasketAsync()
        => CustomResult(await Mediator.Send(new ClearBasketCommand()));

    [HttpDelete("me")]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Result<bool>>> DeleteBasketAsync()
        => CustomResult(await Mediator.Send(new DeleteBasketCommand()));


    [HttpDelete("me/items")]
    [ProducesResponseType(typeof(Result<BasketDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<BasketDto>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Result<BasketDto>>> RemoveItemFromBasketAsync(
        [FromBody] RemoveItemFromBasketCommand command)
        => CustomResult(await Mediator.Send(command));


    [HttpPut("me/items/{id:guid}/{quantity:int}")]
    [ProducesResponseType(typeof(Result<BasketDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<BasketDto>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Result<BasketDto>), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Result<BasketDto>>> UpdateItemQuantityBasketAsync(
        [FromRoute] Guid id, [FromRoute] int quantity)
        => CustomResult(await Mediator.Send(new UpdateItemQuantityCommand { BasketItemId = id, Quantity = quantity }));

}
