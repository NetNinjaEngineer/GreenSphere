using GreenSphere.Application.Bases;
using GreenSphere.Application.Features.Users.DTOs;
using MediatR;

namespace GreenSphere.Application.Features.Users.Requests.Commands;
public sealed class AssignUserPrivacyCommand : IRequest<Result<string>>
{
    public string UserId { get; set; } = null!;
    public AssignPrivacySettingsRequestDto AssignPrivacySettingsRequest { get; set; } = null!;
}
