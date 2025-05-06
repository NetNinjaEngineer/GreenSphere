using GreenSphere.Application.Bases;
using GreenSphere.Application.Interfaces.Services;
using MediatR;

namespace GreenSphere.Application.Features.Rewards.Commands.FulfillReward;
public sealed class FulfillRewardCommandHandler(IRewardsService service)
    : IRequestHandler<FulfillRewardCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(
        FulfillRewardCommand request, CancellationToken cancellationToken)
        => await service.FulfillRewardAsync(request);
}
