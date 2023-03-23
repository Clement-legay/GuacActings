using guacactings.Models;

namespace guacactings.Services;

public interface ISiteService
{
    Task<IEnumerable<Site>?> GetSites(int page, int rows);
}