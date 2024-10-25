﻿using GreenSphere.Application.Abstractions;
using MediatR;

namespace GreenSphere.Application.Features.Roles.Requests.Queries;

public class GetRoleClaimsQuery : IRequest<Result<IEnumerable<string>>>
{
    public string RoleName { get; set; } = string.Empty;
}
