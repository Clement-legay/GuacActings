using Microsoft.EntityFrameworkCore;
using guacactings.Models;
using guacactings.Configurations;

namespace guacactings.Context;

public class DataContext : DbContext
{
    #region Constructor

    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    #endregion

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new EmployeeEntityConfiguration());
        modelBuilder.ApplyConfiguration(new AddressEntityConfiguration());
    }
    
    #region Properties
    
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Address> Addresses { get; set; }
    
    #endregion
}