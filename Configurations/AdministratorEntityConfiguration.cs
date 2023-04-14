using guacactings.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace guacactings.Configurations;

public class AdministratorEntityConfiguration : IEntityTypeConfiguration<Administrator>
{
    public void Configure(EntityTypeBuilder<Administrator> builder)
    {
        builder.ToTable("administrator");
        
        builder.HasOne(a => a.Employee)
            .WithOne(e => e.Administrator)
            .HasForeignKey<Administrator>(a => a.EmployeeId);
        
        builder.HasKey(item => item.Id);
    }
}