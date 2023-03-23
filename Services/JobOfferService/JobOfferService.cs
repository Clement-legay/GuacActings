using guacactings.Context;
using guacactings.Models;
using Microsoft.EntityFrameworkCore;

namespace guacactings.Services;

public class JobOfferService : IJobOfferService
{
    #region Fields

    private readonly DataContext _context;

    #endregion

    #region Constructor

    public JobOfferService(DataContext context)
    {
        _context = context;
    }

    #endregion

    #region Methods

    // Get all job offers
    public async Task<IEnumerable<JobOffer>> GetJobOffers(int page, int rows)
    {
        var jobOffers = await _context.JobOffers.ToListAsync();
        var jobOffersPaged = jobOffers.Skip((page - 1) * rows).Take(rows);
        return jobOffersPaged;
    }

    #endregion
}