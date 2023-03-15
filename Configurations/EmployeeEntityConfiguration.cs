using guacactings.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace guacactings.Configurations;

public class EmployeeEntityConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        // table name
        builder.ToTable("employee");
        
        // primary key
        builder.HasKey(item => item.Id);
        
        // foreign key
        builder.HasOne(item => item.Address);
    }
}