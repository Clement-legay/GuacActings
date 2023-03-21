using System.Collections;
using System.Text.Json.Serialization;

namespace guacactings.Models;

public class DocumentType
{
    public int? Id { get; set; }
    public string? Name { get; set; }
    
    [JsonIgnore]
    public ICollection<Document>? Documents { get; set; }
}