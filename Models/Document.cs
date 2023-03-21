using System.ComponentModel.DataAnnotations.Schema;

namespace guacactings.Models;

public class Document
{
    public int? Id { get; set; }
    public string? Name { get; set; }
    public string? Link { get; set; }
    public int? DocumentTypeId { get; set; }
    public int? EmployeeId { get; set; }
    
    [ForeignKey("DocumentTypeId")]
    public DocumentType? DocumentType { get; set; }
    
    [ForeignKey("EmployeeId")]
    public Employee? Employee { get; set; }
}