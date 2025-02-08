namespace GreenSphere.Application.DTOs.Ratings;

public sealed class RatingStatisticsDto
{
    public double AverageRating { get; set; }
    public int TotalRatings { get; set; }
    public Dictionary<int, int> RatingDistribution { get; set; } = [];
    public int TotalComments { get; set; }
    public DateTimeOffset LastRatedDate { get; set; }
    public DateTimeOffset FirstRatedDate { get; set; }
}