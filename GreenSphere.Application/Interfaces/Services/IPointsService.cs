using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Points;
using GreenSphere.Application.Features.Points.Commands.AddPoints;
using GreenSphere.Application.Features.Points.Commands.SpendPoints;
using GreenSphere.Application.Features.Points.Queries.GetAvailablePoints;
using GreenSphere.Application.Features.Points.Queries.GetPointsHistory;
using GreenSphere.Application.Features.Points.Queries.GetPointsSummary;

namespace GreenSphere.Application.Interfaces.Services;

public interface IPointsService
{
    Task<Result<PointsSummaryDto?>> GetPointsSummaryAsync(GetPointsSummaryQuery query);
    Task<Result<IEnumerable<PointsDto>>> GetPointsHistoryAsync(GetPointsHistoryQuery query);
    Task<Result<PointsDto>> AddPointsAsync(AddPointsCommand command);
    Task<Result<bool>> SpendPointsAsync(SpendPointsCommand command);
    Task<Result<long>> GetAvailablePointsAsync(GetAvailablePointsQuery query);
}