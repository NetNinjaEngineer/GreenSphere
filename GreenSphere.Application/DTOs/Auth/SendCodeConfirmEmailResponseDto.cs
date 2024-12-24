namespace GreenSphere.Application.DTOs.Auth;

public record SendCodeConfirmEmailResponseDto
{
    public DateTimeOffset CodeExpiration { get; set; }

    public static SendCodeConfirmEmailResponseDto ToResponse(DateTimeOffset expiration)
        => new() { CodeExpiration = expiration };

}
