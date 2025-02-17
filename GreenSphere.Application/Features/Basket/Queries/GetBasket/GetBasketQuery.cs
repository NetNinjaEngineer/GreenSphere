using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Basket;
using MediatR;

namespace GreenSphere.Application.Features.Basket.Queries.GetBasket;
public sealed class GetBasketQuery : IRequest<Result<BasketDto>>
{
}
