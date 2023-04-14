using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using guacactings.Context;

namespace guacactings.Models;

public class DocumentType
{
    public int? Id { get; set; }
    public string? Name { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int? CreatedBy { get; set; }
    public int? UpdatedBy { get; set; }
    
    [ForeignKey("CreatedBy")]
    [JsonIgnore (Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Administrator? CreatedByAdministrator { get; set; }
    [ForeignKey("UpdatedBy")]
    [JsonIgnore (Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Administrator? UpdatedByAdministrator { get; set; }
    public ICollection<Document>? Documents { get; set; }
}

public class DocumentTypeRegistryDto
{
    // unique name
    [Required]
    [UniqueName(ErrorMessage = "Ce type de document existe déjà.")]
    public string? Name { get; set; }
}

public class UniqueNameAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var context = (DataContext) validationContext.GetService(typeof(DataContext))!;
        return context.DocumentTypes.Any(x => x.Name == (string?)value) ? new ValidationResult(ErrorMessage) : ValidationResult.Success;
    }
}