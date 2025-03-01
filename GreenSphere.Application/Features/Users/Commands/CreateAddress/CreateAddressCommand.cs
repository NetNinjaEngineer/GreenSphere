using GreenSphere.Application.Bases;
using MediatR;

namespace GreenSphere.Application.Features.Users.Commands.CreateAddress;

public sealed class CreateAddressCommand : IRequest<Result<Guid>>
{
    public string? BuildingName { get; set; }
    public string? Floor { get; set; }
    public string? Street { get; set; }
    public string? AdditionalDirections { get; set; }
    public string? AddressLabel { get; set; }
    public bool IsMain { get; set; }
}