using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Rewards;
using MediatR;

namespace GreenSphere.Application.Features.Rewards.Queries.GetUserRedeemedRewards;
public sealed class GetUserRedeemedRewardsQuery : IRequest<Result<IReadOnlyList<UserRewardDto>>>
{
}
