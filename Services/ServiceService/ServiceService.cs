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
        var services = await _context.Services
            .Include(s => s.Employees)
            .ToListAsync();
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
        
        var pictureUrl = service.Picture is null ? null : await SaveFileToDisk(service.Picture, service.Name!);

        var newService = new Service()
        {
            Name = service.Name,
            Description = service.Description,
            PictureUrl = pictureUrl,
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
        
        if (service.Picture is not null)
        {
            if (serviceToUpdate.PictureUrl is not null)
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), serviceToUpdate.PictureUrl);
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }
            serviceToUpdate.PictureUrl = await SaveFileToDisk(service.Picture, (service.Name ?? serviceToUpdate.Name)!);
        }
        
        serviceToUpdate.Name = service.Name;
        serviceToUpdate.Description = service.Description;
        serviceToUpdate.UpdatedAt = DateTime.Now;
        serviceToUpdate.UpdatedBy = adminId;
        
        var updatedService = _context.Services.Update(serviceToUpdate).Entity;
        await _context.SaveChangesAsync();
        return updatedService;
    }
    
    // Get Site image File
    public async Task<Service?> GetServiceByLink(string serviceName, string fileName)
    {
        var fileUrl = Path.Combine("files", serviceName, fileName);
        
        var serviceByLink = await _context.Services.FirstOrDefaultAsync(s => s.PictureUrl == fileUrl);

        return serviceByLink ?? null;
    }
    
    // Delete a service
    public async Task<Service?> DeleteService(int id)
    {
        var serviceToDelete = await _context.Services
            .Include(s => s.Employees)
            .FirstOrDefaultAsync(s => s.Id == id);
        if (serviceToDelete == null || serviceToDelete.EmployeesCount > 0) return null;
        
        if (serviceToDelete.PictureUrl is not null)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), serviceToDelete.PictureUrl);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                var directory = Path.GetDirectoryName(filePath);
                if (Directory.Exists(directory!))
                {
                    if (Directory.GetFiles(directory).Length == 0)
                    {
                        Directory.Delete(directory);
                    }
                }
            }
        }
        
        var deletedService = _context.Services.Remove(serviceToDelete).Entity;
        await _context.SaveChangesAsync();
        return deletedService;
    }
    
    private async Task<string> SaveFileToDisk(IFormFile file, string serviceName)
    {
        serviceName = serviceName.Replace(" ", "_").Replace("è", "e").Replace("é", "e")
            .Replace("à", "a").Replace("ù", "u").Replace("ò", "o")
            .Replace("ì", "i").Replace("ç", "c");

        var uniqueFileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "files" , serviceName, uniqueFileName);
        
        Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);
        
        var stream = new FileStream(filePath, FileMode.Create);
        await file.CopyToAsync(stream);
        
        var link = Path.Combine("files", serviceName, uniqueFileName);
        return link;
    }

    #endregion
}