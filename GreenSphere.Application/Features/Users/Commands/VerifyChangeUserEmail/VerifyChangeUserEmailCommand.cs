using GreenSphere.Application.Bases;
using MediatR;

namespace GreenSphere.Application.Features.Users.Commands.VerifyChangeUserEmail
{
    public sealed class VerifyChangeUserEmailCommand : IRequest<Result<bool>>
    {
        public string Code { get; set; } = null!;
    }
}