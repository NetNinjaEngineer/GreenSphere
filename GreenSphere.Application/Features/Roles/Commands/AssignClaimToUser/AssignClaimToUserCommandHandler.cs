using GreenSphere.Application.Abstractions;
using GreenSphere.Application.Interfaces.Services;
using MediatR;

namespace GreenSphere.Application.Features.Roles.Commands.AssignClaimToUser;

public class AssignClaimToUserCommandHandler(IRoleService roleService) : IRequestHandler<AssignClaimToUserCommand, Result<string>>
{
    public async Task<Result<string>> Handle(AssignClaimToUserCommand request, CancellationToken cancellationToken)
        => await roleService.AddClaimToUser(request);
}
