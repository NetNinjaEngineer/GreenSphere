using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Rewards;
using GreenSphere.Application.Features.Rewards.Commands.FulfillReward;
using GreenSphere.Application.Features.Rewards.Commands.RedeemReward;
using GreenSphere.Application.Features.Rewards.Queries.GetRewardById;

namespace GreenSphere.Application.Interfaces.Services;

public interface IRewardsService
{
    Task<Result<IReadOnlyList<RewardDto>>> GetAvailableRewardsAsync();
    Task<Result<RewardDto?>> GetRewardByIdAsync(GetRewardByIdQuery query);
    Task<Result<UserRewardDto>> RedeemRewardAsync(RedeemRewardCommand command);
    Task<Result<IReadOnlyList<UserRewardDto>>> GetUserRedeemedRewardsAsync();
    Task<Result<bool>> FulfillRewardAsync(FulfillRewardCommand command);
}