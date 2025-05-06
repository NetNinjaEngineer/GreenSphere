using System.Net;
using AutoMapper;
using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Rewards;
using GreenSphere.Application.Features.Points.Commands.SpendPoints;
using GreenSphere.Application.Features.Points.Queries.GetAvailablePoints;
using GreenSphere.Application.Features.Rewards.Commands.FulfillReward;
using GreenSphere.Application.Features.Rewards.Commands.RedeemReward;
using GreenSphere.Application.Features.Rewards.Queries.GetRewardById;
using GreenSphere.Application.Interfaces.Services;
using GreenSphere.Domain.Entities;
using GreenSphere.Domain.Interfaces;
using GreenSphere.Domain.Specifications;
using GreenSphere.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace GreenSphere.Services.Services;
public sealed class RewardsService(
    IMapper mapper,
    IGenericRepository<Product> productRepository,
    IPointsService pointsService,
    ApplicationDbContext context,
    ICurrentUser currentUser,
    IGenericRepository<UserReward> userRewardRepository,
    ILogger<RewardsService> logger,
    IConfiguration configuration) : IRewardsService
{
    public async Task<Result<IReadOnlyList<RewardDto>>> GetAvailableRewardsAsync()
    {
        var rewardsSpec = new RewardsSpecification();
        var rewards = await productRepository.GetAllWithSpecificationAsync(rewardsSpec);
        var mappedRewards = mapper.Map<IReadOnlyList<RewardDto>>(rewards);
        return Result<IReadOnlyList<RewardDto>>.Success(mappedRewards);
    }

    public async Task<Result<RewardDto?>> GetRewardByIdAsync(GetRewardByIdQuery query)
    {
        var rewardsSpec = new RewardsSpecification(query.ProductId);
        var reward = await productRepository.GetBySpecificationAsync(rewardsSpec);
        if (reward == null)
            return Result<RewardDto?>.Failure(HttpStatusCode.NotFound);
        var mappedResult = mapper.Map<RewardDto>(reward);
        return Result<RewardDto?>.Success(mappedResult);
    }

    public async Task<Result<UserRewardDto>> RedeemRewardAsync(RedeemRewardCommand command)
    {
        var activeRewardSpec = new GetActiveRewardSpecification(command.ProductId);
        var activeReward = await productRepository.GetBySpecificationAsync(activeRewardSpec);
        if (activeReward == null)
            return Result<UserRewardDto>.Failure(HttpStatusCode.BadRequest, "Reward not found or not available for redemption");

        var availablePoints = await pointsService.GetAvailablePointsAsync(new GetAvailablePointsQuery());

        if (availablePoints.Value < activeReward.PointsCost)
            return Result<UserRewardDto>.Failure(HttpStatusCode.BadRequest, "Insufficient points to redeem this reward");

        await using var transaction = await context.Database.BeginTransactionAsync();

        try
        {
            var pointsSpentResult = await pointsService.SpendPointsAsync(new SpendPointsCommand() { Points = activeReward.PointsCost!.Value });

            if (pointsSpentResult.IsFailure)
                return Result<UserRewardDto>.Failure(HttpStatusCode.BadRequest, "Failed to spend points");

            var userReward = new UserReward
            {
                UserId = currentUser.Id,
                ProductId = command.ProductId,
                RedeemedDate = DateTime.UtcNow,
                PointsSpent = activeReward.PointsCost.Value
            };

            userRewardRepository.Create(userReward);

            activeReward.StockQuantity--;
            productRepository.Update(activeReward);

            await productRepository.SaveChangesAsync();

            await transaction.CommitAsync();

            return Result<UserRewardDto>.Success(new UserRewardDto()
            {
                Id = userReward.Id,
                ProductId = userReward.ProductId,
                ProductName = activeReward.Name,
                ProductImageUrl = $"{configuration["Urls:BaseApiUrl"]}/Uploads/Images/{activeReward.Img}",
                RedeemedDate = userReward.RedeemedDate,
                FulfilledDate = userReward.FulfilledDate,
                PointsSpent = userReward.PointsSpent
            });

        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            logger.LogError(ex, "Error during reward redemption");

            return Result<UserRewardDto>.Failure(HttpStatusCode.BadRequest);
        }
    }

    public async Task<Result<IReadOnlyList<UserRewardDto>>> GetUserRedeemedRewardsAsync()
    {
        var userRewardsSpec = new GetUserRedeemedRewardsSpecification(currentUser.Id);
        var userRewards = await userRewardRepository.GetAllWithSpecificationAsync(userRewardsSpec);
        return Result<IReadOnlyList<UserRewardDto>>.Success(mapper.Map<IReadOnlyList<UserRewardDto>>(userRewards));
    }

    public async Task<Result<bool>> FulfillRewardAsync(FulfillRewardCommand command)
    {
        var userReward = await userRewardRepository.GetByIdAsync(command.RewardId);

        if (userReward == null)
        {
            return Result<bool>.Failure(HttpStatusCode.NotFound);
        }

        userReward.FulfilledDate = DateTimeOffset.Now;
        userRewardRepository.Update(userReward);
        await userRewardRepository.SaveChangesAsync();

        return Result<bool>.Success(true);
    }
}
