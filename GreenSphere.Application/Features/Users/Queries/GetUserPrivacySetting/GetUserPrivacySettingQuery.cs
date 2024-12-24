using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Users;
using MediatR;

namespace GreenSphere.Application.Features.Users.Queries.GetUserPrivacySetting;
public sealed class GetUserPrivacySettingQuery : IRequest<Result<PrivacySettingListDto>>
{
    public string UserId { get; set; } = null!;
}
