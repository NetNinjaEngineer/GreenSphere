using GreenSphere.Domain.Entities.Identity;
using GreenSphere.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GreenSphere.Persistence.Repositories;

public sealed class AddressRepository(ApplicationDbContext context)
    : GenericRepository<Address>(context), IAddressRepository
{
    public async Task<List<Address>> GetUserAddressesAsync(string userId) =>
        await context.Addresses
            .Where(a => a.UserId == userId)
            .OrderByDescending(a => a.IsMain)
            .ToListAsync();


    public async Task<Address?> GetMainAddressAsync(string userId) =>
        await context.Addresses
            .Where(a => a.UserId == userId && a.IsMain)
            .FirstOrDefaultAsync();

    public async Task<int> GetUserAddressesCountAsync(string userId) =>
        await context.Addresses
            .Where(a => a.UserId == userId)
            .CountAsync();

    public async Task ResetMainAddressesAsync(string userId)
    {
        var mainAddresses = await context.Addresses
            .Where(a => a.UserId == userId && a.IsMain)
            .ToListAsync();

        foreach (var address in mainAddresses)
        {
            address.IsMain = false;
            context.Addresses.Update(address);
        }

        await context.SaveChangesAsync();
    }
}