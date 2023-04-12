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
    
    [ForeignKey("AddressId")]
    [JsonIgnore (Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Address? Address { get; set; }
    
    public ICollection<Document>? Documents { get; set; }
    
    [ForeignKey("ServiceId")]
    [JsonIgnore (Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Service? Service { get; set; }
    
    [ForeignKey("SiteId")]
    [JsonIgnore (Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Site? Site { get; set; }
    
    [JsonIgnore (Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Administrator? Administrator { get; set; }
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