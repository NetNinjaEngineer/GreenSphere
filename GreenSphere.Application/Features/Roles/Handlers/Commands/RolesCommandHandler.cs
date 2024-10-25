using GreenSphere.Application.Abstractions;
using GreenSphere.Application.Features.Roles.Requests.Commands;
using GreenSphere.Application.Interfaces.Identity;
using MediatR;

namespace GreenSphere.Application.Features.Roles.Handlers.Commands;
public class RolesCommandHandler(IRoleService roleService) :
    IRequestHandler<AddClaimToRoleCommand, Result<string>>,
    IRequestHandler<AssignClaimToUserCommand, Result<string>>,
    IRequestHandler<AssignRoleToUserCommand, Result<string>>,
    IRequestHandler<CreateRoleCommand, Result<string>>,
    IRequestHandler<DeleteRoleCommand, Result<string>>,
    IRequestHandler<EditRoleCommand, Result<string>>
{
    public async Task<Result<string>> Handle(AddClaimToRoleCommand request, CancellationToken cancellationToken)
        => await roleService.AddClaimToRole(request);

    public async Task<Result<string>> Handle(AssignClaimToUserCommand request, CancellationToken cancellationToken)
        => await roleService.AddClaimToUser(request);

    public async Task<Result<string>> Handle(AssignRoleToUserCommand request, CancellationToken cancellationToken)
        => await roleService.AddRoleToUser(request);

    public async Task<Result<string>> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        => await roleService.CreateRole(request);

    public async Task<Result<string>> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        => await roleService.DeleteRole(request);

    public async Task<Result<string>> Handle(EditRoleCommand request, CancellationToken cancellationToken)
        => await roleService.EditRole(request);
}
