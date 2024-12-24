using GreenSphere.Application.Abstractions;
using GreenSphere.Domain.Enumerations;
using MediatR;

namespace GreenSphere.Application.Features.Auth.Commands.Enable2Fa;

public sealed class Enable2FaCommand : IRequest<Result<string>>
{
    public TokenProvider TokenProvider { get; set; }
    public string Email { get; set; } = null!;
}