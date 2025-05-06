using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Rewards;
using MediatR;

namespace GreenSphere.Application.Features.Rewards.Commands.RedeemReward;
public sealed class RedeemRewardCommand : IRequest<Result<UserRewardDto>>
{
    public Guid ProductId { get; set; }
}
