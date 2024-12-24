using GreenSphere.Application.Abstractions;
using MediatR;

namespace GreenSphere.Application.Features.Auth.Commands.Disable2Fa;

public sealed class Disable2FaCommand : IRequest<Result<string>>
{
    public string Email { get; set; } = null!;
}