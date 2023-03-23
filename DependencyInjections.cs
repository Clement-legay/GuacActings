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
        services.AddScoped<IEnterpriseService, EnterpriseService>();
        services.AddScoped<IJobService, JobService>();
        services.AddScoped<IJobOfferService, JobOfferService>();
    }
}