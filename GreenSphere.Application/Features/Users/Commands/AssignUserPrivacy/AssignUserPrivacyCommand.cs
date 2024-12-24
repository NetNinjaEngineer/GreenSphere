using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Users;
using MediatR;

namespace GreenSphere.Application.Features.Users.Commands.AssignUserPrivacy;
public sealed class AssignUserPrivacyCommand : IRequest<Result<string>>
{
    public string UserId { get; set; } = null!;
    public AssignPrivacySettingsRequestDto AssignPrivacySettingsRequest { get; set; } = null!;
}
