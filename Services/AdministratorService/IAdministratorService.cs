using guacactings.Models;

namespace guacactings.Services;

public interface IAdministratorService
{
    // Get all administrators
    Task<IEnumerable<Administrator>> GetAdministrators(int page = 1, int rows = 10);
    
    // Get an administrator by id
    Task<Administrator?> GetAdministratorById(int id);
    
    // Create a new administrator
    Task<Administrator?> AddAdministrator(AdministratorRegistryDto administrator);
    
    // Update an administrator password
    Task<Administrator?> UpdatePasswordAdministrator(int id, AdministratorUpdatePasswordDto administrator);
    
    // Update an administrator email
    Task<Administrator?> UpdateEmailAdministrator(int id, AdministratorUpdateEmailDto administrator);
    
    // Delete an administrator
    Task<Administrator?> DeleteAdministrator(int id);
    
    // Login an administrator
    Task<Administrator?> LoginAdministrator(AdministratorLoginDto administrator);
    
    // Persist administrator connection
    Task<Administrator?> PersistConnection(AdministratorPersistDto administratorPersistDto);
}