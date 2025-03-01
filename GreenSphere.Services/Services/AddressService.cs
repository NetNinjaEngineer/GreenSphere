using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Users;
using GreenSphere.Application.Features.Users.Commands.CreateAddress;
using GreenSphere.Application.Features.Users.Commands.UpdateAddress;
using GreenSphere.Application.Interfaces.Services;

namespace GreenSphere.Services.Services;

public sealed class AddressService : IAddressService
{
    public Task<Result<List<AddressDto>>> GetUserAddressesAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Result<AddressDto>> GetAddressByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Result<AddressDto>> GetMainAddressAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Result<Guid>> CreateAddressAsync(CreateAddressCommand command)
    {
        throw new NotImplementedException();
    }

    public Task<Result<AddressDto>> UpdateAddressAsync(UpdateAddressCommand command)
    {
        throw new NotImplementedException();
    }

    public Task<Result<AddressDto>> SetMainAddressAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Result<bool>> DeleteAddressAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}