using guacactings.Models;

namespace guacactings.Services;

public interface IJobService
{
    // Get all jobs
    Task<IEnumerable<Job>> GetJobs(int page, int rows);
}