using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Users;
using GreenSphere.Application.Features.Users.Commands.CreateAddress;
using GreenSphere.Application.Features.Users.Commands.UpdateAddress;

namespace GreenSphere.Application.Interfaces.Services;

public interface IAddressService
{
    Task<Result<List<AddressDto>>> GetUserAddressesAsync();
    Task<Result<AddressDto>> GetAddressByIdAsync(Guid id);
    Task<Result<AddressDto>> GetMainAddressAsync();
    Task<Result<Guid>> CreateAddressAsync(CreateAddressCommand command);
    Task<Result<AddressDto>> UpdateAddressAsync(UpdateAddressCommand command);
    Task<Result<AddressDto>> SetMainAddressAsync(Guid id);
    Task<Result<bool>> DeleteAddressAsync(Guid id);
}