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
        modelBuilder.ApplyConfiguration(new AdministratorEntityConfiguration());
        modelBuilder.ApplyConfiguration(new AddressEntityConfiguration());
        modelBuilder.ApplyConfiguration(new SiteEntityConfiguration());
        modelBuilder.ApplyConfiguration(new ServiceEntityConfiguration());
        modelBuilder.ApplyConfiguration(new DocumentTypeEntityConfiguration());
        modelBuilder.ApplyConfiguration(new DocumentEntityConfiguration());
        modelBuilder.ApplyConfiguration(new EmployeeEntityConfiguration());
        
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
        
        for (int i = 0; i < 1004; i++)
        {
            modelBuilder.Entity<Address>().HasData(new Address
            {
                Id = i + 1,
                Street = Faker.Address.StreetAddress(),
                City = Faker.Address.City(),
                PostalCode = Faker.Address.ZipCode(),
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                CreatedBy = 1,
                UpdatedBy = 1
            });
        }
        
        modelBuilder.Entity<Site>().HasData(
            new Site()
                {
                    Id = 1,
                    Name = "Siège administratif",
                    Description = "Siège administratif de la compagnie",
                    AddressId = 1,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    CreatedBy = 1,
                    UpdatedBy = 1
                },
            new Site()
                {
                    Id = 2,
                    Name = "Site de production",
                    Description = "Usine de production de la compagnie, spécialisée dans les sandwichs",
                    AddressId = 2,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    CreatedBy = 1,
                    UpdatedBy = 1
                },
            new Site()
            {
                Id = 3,
                Name = "Site de production",
                Description = "Usine de production de la compagnie, spécialisée dans les pizzas",
                AddressId = 3,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                CreatedBy = 1,
                UpdatedBy = 1
            },
            new Site()
            {
                Id = 4,
                Name = "Site de production",
                Description = "Usine de production de la compagnie, spécialisée dans les salades",
                AddressId = 4,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                CreatedBy = 1,
                UpdatedBy = 1
            },
            new Site()
            {
                Id = 5,
                Name = "Site de production",
                Description = "Usine de production de la compagnie, spécialisée dans les desserts",
                AddressId = 5,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                CreatedBy = 1,
                UpdatedBy = 1
            }
        );
        
        modelBuilder.Entity<Service>().HasData(
            new Service()
            {
                Id = 1,
                Name = "Comptable",
                Description = "Comptabilité au sein de l'entreprise",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                CreatedBy = 1,
                UpdatedBy = 1
            },
            new Service()
            {
                Id = 2,
                Name = "Production",
                Description = "Opérateur de production dans les usines",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                CreatedBy = 1,
                UpdatedBy = 1
            },
            new Service()
            {
                Id = 3,
                Name = "Accueil",
                Description = "Accueil des clients",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                CreatedBy = 1,
                UpdatedBy = 1
            },
            new Service()
            {
                Id = 4,
                Name = "Informatique",
                Description = "Maintenance informatique",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                CreatedBy = 1,
                UpdatedBy = 1
            },
            new Service()
            {
                Id = 5,
                Name = "Commercial",
                Description = "Commercialisation des produits",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                CreatedBy = 1,
                UpdatedBy = 1
            }
        );
        
        for (int i = 0; i < 1004; i++)
        {
            modelBuilder.Entity<Employee>().HasData(new Employee
            {
                Id = i + 2,
                Firstname = Faker.Name.First(),
                Lastname = Faker.Name.Last(),
                Username = Faker.Internet.UserName(),
                Email = Faker.Internet.Email(),
                Phone = Faker.Phone.Number(),
                AddressId = Faker.RandomNumber.Next(1, 1004),
                BirthDate = new DateTime(Faker.RandomNumber.Next(1970, 2000), Faker.RandomNumber.Next(1, 12), Faker.RandomNumber.Next(1, 28)),
                ServiceId = Faker.RandomNumber.Next(1, 5),
                SiteId = Faker.RandomNumber.Next(1, 5),
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                CreatedBy = 1,
                UpdatedBy = 1
            });
        }
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