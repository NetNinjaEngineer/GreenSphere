using GreenSphere.Application.Bases;
using MediatR;

namespace GreenSphere.Application.Features.Basket.Commands.DeleteBasket;
public sealed class DeleteBasketCommand : IRequest<Result<bool>>
{
}
