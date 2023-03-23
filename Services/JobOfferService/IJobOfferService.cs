using guacactings.Models;

namespace guacactings.Services;

public interface IJobOfferService
{
    // Get all job offers
    Task<IEnumerable<JobOffer>> GetJobOffers(int page, int rows);
}