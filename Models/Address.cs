using System.Text.Json.Serialization;

namespace guacactings.Models;

public class Address
{
    public int Id { get; set; }
    public string? Street { get; set; }
    public string? City { get; set; }
    public string? PostalCode { get; set; }
    
    [JsonIgnore]
    public ICollection<Employee>? Employees { get; set; }
    
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public Address()
    {
        Employees = new List<Employee>();
    }
}

public class AddressRegistryDto
{
    public string? Street { get; set; }
    public string? City { get; set; }
    public string? PostalCode { get; set; }
}