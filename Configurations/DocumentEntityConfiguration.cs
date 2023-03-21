using guacactings.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace guacactings.Configurations;

public class DocumentEntityConfiguration : IEntityTypeConfiguration<Document>
{
    public void Configure(EntityTypeBuilder<Document> builder)
    {
        // Table name
        builder.ToTable("document");
        
        // Primary Key
        builder.HasKey(item => item.Id);
    }
}