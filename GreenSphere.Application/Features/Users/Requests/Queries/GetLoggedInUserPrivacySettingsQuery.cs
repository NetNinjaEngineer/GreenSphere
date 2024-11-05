using GreenSphere.Application.Bases;
using GreenSphere.Application.Features.Users.DTOs;
using MediatR;

namespace GreenSphere.Application.Features.Users.Requests.Queries;

public class GetLoggedInUserPrivacySettingsQuery : IRequest<Result<PrivacySettingListDto>>
{

}