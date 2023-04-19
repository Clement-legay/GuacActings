using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace guacactings.Models;

public class Site
{
    public int? Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    
    public string? PictureUrl { get; set; }

    [ForeignKey("AddressId")]
    public int? AddressId { get; set; }

    [JsonIgnore (Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Address? Address { get; set; }
    
    [JsonIgnore]
    public ICollection<Employee>? Employees { get; set; }
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
}

public class SiteRegistryDto
{
    [Required] public string? Name { get; set; }
    [Required] public string? Description { get; set; }
    
    public IFormFile? Picture { get; set; }
    
    [ForeignKey("AddressId")]
    public int? AddressId { get; set; }
}

public class SiteUpdateDto
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public IFormFile? Picture { get; set; }
    [ForeignKey("AddressId")]
    public int? AddressId { get; set; }
}