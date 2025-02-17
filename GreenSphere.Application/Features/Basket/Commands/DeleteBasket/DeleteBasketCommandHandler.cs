using GreenSphere.Application.Bases;
using GreenSphere.Application.Interfaces.Services;
using MediatR;

namespace GreenSphere.Application.Features.Basket.Commands.DeleteBasket;
public sealed class DeleteBasketCommandHandler(IBasketService service)
    : IRequestHandler<DeleteBasketCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(
        DeleteBasketCommand request, CancellationToken cancellationToken)
        => await service.DeleteCustomerBasketAsync();
}
