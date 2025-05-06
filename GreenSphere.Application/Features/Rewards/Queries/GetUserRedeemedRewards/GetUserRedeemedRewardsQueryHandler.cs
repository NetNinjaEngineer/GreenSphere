using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Rewards;
using GreenSphere.Application.Interfaces.Services;
using MediatR;

namespace GreenSphere.Application.Features.Rewards.Queries.GetUserRedeemedRewards;
public sealed class GetUserRedeemedRewardsQueryHandler(IRewardsService service)
    : IRequestHandler<GetUserRedeemedRewardsQuery, Result<IReadOnlyList<UserRewardDto>>>
{
    public async Task<Result<IReadOnlyList<UserRewardDto>>> Handle(
        GetUserRedeemedRewardsQuery request, CancellationToken cancellationToken)
        => await service.GetUserRedeemedRewardsAsync();
}
