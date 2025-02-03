using GreenSphere.Application.Bases;
using MediatR;

namespace GreenSphere.Application.Features.Users.Commands.ChangeUserPassword
{
    public sealed class ChangeUserPasswordCommand : IRequest<Result<bool>>
    {
        public string CurrentPassword { get; set; } = null!;
        public string NewPassword { get; set; } = null!;
        public string ConfirmNewPassword { get; set; } = null!;

    }
}
