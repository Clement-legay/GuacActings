using guacactings.Context;
using guacactings.Models;
using Microsoft.EntityFrameworkCore;

namespace guacactings.Services;

public class JobService : IJobService
{
    #region Fields

    private readonly DataContext _context;
    
    #endregion

    #region Constructor

    public JobService(DataContext context)
    {
        _context = context;
    }

    #endregion

    #region Methods

    // Get all jobs
    public async Task<IEnumerable<Job>> GetJobs(int page, int rows)
    {
        var jobs = await _context.Jobs.ToListAsync();
        var jobsPaged = jobs.Skip((page - 1) * rows).Take(rows);
        return jobsPaged;
    }

    #endregion
}