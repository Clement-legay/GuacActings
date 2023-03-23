using guacactings.Context;
using guacactings.Models;
using Microsoft.EntityFrameworkCore;

namespace guacactings.Services;

public class SiteService : ISiteService
{
    #region Fields

    private readonly DataContext _context;

    #endregion

    #region Constructor

    public SiteService(DataContext context)
    {
        _context = context;
    }

    #endregion

    #region Methods

    public async Task<IEnumerable<Site>?> GetSites(int page, int rows)
    {
        var sites = await _context.Sites.ToListAsync();
        if (sites is null) return null;
        
        var sitesPaged = sites.Skip((page - 1) * rows).Take(rows);
        return sitesPaged;
    }

    #endregion
}