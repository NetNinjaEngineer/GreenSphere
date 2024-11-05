using GreenSphere.Application.Abstractions;
using GreenSphere.Application.Features.Roles.Requests.Commands;
using GreenSphere.Application.Helpers;
using GreenSphere.Application.Interfaces.Identity;
using GreenSphere.Domain.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace GreenSphere.Identity.Services;

public sealed class RoleService(
    RoleManager<IdentityRole> roleManager,
    UserManager<ApplicationUser> userManager,
    ICurrentUser currentUser) : BaseResponseHandler, IRoleService
{
    public async Task<Result<string>> CreateRole(CreateRoleCommand request)
    {
        var roleExist = await roleManager.RoleExistsAsync(request.RoleName);
        if (roleExist)
        {
            return BadRequest<string>(string.Format(DomainErrors.Roles.ErrorCreatingRole, request.RoleName));
        }

        var role = new IdentityRole(request.RoleName);
        var result = await roleManager.CreateAsync(role);

        return result.Succeeded
            ? Success(string.Format(Constants.Roles.RoleCreatedSuccessfully, request.RoleName))
            : BadRequest<string>(string.Format(DomainErrors.Roles.ErrorCreatingRole, request.RoleName));
    }

    public async Task<Result<string>> EditRole(EditRoleCommand request)
    {
        var role = await roleManager.FindByNameAsync(request.RoleName);
        if (role == null)
        {
            return NotFound<string>(string.Format(DomainErrors.Roles.RoleNotFound, request.RoleName));
        }

        role.Name = request.NewRoleName;
        var result = await roleManager.UpdateAsync(role);

        return result.Succeeded
            ? Success(string.Format(Constants.Roles.RoleUpdatedSuccessfully, request.RoleName))
            : BadRequest<string>(string.Format(DomainErrors.Roles.ErrorUpdatingRole, request.RoleName));
    }

    public async Task<Result<string>> DeleteRole(DeleteRoleCommand request)
    {
        var role = await roleManager.FindByNameAsync(request.RoleName);
        if (role == null)
        {
            return BadRequest<string>(string.Format(DomainErrors.Roles.RoleNotFound, request.RoleName));
        }

        var result = await roleManager.DeleteAsync(role);

        return result.Succeeded
            ? Success(string.Format(Constants.Roles.RoleDeletedSuccessfully, role.Name))
            : BadRequest<string>(string.Format(DomainErrors.Roles.ErrorDeletingRole, role.Name));
    }

    public async Task<Result<string>> AddClaimToRole(AddClaimToRoleCommand request)
    {
        var role = await roleManager.FindByNameAsync(request.RoleName);
        if (role == null)
        {
            return NotFound<string>(string.Format(DomainErrors.Roles.RoleNotFound, request.RoleName));
        }

        var result = await roleManager.AddClaimAsync(role, new Claim(request.ClaimType, request.ClaimValue));

        return result.Succeeded
            ? Success(string.Format(Constants.Roles.ClaimAddedToRoleSuccessfully, request.ClaimType, request.ClaimValue, role.Name))
            : BadRequest<string>(string.Format(DomainErrors.Roles.ErrorAddingClaimToRole, role.Name));
    }
    public async Task<Result<IEnumerable<string>>> GetUserRoles(string userId)
    {
        var user = await userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return NotFound<IEnumerable<string>>(string.Format(DomainErrors.User.UserNotFound, userId));
        }

        var roles = await userManager.GetRolesAsync(user);
        return Success<IEnumerable<string>>(roles);
    }

    public async Task<Result<IEnumerable<string>>> GetUserClaims(string userId)
    {
        var user = await userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return NotFound<IEnumerable<string>>(string.Format(DomainErrors.User.UserNotFound, currentUser.Id));
        }

        var claims = await userManager.GetClaimsAsync(user);
        return Success<IEnumerable<string>>(claims.Select(a => a.Value).ToList());
    }

    public async Task<Result<IEnumerable<string>>> GetRoleClaims(string roleName)
    {
        var role = await roleManager.FindByNameAsync(roleName);
        if (role == null)
        {
            return NotFound<IEnumerable<string>>(string.Format(DomainErrors.Roles.RoleNotFound, roleName));
        }

        var claims = await roleManager.GetClaimsAsync(role);
        return Success(claims.Select(c => $"{c.Type}:{c.Value}"));
    }

    public async Task<Result<IEnumerable<string>>> GetAllRoles()
    {
        var roles = await Task.FromResult(roleManager.Roles.Select(r => r.Name));
        return Success<IEnumerable<string>>(roles);
    }

    public async Task<Result<string>> AddRoleToUser(AssignRoleToUserCommand request)
    {
        var user = await userManager.FindByEmailAsync(request.UserId);
        if (user == null)
        {
            return NotFound<string>(string.Format(DomainErrors.User.UserNotFound, request.UserId));
        }

        var result = await userManager.AddToRoleAsync(user, request.RoleName);

        return result.Succeeded
            ? Success(string.Format(Constants.Roles.RoleAssignedSuccessfully, request.RoleName, user.UserName))
            : BadRequest<string>(string.Format(DomainErrors.Roles.ErrorAssigningRole, request.RoleName, user.UserName));
    }

    public async Task<Result<string>> AddClaimToUser(AssignClaimToUserCommand request)
    {
        var user = await userManager.FindByIdAsync(request.UserId);
        if (user == null)
        {
            return NotFound<string>(string.Format(DomainErrors.User.UserNotFound, request.UserId));
        }

        var result = await userManager.AddClaimAsync(user, new Claim(request.ClaimType, request.ClaimValue));

        return result.Succeeded
            ? Success(string.Format(Constants.Roles.ClaimAddedSuccessfully, request.ClaimType, request.ClaimValue, user.UserName))
            : BadRequest<string>(string.Format(DomainErrors.Roles.ErrorAddingClaim, user.UserName));
    }
}
