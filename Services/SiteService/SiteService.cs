using System.Security.Claims;
using guacactings.Context;
using guacactings.Models;
using Microsoft.EntityFrameworkCore;

namespace guacactings.Services;

public class SiteService : ISiteService
{
    #region Fields

    private readonly DataContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    #endregion

    #region Constructor

    public SiteService(DataContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    #endregion

    #region Methods

    // Get all enterprises
    public async Task<IEnumerable<Site>?> GetSites(int page, int rows)
    {
        var sites = await _context.Sites.ToListAsync();
        var sitesPaged = sites.Skip((page - 1) * rows).Take(rows);
        return sitesPaged;
    }

    // Get an enterprise by id
    public async Task<Site?> GetSiteById(int id)
    {
        var site = await _context.Sites.FindAsync(id);
        return site ?? null;
    }

    // Add an enterprise
    public async Task<Site?> AddSite(SiteRegistryDto site)
    {
        var adminIdString = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (adminIdString is null) return null;
        var adminId = int.Parse(adminIdString);
        
        var newSite = new Site()
        {
            Name = site.Name,
            Description = site.Description, 
            AddressId = site.AddressId,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
            CreatedBy = adminId
        };

        var addedSite = _context.Sites.Add(newSite).Entity;
        await _context.SaveChangesAsync();
        return addedSite;
    }

    // Update an enterprise
    public async Task<Site?> UpdateSite(SiteUpdateDto site, int id)
    {
        var adminIdString = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (adminIdString is null) return null;
        var adminId = int.Parse(adminIdString);
        
        var updateSite = await _context.Sites.FindAsync(id);
        if (updateSite is null) return null;

        updateSite.Name = site.Name ?? updateSite.Name;
        updateSite.Description = site.Description ?? updateSite.Description;
        updateSite.AddressId = site.AddressId ?? updateSite.AddressId;
        updateSite.UpdatedAt = DateTime.Now;
        updateSite.UpdatedBy = adminId;
        
        var updatedSite = _context.Sites.Update(updateSite).Entity;
        await _context.SaveChangesAsync();
        return updatedSite;
    }

    // Delete an enterprise
    public async Task<Site?> DeleteSite(int id)
    {
        var deleteSite = await _context.Sites.FindAsync(id);
        if (deleteSite is null) return null;

        var deletedSite = _context.Sites.Remove(deleteSite).Entity;
        await _context.SaveChangesAsync();
        return deletedSite;
    }

    #endregion
}