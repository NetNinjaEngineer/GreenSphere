using GreenSphere.Application.Bases;
using GreenSphere.Application.Features.Users.DTOs;
using MediatR;

namespace GreenSphere.Application.Features.Users.Requests.Queries;
public sealed class GetUserPrivacySettingQuery : IRequest<Result<PrivacySettingListDto>>
{
    public string UserId { get; set; } = null!;
}
