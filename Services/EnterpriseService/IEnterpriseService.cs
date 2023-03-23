using guacactings.Models;

namespace guacactings.Services;

public interface IEnterpriseService
{
    // Get all enterprises
    Task<IEnumerable<Enterprise>?> GetEnterprises(int page, int rows);
    
    // Get an enterprise by id
    Task<Enterprise?> GetEnterpriseById(int id);
    
    // Add an enterprise
    Task<Enterprise?> AddEnterprise(EnterpriseRegistryDto enterprise);
    
    // Update an enterprise
    Task<Enterprise?> UpdateEnterprise(EnterpriseUpdateDto enterprise, int id);
    
    // Delete an enterprise
    Task<Enterprise?> DeleteEnterprise(int id);
}