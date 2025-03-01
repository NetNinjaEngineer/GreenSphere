using GreenSphere.Domain.Entities.Identity;

namespace GreenSphere.Domain.Interfaces;

public interface IAddressRepository : IGenericRepository<Address>
{
    Task<List<Address>> GetUserAddressesAsync(string userId);
    Task<Address?> GetMainAddressAsync(string userId);
    Task<int> GetUserAddressesCountAsync(string userId);
    Task ResetMainAddressesAsync(string userId);
}