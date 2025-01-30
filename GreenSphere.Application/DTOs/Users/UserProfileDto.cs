using GreenSphere.Domain.Common;

namespace GreenSphere.Application.DTOs.Users;

public record UserProfileDto
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public Gender Gender { get; set; }
    public DateTime DateOfBirth { get; set; }
}


