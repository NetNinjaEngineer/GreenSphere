using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Basket;
using MediatR;

namespace GreenSphere.Application.Features.Basket.Commands.AddItemToBasket;
public sealed class AddItemToBasketCommand : IRequest<Result<BasketDto>>
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}
