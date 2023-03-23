using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace guacactings.Models;

public class Enterprise
{
    public int? Id { get; set; }
    public string? Name { get; set; }
    public string? Siret { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    [ForeignKey("AddressId")]
    public int? AddressId { get; set; }
    
    [JsonIgnore (Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Address? Address { get; set; }
    
    public ICollection<Site>? Sites { get; set; }
}