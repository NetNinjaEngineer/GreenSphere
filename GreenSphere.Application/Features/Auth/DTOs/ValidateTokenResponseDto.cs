namespace GreenSphere.Application.Features.Auth.DTOs;

public sealed record ValidateTokenResponseDto
{
    public List<ClaimsResponse> Claims { get; set; } = [];
}