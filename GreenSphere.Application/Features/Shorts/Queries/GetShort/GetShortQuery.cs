using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Shorts;
using MediatR;

namespace GreenSphere.Application.Features.Shorts.Queries.GetShort;

public sealed class GetShortQuery : IRequest<Result<ShortDto>>
{
    public Guid Id { get; set; }
}
