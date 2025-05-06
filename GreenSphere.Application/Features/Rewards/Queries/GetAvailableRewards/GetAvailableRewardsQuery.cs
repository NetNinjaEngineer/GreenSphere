using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Rewards;
using MediatR;

namespace GreenSphere.Application.Features.Rewards.Queries.GetAvailableRewards;
public sealed class GetAvailableRewardsQuery : IRequest<Result<IReadOnlyList<RewardDto>>>
{
}
