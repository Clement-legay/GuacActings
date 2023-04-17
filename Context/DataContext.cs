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
        modelBuilder.ApplyConfiguration(new SiteEntityConfiguration());
        modelBuilder.ApplyConfiguration(new ServiceEntityConfiguration());
        modelBuilder.ApplyConfiguration(new AdministratorEntityConfiguration());
        
        // create a default employee
        modelBuilder.Entity<Employee>().HasData(new Employee
        {
            Id = 1,
            Firstname = "John",
            Lastname = "Doe",
            Username = "admin",
            BirthDate = DateTime.Now,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        });
        
        // create a default admin account
        modelBuilder.Entity<Administrator>().HasData(new Administrator
        {
            Id = 1,
            Password = BCrypt.Net.BCrypt.HashPassword("123456"),
            Email = "admin@admin.fr",
            EmployeeId = 1,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        });
    }

    #region Properties
    
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<DocumentType> DocumentTypes { get; set; }
    public DbSet<Document> Documents { get; set; }
    public DbSet<Site> Sites { get; set; }
    public DbSet<Service> Services { get; set; }
    public DbSet<Administrator> Administrators { get; set; }

    #endregion
}