using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Users;
using GreenSphere.Domain.Enumerations;
using MediatR;

namespace GreenSphere.Application.Features.Users.Commands.EditUserProfile;
public sealed class EditUserProfileCommand : IRequest<Result<UserProfileDto>>
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public Gender? Gender { get; set; }
    public DateOnly? DateOfBirth { get; set; }
}