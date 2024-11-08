﻿using GreenSphere.Application.Abstractions;
using MediatR;

namespace GreenSphere.Application.Features.Auth.Requests.Commands;
public sealed class Disable2FACommand : IRequest<Result<string>>
{
    public string Email { get; set; } = null!;
}
