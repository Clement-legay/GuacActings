using guacactings.Models;

namespace guacactings.Services;

public interface ISiteService
{
    // Get all sites
    Task<IEnumerable<Site>?> GetSites(int page, int rows);
    
    // Get site by id
    Task<Site?> GetSiteById(int id);
    
    // Get Site image File
    Task<Site?> GetSiteByLink(string siteName, string fileName);
    
    // Add a new Site
    Task<Site?> AddSite(SiteRegistryDto site);
    
    // Update a site
    Task<Site?> UpdateSite(SiteUpdateDto site, int id);
    
    // Delete a Site
    Task<Site?> DeleteSite(int id);
}