using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Rewards;
using MediatR;

namespace GreenSphere.Application.Features.Rewards.Queries.GetRewardById;
public sealed class GetRewardByIdQuery : IRequest<Result<RewardDto?>>
{
    public Guid ProductId { get; set; }
}
