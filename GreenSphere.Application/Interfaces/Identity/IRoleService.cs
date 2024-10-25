using GreenSphere.Application.Abstractions;
using GreenSphere.Application.Features.Roles.Requests.Commands;

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
