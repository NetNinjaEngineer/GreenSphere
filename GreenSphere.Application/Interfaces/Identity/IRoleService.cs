using GreenSphere.Application.Abstractions;
using GreenSphere.Application.Features.Roles.Commands.AddClaimToRole;
using GreenSphere.Application.Features.Roles.Commands.AssignClaimToUser;
using GreenSphere.Application.Features.Roles.Commands.AssignRoleToUser;
using GreenSphere.Application.Features.Roles.Commands.CreateRole;
using GreenSphere.Application.Features.Roles.Commands.DeleteRole;
using GreenSphere.Application.Features.Roles.Commands.EditRole;

namespace GreenSphere.Application.Interfaces.Identity;
public interface IRoleService
{
    Task<Result<string>> CreateRole(CreateRoleCommand request);
    Task<Result<string>> EditRole(EditRoleCommand request);
    Task<Result<string>> DeleteRole(DeleteRoleCommand request);
    Task<Result<string>> AddClaimToRole(AddClaimToRoleCommand request);
    Task<Result<string>> AddRoleToUser(AssignRoleToUserCommand request);
    Task<Result<string>> AddClaimToUser(AssignClaimToUserCommand request);
    Task<Result<IEnumerable<string>>> GetUserRoles(string userId);
    Task<Result<IEnumerable<string>>> GetUserClaims(string userId);
    Task<Result<IEnumerable<string>>> GetRoleClaims(string roleName);
    Task<Result<IEnumerable<string>>> GetAllRoles();
}
