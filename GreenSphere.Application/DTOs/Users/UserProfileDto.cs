using GreenSphere.Domain.Enumerations;

namespace GreenSphere.Application.DTOs.Users;

public record UserProfileDto
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public Gender? Gender { get; set; }
    public DateOnly? DateOfBirth { get; set; }
    public string? ProfilePictureUrl { get; set; }
}


