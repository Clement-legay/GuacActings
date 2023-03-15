using guacactings.Context;
using guacactings.Models;
using Microsoft.EntityFrameworkCore;

namespace guacactings.Services;

public class AddressService : IAddressService
{
    #region Fields

    private readonly DataContext _context;

    #endregion
    
    #region Constructor
    
    public AddressService(DataContext context)
    {
        _context = context;
    }
    
    #endregion
    
    #region Methods
    
    // Get all employees
    public async Task<IEnumerable<Address>> GetAddresses()
    {
        return await _context.Addresses.ToListAsync();
    }
    
    // Create a new address
    public async Task<Address> AddAddress(Address address)
    {
        if (address is null)
        {
            return null;
        }

        var newAddress = new Address
        {
            Street = address.Street,
            City = address.City,
            PostalCode = address.PostalCode,
        };

        var saveAddress = _context.Addresses.Add(newAddress).Entity;
        await _context.SaveChangesAsync();
        return saveAddress;
    }

    #endregion
}