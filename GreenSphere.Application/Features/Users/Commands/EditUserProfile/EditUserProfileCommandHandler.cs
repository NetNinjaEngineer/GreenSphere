using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Users;
using GreenSphere.Application.Interfaces.Identity;
using MediatR;

namespace GreenSphere.Application.Features.Users.Commands.EditUserProfile;
public sealed class EditUserProfileCommandHandler(
    IUserPrivacyService privacyService)
    : IRequestHandler<EditUserProfileCommand, Result<UserProfileDto>>
{
    public async Task<Result<UserProfileDto>> Handle(
        EditUserProfileCommand request,
        CancellationToken cancellationToken)
    {
        return await privacyService.EditUserProfileAsync(request);
    }
}