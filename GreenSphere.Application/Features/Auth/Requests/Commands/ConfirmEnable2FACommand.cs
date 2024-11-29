﻿using GreenSphere.Application.Abstractions;
using MediatR;

namespace GreenSphere.Application.Features.Auth.Requests.Commands;

public sealed class ConfirmEnable2FaCommand : IRequest<Result<string>>
{
    public string Email { get; set; } = null!;
    public string Code { get; set; } = null!;
}