using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Users;
using GreenSphere.Application.Interfaces.Services;
using MediatR;

namespace GreenSphere.Application.Features.Users.Commands.EditUserProfile;
public sealed class EditUserProfileCommandHandler(
    IUserService userService)
    : IRequestHandler<EditUserProfileCommand, Result<UserProfileDto>>
{
    public async Task<Result<UserProfileDto>> Handle(
        EditUserProfileCommand request,
        CancellationToken cancellationToken)
    {
        return await userService.EditUserProfileAsync(request);
    }
}