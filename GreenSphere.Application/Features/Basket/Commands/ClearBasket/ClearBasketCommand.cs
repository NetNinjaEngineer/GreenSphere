using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Basket;
using MediatR;

namespace GreenSphere.Application.Features.Basket.Commands.ClearBasket;
public sealed class ClearBasketCommand : IRequest<Result<BasketDto>>
{
}
