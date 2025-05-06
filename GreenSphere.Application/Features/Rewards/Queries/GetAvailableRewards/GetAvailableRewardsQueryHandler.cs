using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Rewards;
using GreenSphere.Application.Interfaces.Services;
using MediatR;

namespace GreenSphere.Application.Features.Rewards.Queries.GetAvailableRewards;
public sealed class GetAvailableRewardsQueryHandler(IRewardsService service)
    : IRequestHandler<GetAvailableRewardsQuery, Result<IReadOnlyList<RewardDto>>>
{
    public async Task<Result<IReadOnlyList<RewardDto>>> Handle(
        GetAvailableRewardsQuery request, CancellationToken cancellationToken)
        => await service.GetAvailableRewardsAsync();
}
