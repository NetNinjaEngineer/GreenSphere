using GreenSphere.Application.Bases;
using GreenSphere.Application.Interfaces.Services;
using MediatR;

namespace GreenSphere.Application.Features.Users.Commands.VerifyChangeUserEmail
{
    public sealed class VerifyChangeUserEmailCommandHandler(IUserService userService)
        : IRequestHandler<VerifyChangeUserEmailCommand, Result<bool>>
    {
        public async Task<Result<bool>> Handle(
            VerifyChangeUserEmailCommand request,
            CancellationToken cancellationToken)
        {
            return await userService.VerifyChangeUserEmailAsync(request);
        }
    }
}