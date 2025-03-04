using GreenSphere.Domain.Entities.Identity;
using GreenSphere.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GreenSphere.Persistence.Repositories;

public sealed class AddressRepository(ApplicationDbContext context)
    : GenericRepository<Address>(context), IAddressRepository
{
    public async Task<List<Address>> GetUserAddressesAsync(string userId) =>
        await Context.Addresses
            .Where(a => a.UserId == userId)
            .OrderByDescending(a => a.IsMain)
            .ToListAsync();


    public async Task<Address?> GetMainAddressAsync(string userId) =>
        await Context.Addresses
            .Where(a => a.UserId == userId && a.IsMain)
            .FirstOrDefaultAsync();

    public async Task<int> GetUserAddressesCountAsync(string userId) =>
        await Context.Addresses
            .Where(a => a.UserId == userId)
            .CountAsync();

    public async Task ResetMainAddressesAsync(string userId)
    {
        var mainAddresses = await Context.Addresses
            .Where(a => a.UserId == userId && a.IsMain)
            .ToListAsync();

        foreach (var address in mainAddresses)
        {
            address.IsMain = false;
            Context.Addresses.Update(address);
        }

        await Context.SaveChangesAsync();
    }
}