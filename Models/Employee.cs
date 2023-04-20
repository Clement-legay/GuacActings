using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace guacactings.Models;

public class Employee
{
    public int Id { get; set; }
    public string? Firstname { get; set; }
    public string? Lastname { get; set; }
    public string? Username { get; init; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? HomePhone { get; set; }
    public DateTime BirthDate { get; set; }
    public int? AddressId { get; set; }
    public int? ServiceId { get; set; }
    public int? SiteId { get; set; }
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
    
    [ForeignKey("AddressId")]
    public virtual Address? Address { get; set; }
    
    [JsonIgnore]
    public virtual ICollection<Document>? Documents { get; set; }
    
    [ForeignKey("ServiceId")]
    [JsonIgnore]
    public virtual Service? Service { get; set; }
    
    [ForeignKey("SiteId")]
    [JsonIgnore]
    public virtual Site? Site { get; set; }
    
    [JsonIgnore]
    public virtual Administrator? Administrator { get; set; }
    
    [JsonPropertyName("PictureUrl")]
    public string? PictureUrl => Documents?.FirstOrDefault(d => d.Name!.ToLower().Contains("photo"))?.Link ?? null;
    
    [JsonPropertyName("SiteName")]
    public string? SiteName => Site?.Name ?? null;
    
    [JsonPropertyName("ServiceName")]
    public string? ServiceName => Service?.Name ?? null;
    
}

public class EmployeeRegistryDto
{
    [Required] public string? Firstname { get; set; }
    [Required] public string? Lastname { get; set; }
    [Required] public string? Email { get; set; }
    [Required] public DateTime BirthDate { get; set; }
    public string? Phone { get; set; }
    public string? HomePhone { get; set; }
    
    [ForeignKey("AddressId")]
    public int? AddressId { get; set; }
    
    [ForeignKey("ServiceId")]
    public int? ServiceId { get; set; }
    
    [ForeignKey("SiteId")]
    public int? SiteId { get; set; }
}

public class EmployeeUpdateDto
{ 
    public string? Firstname { get; set; }
    public string? Lastname { get; set; }
    public string? Email { get; set; }
    public DateTime? BirthDate { get; set; }
    public string? Phone { get; set; }
    public string? HomePhone { get; set; }
    
    [ForeignKey("AddressId")]
    public int? AddressId { get; set; }
    
    [ForeignKey("ServiceId")]
    public int? ServiceId { get; set; }
    
    [ForeignKey("SiteId")]
    public int? SiteId { get; set; }
}