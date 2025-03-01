using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Order;
using GreenSphere.Application.Features.Orders.Commands.CreateCashOrder;
using GreenSphere.Application.Features.Orders.Commands.CreateOnlineOrder;
using GreenSphere.Application.Features.Orders.Queries.GetUserOrder;

namespace GreenSphere.Application.Interfaces.Services;

public interface IOrderService
{
    Task<Result<OrderDto>> CreateCashOrderAsync(CreateCashOrderCommand command);
    Task<Result<OrderDto>> CreateOnlineOrderAsync(CreateOnlineOrderCommand command);
    Task<Result<IEnumerable<OrderDto>>> GetAllOrdersAsync();
    Task<Result<IEnumerable<OrderDto>>> GetUserOrdersAsync();
    Task<Result<OrderDto?>> GetUserOrderAsync(GetUserOrderQuery query);
}