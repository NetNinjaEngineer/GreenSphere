using Asp.Versioning;
using GreenSphere.Api.Controllers.Base;
using GreenSphere.Application.Attributes;
using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Rewards;
using GreenSphere.Application.Features.Rewards.Commands.FulfillReward;
using GreenSphere.Application.Features.Rewards.Commands.RedeemReward;
using GreenSphere.Application.Features.Rewards.Queries.GetAvailableRewards;
using GreenSphere.Application.Features.Rewards.Queries.GetRewardById;
using GreenSphere.Application.Features.Rewards.Queries.GetUserRedeemedRewards;
using GreenSphere.Application.Helpers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GreenSphere.Api.Controllers;
[ApiVersion(1.0)]
[Route("api/v{ver:apiVersion}/rewards")]
public class RewardsController(IMediator mediator) : BaseApiController(mediator)
{
    [Guard]
    [HttpGet]
    [ProducesResponseType(typeof(Result<IReadOnlyList<RewardDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAvailableRewards()
        => CustomResult(await Mediator.Send(new GetAvailableRewardsQuery()));

    [Guard]
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(Result<RewardDto?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<RewardDto?>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetRewardById(Guid id)
        => CustomResult(await Mediator.Send(new GetRewardByIdQuery() { ProductId = id }));

    [Guard]
    [HttpPost("redeem")]
    [ProducesResponseType(typeof(Result<UserRewardDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<UserRewardDto>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RedeemReward([FromBody] RedeemRewardCommand command)
        => CustomResult(await Mediator.Send(command));

    [Guard]
    [HttpGet("me/redeemed-rewards")]
    [ProducesResponseType(typeof(Result<IReadOnlyList<UserRewardDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUserRewards()
        => CustomResult(await Mediator.Send(new GetUserRedeemedRewardsQuery()));

    [HttpPut("fulfill/{id:guid}")]
    [Guard(roles: [Constants.Roles.Admin])]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> FulfillReward([FromRoute] Guid id)
        => CustomResult(await Mediator.Send(new FulfillRewardCommand() { RewardId = id }));
}
