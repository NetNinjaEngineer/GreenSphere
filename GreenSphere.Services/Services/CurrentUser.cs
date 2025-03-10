﻿using GreenSphere.Application.Helpers;
using GreenSphere.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace GreenSphere.Services.Services;
public sealed class CurrentUser(IHttpContextAccessor contextAccessor) : ICurrentUser
{
    public string Id => contextAccessor.HttpContext?.User.FindFirstValue(CustomClaimTypes.Uid)!;
    public string Email => contextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Email)!;
}
