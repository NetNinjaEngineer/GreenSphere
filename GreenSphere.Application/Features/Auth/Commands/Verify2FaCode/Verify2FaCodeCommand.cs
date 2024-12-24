using GreenSphere.Application.Abstractions;
using GreenSphere.Application.DTOs.Auth;
using MediatR;

namespace GreenSphere.Application.Features.Auth.Commands.Verify2FaCode;

public sealed class Verify2FaCodeCommand : IRequest<Result<SignInResponseDto>>
{
    public string Code { get; set; } = null!;
}