﻿using System.Security.Claims;

namespace GreenSphere.Application.Interfaces.Services;
public interface ICurrentUser
{
    string Id { get; }
    string Email { get; }
    ClaimsPrincipal? GetUser();
    Task<bool> IsExistsAsync(string email);
}
