using guacactings.Models;

namespace guacactings.Services;

public interface IEnterpriseService
{
    Task<IEnumerable<Enterprise>?> GetEnterprises(int page, int rows);
}