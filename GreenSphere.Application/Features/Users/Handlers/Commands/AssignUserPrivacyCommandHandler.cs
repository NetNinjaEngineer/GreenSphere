using GreenSphere.Application.Bases;
using GreenSphere.Application.Features.Users.Requests.Commands;
using GreenSphere.Application.Interfaces.Identity;
using MediatR;

namespace GreenSphere.Application.Features.Users.Handlers.Commands;
public sealed class AssignUserPrivacyCommandHandler(IUserPrivacyService privacyService) : IRequestHandler<AssignUserPrivacyCommand, Result<string>>
{
    public async Task<Result<string>> Handle(AssignUserPrivacyCommand request, CancellationToken cancellationToken)
        => await privacyService.AssignPrivacyToUserAsync(request);
}
