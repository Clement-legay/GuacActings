using guacactings.Models;

namespace guacactings.Services;

public interface IAddressService
{
    // Get all addresses
    Task<IEnumerable<Address>> GetAddresses(int page = 1, int rows = 10);
    
    // Create a new address
    Task<Address?> AddAddress(AddressRegistryDto address);
    
    // Update an address
    Task<Address?> UpdateAddress(AddressRegistryDto address, int id);
}