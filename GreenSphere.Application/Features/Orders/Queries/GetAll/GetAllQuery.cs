using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Order;
using MediatR;

namespace GreenSphere.Application.Features.Orders.Queries.GetAll;

public sealed class GetAllQuery : IRequest<Result<IEnumerable<OrderDto>>>
{
}