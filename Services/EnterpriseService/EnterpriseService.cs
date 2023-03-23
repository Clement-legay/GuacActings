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

    public async Task<IEnumerable<Enterprise>?> GetEnterprises(int page, int rows)
    {
        var enterprises = await _context.Enterprises.ToListAsync();
        if (enterprises is null) return null;
        
        var enterprisesPaged = enterprises.Skip((page - 1) * rows).Take(rows);
        return enterprisesPaged;
    }

    #endregion
}