namespace GreenSphere.Application.Features.Auth.DTOs;

public record SendCodeConfirmEmailResponseDto
{
    public DateTimeOffset CodeExpiration { get; set; }

    public static SendCodeConfirmEmailResponseDto ToResponse(DateTimeOffset expiration)
        => new() { CodeExpiration = expiration };

}
