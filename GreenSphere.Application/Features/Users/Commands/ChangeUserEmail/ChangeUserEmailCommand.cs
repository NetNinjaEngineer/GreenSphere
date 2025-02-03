using GreenSphere.Application.Bases;
using MediatR;

namespace GreenSphere.Application.Features.Users.Commands.ChangeUserEmail
{
    public sealed class ChangeUserEmailCommand : IRequest<Result<bool>>
    {
        public string NewEmail { get; set; } = null!;
        public string CurrentPassword { get; set; } = null!;
    }
}
