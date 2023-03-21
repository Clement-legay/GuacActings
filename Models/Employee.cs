using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace guacactings.Models;

public class Employee
{
    public int? Id { get; set; }
    public string? Firstname { get; set; }
    public string? Lastname { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public DateTime? BirthDate { get; set; }

    public int? AddressId { get; set; }

    [ForeignKey("AddressId")]
    public Address? Address { get; set; }
    
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

public class EmployeeRegistryDto
{
    [Required] public string? Firstname { get; set; }
    [Required] public string? Lastname { get; set; }
    [Required] public string? Email { get; set; }
    [Required] public DateTime BirthDate { get; set; }
    public string? Phone { get; set; }
    public int? AddressId { get; set; }
}

public class EmployeeUpdateDto
{ 
    public string? Firstname { get; set; }
    public string? Lastname { get; set; }
    public string? Email { get; set; }
    public DateTime? BirthDate { get; set; }
    public string? Phone { get; set; }
    public int? AddressId { get; set; }
}