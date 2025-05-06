using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Rewards;
using GreenSphere.Application.Interfaces.Services;
using MediatR;

namespace GreenSphere.Application.Features.Rewards.Commands.RedeemReward;
public sealed class RedeemRewardCommandHandler(IRewardsService service)
    : IRequestHandler<RedeemRewardCommand, Result<UserRewardDto>>
{
    public async Task<Result<UserRewardDto>> Handle(
        RedeemRewardCommand request, CancellationToken cancellationToken)
        => await service.RedeemRewardAsync(request);
}
