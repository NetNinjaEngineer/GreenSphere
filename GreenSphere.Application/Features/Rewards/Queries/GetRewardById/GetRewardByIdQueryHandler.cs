using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Rewards;
using GreenSphere.Application.Interfaces.Services;
using MediatR;

namespace GreenSphere.Application.Features.Rewards.Queries.GetRewardById;
public sealed class GetRewardByIdQueryHandler(IRewardsService service)
    : IRequestHandler<GetRewardByIdQuery, Result<RewardDto?>>
{
    public async Task<Result<RewardDto?>> Handle(
        GetRewardByIdQuery request, CancellationToken cancellationToken)
        => await service.GetRewardByIdAsync(request);
}
