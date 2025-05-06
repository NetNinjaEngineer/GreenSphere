using System.Net;
using AutoMapper;
using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Points;
using GreenSphere.Application.Features.Points.Commands.AddPoints;
using GreenSphere.Application.Features.Points.Commands.SpendPoints;
using GreenSphere.Application.Features.Points.Queries.GetAvailablePoints;
using GreenSphere.Application.Features.Points.Queries.GetPointsHistory;
using GreenSphere.Application.Features.Points.Queries.GetPointsSummary;
using GreenSphere.Application.Interfaces.Services;
using GreenSphere.Domain.Entities;
using GreenSphere.Domain.Interfaces;
using GreenSphere.Domain.Specifications;

namespace GreenSphere.Services.Services;

public sealed class PointsService(
    IMapper mapper,
    ICurrentUser currentUser,
    IGenericRepository<UserPoints> userPointsRepository) : IPointsService
{
    public async Task<Result<PointsSummaryDto?>> GetPointsSummaryAsync(GetPointsSummaryQuery query)
    {
        var today = DateTimeOffset.Now.Date;
        var warningDate = today.AddDays(30);

        var userPointsSpec = new UserPointsWithDetailsSpecification(currentUser.Id);

        var pointsHistory = (await userPointsRepository.GetAllWithSpecificationAsync(userPointsSpec)).ToList();

        if (pointsHistory.Count == 0)
            return Result<PointsSummaryDto?>.Success(null);

        var totalPoints = pointsHistory.Sum(p => p.Points);
        var spentPoints = pointsHistory.Where(p => p.IsSpent).Sum(p => p.Points);
        var expiredPoints = pointsHistory.Where(p => p is { IsExpired: true, IsSpent: false }).Sum(p => p.Points);
        var availablePoints = pointsHistory.Where(p => p is { IsSpent: false, IsExpired: false }).Sum(p => p.Points);

        var expiringPoints = pointsHistory
            .Where(p => p is { IsSpent: false, IsExpired: false } && p.ExpirationDate <= warningDate)
            .Sum(p => p.Points);

        var nextExpiryDate = pointsHistory
            .Where(p => p is { IsSpent: false, IsExpired: false })
            .OrderBy(p => p.ExpirationDate)
            .FirstOrDefault()?.ExpirationDate;

        var lastEarnedDate = pointsHistory
            .Where(p => p is { IsSpent: false, IsExpired: false })
            .Max(p => p.EarnedDate);

        return Result<PointsSummaryDto?>.Success(
            new PointsSummaryDto
            {
                AvailablePoints = availablePoints,
                LastEarnedDate = lastEarnedDate,
                TotalPoints = totalPoints,
                SpentPoints = spentPoints,
                ExpiredPoints = expiredPoints,
                ExpiringPoints = expiringPoints,
                NextExpiryDate = nextExpiryDate
            });

    }

    public async Task<Result<IEnumerable<PointsDto>>> GetPointsHistoryAsync(GetPointsHistoryQuery query)
    {
        var userPointsSpec = new UserPointsWithDetailsSpecification(currentUser.Id);
        var pointsHistory = await userPointsRepository.GetAllWithSpecificationAsync(userPointsSpec);
        var mappedPoints = mapper.Map<IEnumerable<PointsDto>>(pointsHistory);
        return Result<IEnumerable<PointsDto>>.Success(mappedPoints);
    }

    public async Task<Result<PointsDto>> AddPointsAsync(AddPointsCommand command)
    {
        var userPoints = new UserPoints
        {
            Id = Guid.NewGuid(),
            UserId = command.UserId,
            Points = command.Points,
            EarnedDate = DateTime.UtcNow,
            ActivityType = command.ActivityType,
            Description = command.Description,
            IsSpent = false
        };

        userPointsRepository.Create(userPoints);

        await userPointsRepository.SaveChangesAsync();

        return Result<PointsDto>.Success(mapper.Map<PointsDto>(userPoints));
    }

    public async Task<Result<bool>> SpendPointsAsync(SpendPointsCommand command)
    {
        var availablePointsResult = await GetAvailablePointsAsync(new GetAvailablePointsQuery());
        var availablePoints = availablePointsResult.Value;

        if (availablePoints < command.Points)
            return Result<bool>.Failure(HttpStatusCode.BadRequest, "Not have enough points");

        var pointsToSpend = command.Points;

        var pointsNotSpentSpec = new UserNotSpentPointsSpecification(currentUser.Id);
        var pointsNotSpentHistory = await userPointsRepository.GetAllWithSpecificationAsync(pointsNotSpentSpec);

        foreach (var pointEntry in pointsNotSpentHistory)
        {
            if (pointsToSpend <= 0)
                break;

            if (pointEntry.Points <= pointsToSpend)
            {
                pointEntry.IsSpent = true;
                pointsToSpend -= pointEntry.Points;
            }
            else
            {
                var newSpentEntry = new UserPoints
                {
                    Id = Guid.NewGuid(),
                    UserId = currentUser.Id,
                    Points = pointsToSpend,
                    EarnedDate = pointEntry.EarnedDate,
                    ActivityType = pointEntry.ActivityType,
                    Description = pointEntry.Description,
                    IsSpent = true
                };

                pointEntry.Points -= pointsToSpend;

                userPointsRepository.Create(newSpentEntry);
                pointsToSpend = 0;
            }

        }

        await userPointsRepository.SaveChangesAsync();

        return Result<bool>.Success(true);
    }

    public async Task<Result<long>> GetAvailablePointsAsync(GetAvailablePointsQuery query)
    {
        var userPointsSpec = new UserPointsWithDetailsSpecification(currentUser.Id);

        var pointsHistory = (await userPointsRepository.GetAllWithSpecificationAsync(userPointsSpec)).ToList();

        var totalPoints = pointsHistory.Sum(p => p.Points);
        var spentPoints = pointsHistory.Where(p => p.IsSpent).Sum(p => p.Points);

        return Result<long>.Success(totalPoints - spentPoints);
    }
}