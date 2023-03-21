using guacactings.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace guacactings.Configurations;

public class DocumentTypeEntityConfiguration : IEntityTypeConfiguration<DocumentType>
{
    public void Configure(EntityTypeBuilder<DocumentType> builder)
    {
        // Table name
        builder.ToTable("document_type");

        // Primary Key
        builder.HasKey(item => item.Id);
    }
}