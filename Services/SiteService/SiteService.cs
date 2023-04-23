using System.Security.Claims;
using System.Text;
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
        var sites = await _context.Sites
            .Include(s => s.Employees)
            .Include(s => s.Address)
            .ToListAsync();
        var sitesPaged = sites.Skip((page - 1) * rows).Take(rows);
        return sitesPaged;
    }

    // Get an enterprise by id
    public async Task<Site?> GetSiteById(int id)
    {
        var site = await _context.Sites
            .Include(s => s.Employees)
            .Include(s => s.Address)
            .FirstOrDefaultAsync(s => s.Id == id);
        return site ?? null;
    }

    // Add an enterprise
    public async Task<Site?> AddSite(SiteRegistryDto site)
    {
        var adminIdString = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (adminIdString is null) return null;
        var adminId = int.Parse(adminIdString);
        
        var pictureUrl = site.Picture is null ? null : await SaveFileToDisk(site.Picture, site.Name!);
        
        var newSite = new Site()
        {
            Name = site.Name,
            Description = site.Description, 
            AddressId = site.AddressId,
            PictureUrl = pictureUrl,
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

        if (site.Picture is not null)
        {
            if (updateSite.PictureUrl is not null)
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), updateSite.PictureUrl);
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }
            updateSite.PictureUrl = await SaveFileToDisk(site.Picture, (site.Name ?? updateSite.Name)!);
        }

        updateSite.Name = site.Name ?? updateSite.Name;
        updateSite.Description = site.Description ?? updateSite.Description;
        updateSite.AddressId = site.AddressId ?? updateSite.AddressId;
        updateSite.UpdatedAt = DateTime.Now;
        updateSite.UpdatedBy = adminId;
        
        var updatedSite = _context.Sites.Update(updateSite).Entity;
        await _context.SaveChangesAsync();
        return updatedSite;
    }
    
    // Get Site image File
    public async Task<Site?> GetSiteByLink(string siteName, string fileName)
    {
        var fileUrl = Path.Combine("files", siteName, fileName);
        
        var siteByPicture = await _context.Sites.FirstOrDefaultAsync(d => d.PictureUrl == fileUrl);

        return siteByPicture ?? null;
        
    }

    // Delete an enterprise
    public async Task<Site?> DeleteSite(int id)
    {
        var deleteSite = await _context.Sites.Include(s => s.Employees).FirstOrDefaultAsync(s => s.Id == id);
        if (deleteSite is null || deleteSite.Employees?.Count > 0) return null;

        if (deleteSite.PictureUrl is not null)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), deleteSite.PictureUrl);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                var directory = Path.GetDirectoryName(filePath);
                if (Directory.Exists(directory!))
                {
                    if (Directory.GetFiles(directory).Length == 0)
                    {
                        Directory.Delete(directory);
                    }
                }
            }
        }
        
        var deletedSite = _context.Sites.Remove(deleteSite).Entity;
        await _context.SaveChangesAsync();
        return deletedSite;
    }
    
    private async Task<string> SaveFileToDisk(IFormFile file, string siteName)
    {
        siteName = siteName.Replace(" ", "_").Replace("è", "e").Replace("é", "e")
            .Replace("à", "a").Replace("ù", "u").Replace("ò", "o")
            .Replace("ì", "i").Replace("ç", "c");

        var uniqueFileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "files" , siteName, uniqueFileName);
        
        Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);
        
        var stream = new FileStream(filePath, FileMode.Create);
        await file.CopyToAsync(stream);
        
        var link = Path.Combine("files", siteName, uniqueFileName);
        return link;
    }

    #endregion
}