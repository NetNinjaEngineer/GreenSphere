using AutoMapper;
using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Users;
using GreenSphere.Application.Features.Users.Commands.CreateAddress;
using GreenSphere.Application.Features.Users.Commands.UpdateAddress;
using GreenSphere.Application.Interfaces.Services;
using GreenSphere.Domain.Entities.Identity;
using GreenSphere.Domain.Interfaces;
using Microsoft.Extensions.Localization;
using System.Net;

namespace GreenSphere.Services.Services;

public sealed class AddressService(
    IMapper mapper,
    IAddressRepository addressRepository,
    ICurrentUser currentUser,
    IStringLocalizer<AddressService> localizer) : IAddressService
{
    public async Task<Result<List<AddressDto>>> GetUserAddressesAsync()
    {
        var addresses = await addressRepository.GetUserAddressesAsync(currentUser.Id);
        var addressDtos = mapper.Map<List<AddressDto>>(addresses);
        return Result<List<AddressDto>>.Success(addressDtos);
    }

    public async Task<Result<AddressDto>> GetAddressByIdAsync(Guid id)
    {
        var address = await addressRepository.GetByIdAsync(id);

        if (address == null)
            return Result<AddressDto>.Failure(HttpStatusCode.NotFound, localizer["AddressNotFound"]);

        if (address.UserId != currentUser.Id)
            return Result<AddressDto>.Failure(HttpStatusCode.Forbidden, localizer["NotYourAddress"]);

        var addressDto = mapper.Map<AddressDto>(address);
        return Result<AddressDto>.Success(addressDto);
    }

    public async Task<Result<AddressDto>> GetMainAddressAsync()
    {
        var mainAddress = await addressRepository.GetMainAddressAsync(currentUser.Id);

        if (mainAddress == null)
            return Result<AddressDto>.Failure(HttpStatusCode.NotFound, localizer["NoMainAddressFound"]);

        var addressDto = mapper.Map<AddressDto>(mainAddress);
        return Result<AddressDto>.Success(addressDto);
    }

    public async Task<Result<Guid>> CreateAddressAsync(CreateAddressCommand command)
    {
        var address = new Address
        {
            Id = Guid.NewGuid(),
            UserId = currentUser.Id,
            BuildingName = command.BuildingName,
            Floor = command.Floor,
            Street = command.Street,
            AdditionalDirections = command.AdditionalDirections,
            AddressLabel = command.AddressLabel,
            IsMain = command.IsMain
        };

        if (address.IsMain || await addressRepository.GetUserAddressesCountAsync(currentUser.Id) == 0)
        {
            address.IsMain = true;
            await addressRepository.ResetMainAddressesAsync(currentUser.Id);
        }

        addressRepository.Create(address);
        await addressRepository.SaveChangesAsync();

        var addressDto = mapper.Map<AddressDto>(address);
        return Result<Guid>.Success(addressDto.Id);
    }

    public async Task<Result<AddressDto>> UpdateAddressAsync(UpdateAddressCommand command)
    {
        var address = await addressRepository.GetByIdAsync(command.Id);

        if (address == null)
            return Result<AddressDto>.Failure(HttpStatusCode.NotFound, localizer["AddressNotFound"]);

        if (address.UserId != currentUser.Id)
            return Result<AddressDto>.Failure(HttpStatusCode.Forbidden, localizer["NotYourAddress"]);

        address.BuildingName = command.BuildingName;
        address.Floor = command.Floor;
        address.Street = command.Street;
        address.AdditionalDirections = command.AdditionalDirections;
        address.AddressLabel = command.AddressLabel;

        if (command.IsMain && !address.IsMain)
        {
            await addressRepository.ResetMainAddressesAsync(currentUser.Id);
            address.IsMain = true;
        }

        addressRepository.Update(address);
        await addressRepository.SaveChangesAsync();

        var addressDto = mapper.Map<AddressDto>(address);
        return Result<AddressDto>.Success(addressDto);
    }

    public async Task<Result<AddressDto>> SetMainAddressAsync(Guid id)
    {
        var address = await addressRepository.GetByIdAsync(id);

        if (address == null)
            return Result<AddressDto>.Failure(HttpStatusCode.NotFound, localizer["AddressNotFound"]);

        if (address.UserId != currentUser.Id)
            return Result<AddressDto>.Failure(HttpStatusCode.Forbidden, localizer["NotYourAddress"]);

        if (address.IsMain)
        {
            var addressDto = mapper.Map<AddressDto>(address);
            return Result<AddressDto>.Success(addressDto);
        }

        await addressRepository.ResetMainAddressesAsync(currentUser.Id);
        address.IsMain = true;

        addressRepository.Update(address);
        await addressRepository.SaveChangesAsync();

        var result = mapper.Map<AddressDto>(address);
        return Result<AddressDto>.Success(result);
    }

    public async Task<Result<bool>> DeleteAddressAsync(Guid id)
    {
        var address = await addressRepository.GetByIdAsync(id);

        if (address == null)
            return Result<bool>.Failure(HttpStatusCode.NotFound, localizer["AddressNotFound"]);

        if (address.UserId != currentUser.Id)
            return Result<bool>.Failure(HttpStatusCode.Forbidden, localizer["NotYourAddress"]);

        var wasMain = address.IsMain;

        addressRepository.Delete(address);
        await addressRepository.SaveChangesAsync();

        // If the deleted address was the main one, set a new main address if any exists
        if (wasMain)
        {
            await SetNewMainAddressIfExistsAsync();
        }

        return Result<bool>.Success(true);
    }

    private async Task SetNewMainAddressIfExistsAsync()
    {
        var addresses = await addressRepository.GetUserAddressesAsync(currentUser.Id);
        if (addresses.Count > 0)
        {
            var newMainAddress = addresses.First();
            newMainAddress.IsMain = true;
            addressRepository.Update(newMainAddress);
            await addressRepository.SaveChangesAsync();
        }
    }
}