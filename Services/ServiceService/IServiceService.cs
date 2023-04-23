using guacactings.Models;

namespace guacactings.Services;

public interface IServiceService
{
    // Get all services
    Task<IEnumerable<Service>?> GetServices(int page, int rows);
    
    // Get a service by id
    Task<Service?> GetServiceById(int id);
    
    // Get service by link
    Task<Service?> GetServiceByLink(string serviceName, string fileName);
    
    // Add a service
    Task<Service?> AddService(ServiceRegistryDto service);
    
    // Update a service
    Task<Service?> UpdateService(ServiceUpdateDto service, int id);
    
    // Delete a service
    Task<Service?> DeleteService(int id);
}