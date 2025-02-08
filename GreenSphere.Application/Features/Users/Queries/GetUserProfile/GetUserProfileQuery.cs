using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Users;
using MediatR;

namespace GreenSphere.Application.Features.Users.Queries.GetUserProfile;
public sealed class GetUserProfileQuery : IRequest<Result<UserProfileDto>>
{
}
