using System.Security.Claims;
using guacactings.Context;
using guacactings.Models;
using Microsoft.EntityFrameworkCore;

namespace guacactings.Services;

public class AddressService : IAddressService
{
    #region Fields

    private readonly DataContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    #endregion
    
    #region Constructor
    
    public AddressService(DataContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
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
        var adminIdString = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (adminIdString is null) return null;
        var adminId = int.Parse(adminIdString.ToString());
        

        var newAddress = new Address
        {
            Street = address.Street,
            City = address.City,
            PostalCode = address.PostalCode,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
            CreatedBy = adminId
        };

        var saveAddress = _context.Addresses.Add(newAddress).Entity;
        await _context.SaveChangesAsync();
        return saveAddress;
    }
    
    // Update an address
    public async Task<Address?> UpdateAddress(AddressUpdateDto address, int id)
    {
        var adminIdString = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (adminIdString is null) return null;
        var adminId = int.Parse(adminIdString);

        var updateAddress = await _context.Addresses.FindAsync(id);
        if (updateAddress is null)
        {
            return null;
        }

        updateAddress.Street = address.Street ?? updateAddress.Street;
        updateAddress.City = address.City ?? updateAddress.City;
        updateAddress.PostalCode = address.PostalCode ?? updateAddress.PostalCode;
        updateAddress.UpdatedAt = DateTime.Now;
        updateAddress.UpdatedBy = adminId;

        var updatedAddress = _context.Addresses.Update(updateAddress).Entity;
        await _context.SaveChangesAsync();
        return updatedAddress;
    }

    public async Task<Address?> DeleteAddress(int id)
    {
        var deleteAddress = await _context.Addresses.Include(a => a.Employees).FirstOrDefaultAsync(a => a.Id == id);
        if (deleteAddress is null || deleteAddress.Employees!.Count > 0)
        {
            return null;
        }

        var deletedAddress = _context.Addresses.Remove(deleteAddress).Entity;
        await _context.SaveChangesAsync();
        return deletedAddress;
    }

    #endregion
}