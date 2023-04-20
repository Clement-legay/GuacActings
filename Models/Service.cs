using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace guacactings.Models;

public class Service
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int? CreatedBy { get; set; }
    public int? UpdatedBy { get; set; }
    
    [ForeignKey("CreatedBy")]
    [JsonIgnore]
    public virtual Administrator? CreatedByAdministrator { get; set; }
    
    [ForeignKey("UpdatedBy")]
    [JsonIgnore]
    public virtual Administrator? UpdatedByAdministrator { get; set; }
    [JsonIgnore]
    public ICollection<Employee>? Employees { get; set; }
    [JsonPropertyName("EmployeesCount")]
    public int EmployeesCount => Employees?.Count ?? 0;
}

public class ServiceRegistryDto
{
    [Required] public string? Name { get; set; }
    [Required] public string? Description { get; set; }
}

public class ServiceUpdateDto
{
    public string? Name { get; set; }
    public string? Description { get; set; }
}