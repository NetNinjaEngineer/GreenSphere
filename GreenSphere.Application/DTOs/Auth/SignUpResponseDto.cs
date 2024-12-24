namespace GreenSphere.Application.DTOs.Auth;
public record SignUpResponseDto
{
    public Guid UserId { get; init; }
    public bool IsActivateRequired { get; init; }

    public static SignUpResponseDto ToResponse(Guid userId)
        => new() { UserId = userId, IsActivateRequired = true };
}
