using Asp.Versioning;
using GreenSphere.Api.Controllers.Base;
using GreenSphere.Application.Attributes;
using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Order;
using GreenSphere.Application.Features.Orders.Commands.CreateCashOrder;
using GreenSphere.Application.Features.Orders.Commands.CreateOnlineOrder;
using GreenSphere.Application.Features.Orders.Queries.GetAll;
using GreenSphere.Application.Features.Orders.Queries.GetUserOrder;
using GreenSphere.Application.Features.Orders.Queries.GetUserOrders;
using GreenSphere.Application.Helpers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GreenSphere.Api.Controllers;

[ApiVersion(1.0)]
[Route("api/v{version:apiVersion}/orders")]
public class OrdersController(IMediator mediator) : BaseApiController(mediator)
{
    [Guard(roles: [Constants.Roles.Admin])]
    [HttpGet]
    [ProducesResponseType<Result<IEnumerable<OrderDto>>>(StatusCodes.Status200OK)]
    public async Task<ActionResult<Result<IEnumerable<OrderDto>>>> GetAllOrdersAsync()
        => CustomResult(await Mediator.Send(new GetAllQuery()));


    [Guard(roles: [Constants.Roles.User])]
    [HttpGet("me")]
    [ProducesResponseType<Result<IEnumerable<OrderDto>>>(StatusCodes.Status200OK)]
    public async Task<ActionResult<Result<IEnumerable<OrderDto>>>> GetUserOrdersAsync()
        => CustomResult(await Mediator.Send(new GetUserOrdersQuery()));

    [Guard(roles: [Constants.Roles.User])]
    [HttpGet("me/{id:guid}")]
    [ProducesResponseType(typeof(Result<OrderDto?>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Result<OrderDto?>>> GetUserOrderAsync([FromRoute] Guid id)
        => CustomResult(await Mediator.Send(new GetUserOrderQuery { OrderId = id }));

    [Guard(roles: [Constants.Roles.User])]
    [HttpPost("me/create-cash-order")]
    [ProducesResponseType(typeof(Result<OrderDto>), StatusCodes.Status200OK)]
    [ProducesResponseType<Result<OrderDto>>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<Result<OrderDto>>(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Result<OrderDto>>> CreateCashOrderAsync([FromBody] CreateCashOrderCommand command)
        => CustomResult(await Mediator.Send(command));

    [Guard(roles: [Constants.Roles.User])]
    [HttpPost("me/create-online-order")]
    [ProducesResponseType(typeof(Result<OrderDto>), StatusCodes.Status200OK)]
    [ProducesResponseType<Result<OrderDto>>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<Result<OrderDto>>(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Result<OrderDto>>> CreateOnlineOrderAsync([FromBody] CreateOnlineOrderCommand command)
        => CustomResult(await Mediator.Send(command));
}

