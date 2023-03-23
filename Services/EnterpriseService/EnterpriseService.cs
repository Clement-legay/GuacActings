using guacactings.Context;
using guacactings.Models;
using Microsoft.EntityFrameworkCore;

namespace guacactings.Services;

public class EnterpriseService : IEnterpriseService
{
    #region Fields

    private readonly DataContext _context;

    #endregion
    
    #region Constructor

    public EnterpriseService(DataContext context)
    {
        _context = context;
    }
    
    #endregion
    
    #region Methods

    // Get all enterprises
    public async Task<IEnumerable<Enterprise>?> GetEnterprises(int page, int rows)
    {
        var enterprises = await _context.Enterprises.ToListAsync();
        var enterprisesPaged = enterprises.Skip((page - 1) * rows).Take(rows);
        return enterprisesPaged;
    }
    
    // Get an enterprise by id
    public async Task<Enterprise?> GetEnterpriseById(int id)
    {
        var enterprise = await _context.Enterprises.FindAsync(id);
        return enterprise ?? null;
    }

    // Add an enterprise
    public async Task<Enterprise?> AddEnterprise(EnterpriseRegistryDto enterprise)
    {
        var newEnterprise = new Enterprise()
        {
            Name = enterprise.Name,
            Description = enterprise.Description,
            Email = enterprise.Email,
            Phone = enterprise.Phone,
            Siret = enterprise.Siret,
            AddressId = enterprise.AddressId,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };

        var addedEnterprise = _context.Enterprises.Add(newEnterprise).Entity;
        await _context.SaveChangesAsync();
        return addedEnterprise;
    }

    // Update an enterprise
    public async Task<Enterprise?> UpdateEnterprise(EnterpriseUpdateDto enterprise, int id)
    {
        var updateEnterprise = await _context.Enterprises.FindAsync(id);
        if (updateEnterprise is null) return null;

        updateEnterprise.Name = enterprise.Name ?? updateEnterprise.Name;
        updateEnterprise.Description = enterprise.Description ?? updateEnterprise.Description;
        updateEnterprise.Email = enterprise.Email ?? updateEnterprise.Email;
        updateEnterprise.Phone = enterprise.Phone ?? updateEnterprise.Phone;
        updateEnterprise.Siret = enterprise.Siret ?? updateEnterprise.Siret;
        updateEnterprise.AddressId = enterprise.AddressId ?? updateEnterprise.AddressId;
        updateEnterprise.UpdatedAt = DateTime.Now;

        var updatedEnterprise = _context.Enterprises.Update(updateEnterprise).Entity;
        await _context.SaveChangesAsync();
        return updatedEnterprise;
    }

    // Delete an enterprise
    public async Task<Enterprise?> DeleteEnterprise(int id)
    {
        var deleteEnterprise = await _context.Enterprises.FindAsync(id);
        if (deleteEnterprise is null) return null;

        var deletedEnterprise = _context.Enterprises.Remove(deleteEnterprise).Entity;
        await _context.SaveChangesAsync();
        return deletedEnterprise;
    }

    #endregion
}