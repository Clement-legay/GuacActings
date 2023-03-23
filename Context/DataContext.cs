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
        modelBuilder.ApplyConfiguration(new DocumentTypeEntityConfiguration());
        modelBuilder.ApplyConfiguration(new DocumentEntityConfiguration());
        modelBuilder.ApplyConfiguration(new EnterpriseEntityConfiguration());
        modelBuilder.ApplyConfiguration(new SiteEntityConfiguration());
    }
    
    #region Properties
    
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<DocumentType> DocumentTypes { get; set; }
    public DbSet<Document> Documents { get; set; }
    public DbSet<Enterprise> Enterprises { get; set; }
    public DbSet<Site> Sites { get; set; }

    #endregion
}