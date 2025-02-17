using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Basket;
using MediatR;

namespace GreenSphere.Application.Features.Basket.Commands.RemoveItemFromBasket;
public sealed class RemoveItemFromBasketCommand : IRequest<Result<BasketDto>>
{
    public Guid BasketItemId { get; set; }
}
