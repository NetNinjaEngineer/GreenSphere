using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Basket;
using GreenSphere.Application.Interfaces.Services;
using MediatR;

namespace GreenSphere.Application.Features.Basket.Commands.UpdateItemQuantity;
public sealed class UpdateItemQuantityCommandHandler(IBasketService service)
    : IRequestHandler<UpdateItemQuantityCommand, Result<BasketDto>>
{
    public async Task<Result<BasketDto>> Handle(
        UpdateItemQuantityCommand request, CancellationToken cancellationToken)
        => await service.UpdateItemQuantityInCustomerBasketAsync(request);
}
