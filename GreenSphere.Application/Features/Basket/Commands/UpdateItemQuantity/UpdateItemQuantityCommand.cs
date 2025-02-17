using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Basket;
using MediatR;

namespace GreenSphere.Application.Features.Basket.Commands.UpdateItemQuantity;
public sealed class UpdateItemQuantityCommand : IRequest<Result<BasketDto>>
{
    public Guid BasketItemId { get; set; }
    public int Quantity { get; set; }
}
