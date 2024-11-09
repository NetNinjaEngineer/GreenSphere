using GreenSphere.Application.Abstractions;
using GreenSphere.Domain.Identity.Enumerations;
using MediatR;

namespace GreenSphere.Application.Features.Auth.Requests.Commands;

public sealed class Enable2FaCommand : IRequest<Result<string>>
{
    public TokenProvider TokenProvider { get; set; }
    public string Email { get; set; } = null!;
}