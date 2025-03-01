using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Users;
using MediatR;

namespace GreenSphere.Application.Features.Users.Commands.UpdateAddress;

public sealed class UpdateAddressCommand : IRequest<Result<AddressDto>>
{
    public Guid Id { get; set; }
    public string? BuildingName { get; set; }
    public string? Floor { get; set; }
    public string? Street { get; set; }
    public string? AdditionalDirections { get; set; }
    public string? AddressLabel { get; set; }
    public bool IsMain { get; set; }
}