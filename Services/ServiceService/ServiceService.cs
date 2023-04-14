using System.Security.Claims;
using guacactings.Context;
using guacactings.Models;
using Microsoft.EntityFrameworkCore;

namespace guacactings.Services;

public class ServiceService : IServiceService
{
    #region Fields

    private readonly DataContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    #endregion
    
    #region Constructor
    
    public ServiceService(DataContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }
    
    #endregion

    #region Methods

    // Get all services
    public async Task<IEnumerable<Service>?> GetServices(int page = 1, int rows = 10)
    {
        var services = await _context.Services.ToListAsync();
        var servicesPaged = services.Skip((page - 1) * rows).Take(rows);
        return servicesPaged;
    }
    
    // Get a service by id
    public async Task<Service?> GetServiceById(int id)
    {
        var service = await _context.Services.Include(s => s.Employees).FirstOrDefaultAsync(s => s.Id == id);
        return service ?? null;
    }
    
    // Add a service
    public async Task<Service?> AddService(ServiceRegistryDto service)
    {
        var adminIdString = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (adminIdString is null) return null;
        var adminId = int.Parse(adminIdString);
        
        var newService = new Service()
        {
            Name = service.Name,
            Description = service.Description,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
            CreatedBy = adminId
        };
        
        var addedService = _context.Services.Add(newService).Entity;
        await _context.SaveChangesAsync();
        return addedService;
    }
    
    // Update a service
    public async Task<Service?> UpdateService(ServiceUpdateDto service, int id)
    {
        var adminIdString = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (adminIdString is null) return null;
        var adminId = int.Parse(adminIdString);
        
        var serviceToUpdate = await _context.Services.FindAsync(id);
        if (serviceToUpdate == null) return null;
        
        serviceToUpdate.Name = service.Name;
        serviceToUpdate.Description = service.Description;
        serviceToUpdate.UpdatedAt = DateTime.Now;
        serviceToUpdate.UpdatedBy = adminId;
        
        var updatedService = _context.Services.Update(serviceToUpdate).Entity;
        await _context.SaveChangesAsync();
        return updatedService;
    }
    
    // Delete a service
    public async Task<Service?> DeleteService(int id)
    {
        var serviceToDelete = await _context.Services.FindAsync(id);
        if (serviceToDelete == null) return null;
        
        var deletedService = _context.Services.Remove(serviceToDelete).Entity;
        await _context.SaveChangesAsync();
        return deletedService;
    }

    #endregion
}