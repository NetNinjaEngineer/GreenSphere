using GreenSphere.Application.Bases;
using MediatR;

namespace GreenSphere.Application.Features.Basket.Queries.GetItemsCount;
public sealed class GetItemsCountQuery : IRequest<Result<int>>
{
}
