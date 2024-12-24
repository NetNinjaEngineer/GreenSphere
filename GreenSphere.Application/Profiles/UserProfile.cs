using AutoMapper;
using GreenSphere.Application.DTOs.Users;
using GreenSphere.Application.Features.Auth.Commands.Register;
using GreenSphere.Domain.Entities;

namespace GreenSphere.Application.Profiles;
public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<RegisterCommand, ApplicationUser>();
        CreateMap<ApplicationUser, UserProfileDto>();
    }
}
