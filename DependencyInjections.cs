using System.Security.Claims;
using guacactings.Services;

namespace guacactings;

public static class DependencyInjections
{
    public static void AddInjections(this IServiceCollection services)
    {
        services.AddScoped<IEmployeeService, EmployeeService>();
        services.AddScoped<IAddressService, AddressService>();
        services.AddScoped<IDocumentTypeService, DocumentTypeService>();
        services.AddScoped<IDocumentService, DocumentService>();
        services.AddScoped<ISiteService, SiteService>();
        services.AddScoped<IServiceService, ServiceService>();
        services.AddScoped<IAdministratorService, AdministratorService>();
        
        services.AddAuthorization(options =>
        {
            options.AddPolicy("visitor", policy => policy.RequireClaim(ClaimTypes.Role, "visitor"));
            options.AddPolicy("admin", policy => policy.RequireClaim(ClaimTypes.Role, "admin"));
        });
    }
}