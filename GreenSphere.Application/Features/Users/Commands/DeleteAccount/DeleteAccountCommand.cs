using GreenSphere.Application.Bases;
using MediatR;

namespace GreenSphere.Application.Features.Users.Commands.DeleteAccount;
public sealed class DeleteAccountCommand : IRequest<Result<bool>>
{
}
