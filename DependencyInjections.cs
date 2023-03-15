using guacactings.Services;

namespace guacactings;

public static class DependencyInjections
{
    public static void AddInjections(this IServiceCollection services)
    {
        services.AddScoped<IEmployeeService, EmployeeService>();
        services.AddScoped<IAddressService, AddressService>();
    }
}