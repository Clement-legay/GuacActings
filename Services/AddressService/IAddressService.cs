using guacactings.Models;

namespace guacactings.Services;

public interface IAddressService
{
    // Get all addresses
    Task<IEnumerable<Address>> GetAddresses();
    
    // Create a new address
    Task<Address> AddAddress(Address address);
}