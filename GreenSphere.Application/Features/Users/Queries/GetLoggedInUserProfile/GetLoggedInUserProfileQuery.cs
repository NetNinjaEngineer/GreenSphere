using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Users;
using MediatR;

namespace GreenSphere.Application.Features.Users.Queries.GetLoggedInUserProfile;

public class GetLoggedInUserProfileQuery : IRequest<Result<UserProfileDto>>
{

}