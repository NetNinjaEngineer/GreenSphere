using System.Security.Claims;
using GreenSphere.Application.Abstractions;
using GreenSphere.Application.Features.Roles.Commands.AddClaimToRole;
using GreenSphere.Application.Features.Roles.Commands.AssignClaimToUser;
using GreenSphere.Application.Features.Roles.Commands.AssignRoleToUser;
using GreenSphere.Application.Features.Roles.Commands.CreateRole;
using GreenSphere.Application.Features.Roles.Commands.DeleteRole;
using GreenSphere.Application.Features.Roles.Commands.EditRole;
using GreenSphere.Application.Helpers;
using GreenSphere.Application.Interfaces.Services;
using GreenSphere.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace GreenSphere.Services.Services;

public sealed class RoleService(
    RoleManager<IdentityRole> roleManager,
    UserManager<ApplicationUser> userManager,
    ICurrentUser currentUser,
    IStringLocalizer<BaseResponseHandler> localizer) : BaseResponseHandler(localizer), IRoleService
{
    public async Task<Result<string>> CreateRole(CreateRoleCommand request)
    {
        var roleExist = await roleManager.RoleExistsAsync(request.RoleName);
        if (roleExist)
        {
            return BadRequest<string>(Localizer["ErrorCreatingRole", request.RoleName]);
        }

        var role = new IdentityRole(request.RoleName);
        var result = await roleManager.CreateAsync(role);

        return result.Succeeded
            ? Success(string.Format(Constants.Roles.RoleCreatedSuccessfully, request.RoleName))
            : BadRequest<string>(Localizer["ErrorCreatingRole", request.RoleName]);
    }

    public async Task<Result<string>> EditRole(EditRoleCommand request)
    {
        var role = await roleManager.FindByNameAsync(request.RoleName);
        if (role == null)
        {
            return NotFound<string>(Localizer["RoleNotFound", request.RoleName]);
        }

        role.Name = request.NewRoleName;
        var result = await roleManager.UpdateAsync(role);

        return result.Succeeded
            ? Success<string>(Localizer["RoleUpdatedSuccessfully", request.RoleName])
            : BadRequest<string>(Localizer["ErrorUpdatingRole", request.RoleName]);
    }

    public async Task<Result<string>> DeleteRole(DeleteRoleCommand request)
    {
        var role = await roleManager.FindByNameAsync(request.RoleName);
        if (role == null)
        {
            return BadRequest<string>(Localizer["RoleNotFound", request.RoleName]);
        }

        var result = await roleManager.DeleteAsync(role);

        return result.Succeeded
            ? Success<string>(Localizer["RoleDeletedSuccessfully", request.RoleName])
            : BadRequest<string>(Localizer["ErrorDeletingRole", request.RoleName]);
    }

    public async Task<Result<string>> AddClaimToRole(AddClaimToRoleCommand request)
    {
        var role = await roleManager.FindByNameAsync(request.RoleName);
        if (role == null)
        {
            return NotFound<string>(Localizer["RoleNotFound", request.RoleName]);
        }

        var result = await roleManager.AddClaimAsync(role, new Claim(request.ClaimType, request.ClaimValue));

        return result.Succeeded
            ? Success<string>(Localizer["ClaimAddedToRoleSuccessfully", request.ClaimType, request.ClaimValue, role.Name!])
            : BadRequest<string>(Localizer["ErrorAddingClaimToRole", role.Name!]);
    }
    public async Task<Result<IEnumerable<string>>> GetUserRoles(string userId)
    {
        var user = await userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return NotFound<IEnumerable<string>>(Localizer["UserNotFound", userId]);
        }

        var roles = await userManager.GetRolesAsync(user);
        return Success<IEnumerable<string>>(roles);
    }

    public async Task<Result<IEnumerable<string>>> GetUserClaims(string userId)
    {
        var user = await userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return NotFound<IEnumerable<string>>(Localizer["UserNotFound", currentUser.Id]);
        }

        var claims = await userManager.GetClaimsAsync(user);
        return Success<IEnumerable<string>>([.. claims.Select(a => a.Value)]);
    }

    public async Task<Result<IEnumerable<string>>> GetRoleClaims(string roleName)
    {
        var role = await roleManager.FindByNameAsync(roleName);
        if (role == null)
        {
            return NotFound<IEnumerable<string>>(Localizer["RoleNotFound", roleName]);
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
            return NotFound<string>(Localizer["UserNotFound", request.UserId]);
        }

        var result = await userManager.AddToRoleAsync(user, request.RoleName);

        return result.Succeeded
            ? Success<string>(Localizer["RoleAssignedSuccessfully", request.RoleName, user.UserName!])
            : BadRequest<string>(Localizer["ErrorAssigningRole", request.RoleName, user.UserName!]);
    }

    public async Task<Result<string>> AddClaimToUser(AssignClaimToUserCommand request)
    {
        var user = await userManager.FindByIdAsync(request.UserId);
        if (user == null)
        {
            return NotFound<string>(Localizer["UserNotFound", request.UserId]);
        }

        var result = await userManager.AddClaimAsync(user, new Claim(request.ClaimType, request.ClaimValue));

        return result.Succeeded
            ? Success<string>(Localizer["ClaimAddedSuccessfully", request.ClaimType, request.ClaimValue, user.UserName!])
            : BadRequest<string>(Localizer["ErrorAddingClaim", user.UserName!]);
    }
}
