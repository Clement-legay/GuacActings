using System.Runtime.InteropServices.JavaScript;
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
    
    // Get all addresses
    public async Task<IEnumerable<Address>> GetAddresses(int page, int rows)
    {
        var addresses = await _context.Addresses.ToListAsync();
        var addressesPaged =  addresses.Skip((page - 1) * rows).Take(rows);
        return addressesPaged;
    }
    
    // Get an address by id
    public async Task<Address?> GetAddressById(int id)
    {
        var address = await _context.Addresses.FindAsync(id);
        return address ?? null;
    }
    
    // Create a new address
    public async Task<Address?> AddAddress(AddressRegistryDto address)
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
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
        };

        var saveAddress = _context.Addresses.Add(newAddress).Entity;
        await _context.SaveChangesAsync();
        return saveAddress;
    }
    
    // Update an address
    public async Task<Address?> UpdateAddress(AddressRegistryDto address, int id)
    {
        if (address is null)
        {
            return null;
        }

        var updateAddress = await _context.Addresses.FindAsync(id);
        if (updateAddress is null)
        {
            return null;
        }

        updateAddress.Street = address.Street ?? updateAddress.Street;
        updateAddress.City = address.City ?? updateAddress.City;
        updateAddress.PostalCode = address.PostalCode ?? updateAddress.PostalCode;
        updateAddress.UpdatedAt = DateTime.Now;

        var updatedAddress = _context.Addresses.Update(updateAddress).Entity;
        await _context.SaveChangesAsync();
        return updatedAddress;
    }

    public async Task<Address?> DeleteAddress(int id)
    {
        var deleteAddress = await _context.Addresses.FindAsync(id);
        if (deleteAddress is null)
        {
            return null;
        }

        var deletedAddress = _context.Addresses.Remove(deleteAddress).Entity;
        await _context.SaveChangesAsync();
        return deletedAddress;
    }

    #endregion
}