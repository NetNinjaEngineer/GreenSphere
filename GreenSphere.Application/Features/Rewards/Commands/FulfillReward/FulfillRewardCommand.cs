using GreenSphere.Application.Bases;
using MediatR;

namespace GreenSphere.Application.Features.Rewards.Commands.FulfillReward;
public sealed class FulfillRewardCommand : IRequest<Result<bool>>
{
    public Guid RewardId { get; set; }
}