﻿using AutoMapper;
using GreenSphere.Application.Features.Auth.Requests.Commands;
using GreenSphere.Application.Interfaces.Identity.Entities;

namespace GreenSphere.Identity.Profiles;
public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<RegisterCommand, ApplicationUser>();
    }
}
