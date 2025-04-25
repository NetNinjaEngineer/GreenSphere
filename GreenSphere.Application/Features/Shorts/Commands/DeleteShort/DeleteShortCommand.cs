using GreenSphere.Application.Bases;
using MediatR;

namespace GreenSphere.Application.Features.Shorts.Commands.DeleteShort;
public sealed class DeleteShortCommand : IRequest<Result<bool>>
{
    public Guid Id { get; set; }
}
