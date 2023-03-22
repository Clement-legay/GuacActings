using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace guacactings.Models;

public class Document
{
    public int? Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? ContentType { get; set; }
    public string? Link { get; set; }
    public int? DocumentTypeId { get; set; }
    public int? EmployeeId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    [ForeignKey("DocumentTypeId")]
    [JsonIgnore (Condition = JsonIgnoreCondition.WhenWritingNull)]
    public DocumentType? DocumentType { get; set; }
    
    [ForeignKey("EmployeeId")]
    [JsonIgnore (Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Employee? Employee { get; set; }
}

public class DocumentRegistryDto
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    [Required] public IFormFile? File { get; set; }
    
    [Required]
    [ForeignKey("EmployeeId")]
    public int? EmployeeId { get; set; }
    
    [Required] 
    [ForeignKey("DocumentTypeId")]
    public int? DocumentTypeId { get; set; }
}

public class DocumentUpdateDto
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public IFormFile? File { get; set; }
    
    [ForeignKey("DocumentTypeId")]
    public int? DocumentTypeId { get; set; }
}