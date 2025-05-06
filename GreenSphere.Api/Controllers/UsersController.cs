using Asp.Versioning;
using GreenSphere.Api.Controllers.Base;
using GreenSphere.Application.Attributes;
using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Users;
using GreenSphere.Application.Features.Users.Commands.ChangeUserEmail;
using GreenSphere.Application.Features.Users.Commands.ChangeUserPassword;
using GreenSphere.Application.Features.Users.Commands.CreateAddress;
using GreenSphere.Application.Features.Users.Commands.DeleteAccount;
using GreenSphere.Application.Features.Users.Commands.DeleteAddress;
using GreenSphere.Application.Features.Users.Commands.EditUserProfile;
using GreenSphere.Application.Features.Users.Commands.SetMainAddress;
using GreenSphere.Application.Features.Users.Commands.UpdateAddress;
using GreenSphere.Application.Features.Users.Commands.VerifyChangeUserEmail;
using GreenSphere.Application.Features.Users.Queries.GetAddress;
using GreenSphere.Application.Features.Users.Queries.GetMainAddress;
using GreenSphere.Application.Features.Users.Queries.GetUserAddresses;
using GreenSphere.Application.Features.Users.Queries.GetUserProfile;
using GreenSphere.Application.Helpers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GreenSphere.Api.Controllers;
[ApiVersion(1.0)]
[Route("api/v{ver:apiVersion}/users")]
public class UsersController(IMediator mediator) : BaseApiController(mediator)
{
    [HttpGet("profile")]
    [Guard(roles: [Constants.Roles.User])]
    [ProducesResponseType(typeof(Result<UserProfileDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<UserProfileDto>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<Result<UserProfileDto>>> GetUserProfileAsync()
        => CustomResult(await Mediator.Send(new GetUserProfileQuery()));

    [Guard]
    [HttpPost("edit-profile")]
    [ProducesResponseType(typeof(Result<UserProfileDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<UserProfileDto>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<Result<UserProfileDto>>> EditUserProfileAsync(
           [FromForm] EditUserProfileCommand request)
    {

        var result = await Mediator.Send(request);

        return CustomResult(result);
    }
    [Guard]
    [HttpPost("change-email")]
    [ProducesResponseType(typeof(Result<ChangeUserEmailDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<ChangeUserEmailDto>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<Result<ChangeUserEmailDto>>> ChangeUserEmailAsync(
    [FromBody] ChangeUserEmailCommand request)
    {
        var result = await Mediator.Send(request);
        return CustomResult(result);
    }

    [Guard]
    [HttpPost("verify-change-email")]
    [ProducesResponseType(typeof(Result<VerifyChangeUserEmailDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<VerifyChangeUserEmailDto>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<Result<VerifyChangeUserEmailDto>>> VerifyChangeUserEmailAsync(
    [FromBody] VerifyChangeUserEmailCommand request)
    {
        var result = await Mediator.Send(request);
        return CustomResult(result);
    }

    [Guard]
    [HttpPost("change-password")]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<Result<bool>>> ChangePasswordAsync(
    [FromBody] ChangeUserPasswordCommand request)
    {
        var result = await Mediator.Send(request);
        return CustomResult(result);
    }

    [Guard]
    [HttpGet("me/addresses")]
    [ProducesResponseType(typeof(Result<List<AddressDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCurrentUserAddressesAsync()
        => CustomResult(await Mediator.Send(new GetUserAddressesQuery()));

    [Guard]
    [HttpGet("me/addresses/{addressId:guid}")]
    [ProducesResponseType(typeof(Result<AddressDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<AddressDto>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Result<AddressDto>), StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> GetAddressAsync([FromRoute] Guid addressId)
        => CustomResult(await Mediator.Send(new GetAddressQuery() { Id = addressId }));


    [Guard]
    [HttpGet("me/addresses/main")]
    [ProducesResponseType(typeof(Result<AddressDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<AddressDto>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetMainAddressAsync()
        => CustomResult(await Mediator.Send(new GetMainAddressQuery()));

    [Guard]
    [HttpPost("me/addresses")]
    [ProducesResponseType(typeof(Result<Guid>), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateNewAddressAsync([FromBody] CreateAddressCommand command)
        => CustomResult(await Mediator.Send(command));

    [Guard]
    [HttpPut("me/addresses")]
    [ProducesResponseType(typeof(Result<AddressDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<AddressDto>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Result<AddressDto>), StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> UpdateAddressAsync([FromBody] UpdateAddressCommand command)
        => CustomResult(await Mediator.Send(command));

    [Guard]
    [HttpPost("me/addresses/set-main/{addressId:guid}")]
    [ProducesResponseType(typeof(Result<AddressDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<AddressDto>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Result<AddressDto>), StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> SetMainAddressAsync([FromRoute] Guid addressId)
        => CustomResult(await Mediator.Send(new SetMainAddressCommand { Id = addressId }));

    [Guard]
    [HttpDelete("me/addresses/{addressId:guid}")]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> DeleteAddressAsync([FromRoute] Guid addressId)
        => CustomResult(await Mediator.Send(new DeleteAddressCommand { Id = addressId }));

    [Guard]
    [HttpDelete("me/delete-account")]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteAccountAsync()
        => CustomResult(await Mediator.Send(new DeleteAccountCommand()));
}