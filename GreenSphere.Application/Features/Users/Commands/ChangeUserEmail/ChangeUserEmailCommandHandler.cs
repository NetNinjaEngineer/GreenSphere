using GreenSphere.Application.Bases;
using GreenSphere.Application.Interfaces.Identity;
using MediatR;

namespace GreenSphere.Application.Features.Users.Commands.ChangeUserEmail
{
    public sealed class ChangeUserEmailCommandHandler(
        IUserService userService)
        : IRequestHandler<ChangeUserEmailCommand, Result<bool>>
    {
        public async Task<Result<bool>> Handle(
            ChangeUserEmailCommand request,
            CancellationToken cancellationToken)
        {
            return await userService.ChangeUserEmailAsync(request);
        }
    }
}