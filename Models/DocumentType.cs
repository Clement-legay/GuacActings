using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace guacactings.Models;

public class DocumentType
{
    public int? Id { get; set; }
    public string? Name { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    [JsonIgnore (Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ICollection<Document>? Documents { get; set; }
}

public class DocumentTypeRegistryDto
{
    [Required] public string? Name { get; set; }
}