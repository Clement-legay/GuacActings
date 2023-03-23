using guacactings.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace guacactings.Configurations;

public class EnterpriseEntityConfiguration : IEntityTypeConfiguration<Enterprise>
{
    public void Configure(EntityTypeBuilder<Enterprise> builder)
    {
        builder.ToTable("enterprise");
        
        builder.HasKey(e => e.Id);
    }
}