using GreenSphere.Application.Abstractions;
using GreenSphere.Application.Features.Auth.DTOs;
using MediatR;

namespace GreenSphere.Application.Features.Auth.Requests.Commands;
public sealed class RegisterCommand : IRequest<Result<SignUpResponseDto>>
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
