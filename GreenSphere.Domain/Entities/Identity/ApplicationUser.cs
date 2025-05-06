using GreenSphere.Domain.Enumerations;
using Microsoft.AspNetCore.Identity;

namespace GreenSphere.Domain.Entities.Identity;
public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? Code { get; set; }
    public DateTimeOffset? CodeExpiration { get; set; }
    public Gender? Gender { get; set; }
    public DateOnly? DateOfBirth { get; set; }
    public string? ProfilePictureUrl { get; set; }
    public ICollection<Rating> Ratings { get; set; } = [];
    public ICollection<Order> Orders { get; set; } = [];
    public List<RefreshToken>? RefreshTokens { get; set; }
    public ICollection<Address> Addresses { get; set; } = [];
    public ICollection<Short> Shorts { get; set; } = [];
    public ICollection<UserPoints> PointsHistory { get; set; } = [];
    public ICollection<UserReward> RedeemedRewards { get; set; } = [];
}