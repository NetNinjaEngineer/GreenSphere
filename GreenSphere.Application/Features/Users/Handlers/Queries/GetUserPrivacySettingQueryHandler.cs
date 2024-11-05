﻿using GreenSphere.Application.Bases;
using GreenSphere.Application.Features.Users.DTOs;
using GreenSphere.Application.Features.Users.Requests.Queries;
using GreenSphere.Application.Interfaces.Identity;
using MediatR;

namespace GreenSphere.Application.Features.Users.Handlers.Queries;
public sealed class GetUserPrivacySettingQueryHandler(IUserPrivacyService privacyService) : IRequestHandler<GetUserPrivacySettingQuery, Result<PrivacySettingListDto>>
{
    public async Task<Result<PrivacySettingListDto>> Handle(
        GetUserPrivacySettingQuery request,
        CancellationToken cancellationToken)
        => await privacyService.GetUserPrivacySettingsAsync(request.UserId);
}