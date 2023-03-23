using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using guacactings.Context;

namespace guacactings.Models;

public class Enterprise
{
    public int? Id { get; set; }
    public string? Name { get; set; }
    public string? Siret { get; set; }
    public string? Description { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    [ForeignKey("AddressId")]
    public int? AddressId { get; set; }
    
    [JsonIgnore (Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Address? Address { get; set; }
    
    public ICollection<Site>? Sites { get; set; }
}

public class EnterpriseRegistryDto
{
    [Required] public string? Name { get; set; }
    [Required] public string? Siret { get; set; }
    [Required] public string? Description { get; set; }
    
    [Required]
    [EmailAddress]
    [UniqueEmail(ErrorMessage = "Cette adresse email est déjà utilisée.")]
    public string? Email { get; set; }
    
    [Required]
    [Phone]
    public string? Phone { get; set; }
    
    [ForeignKey("AddressId")]
    public int? AddressId { get; set; }
}

public class EnterpriseUpdateDto
{
    public string? Name { get; set; }
    public string? Siret { get; set; }
    public string? Description { get; set; }
    
    [EmailAddress]
    [UniqueEmail(ErrorMessage = "Cette adresse email est déjà utilisée.")]
    public string? Email { get; set; }
    
    [Phone]
    public string? Phone { get; set; }
    
    [ForeignKey("AddressId")]
    public int? AddressId { get; set; }
}

public class UniqueEmailAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var context = (DataContext) validationContext.GetService(typeof(DataContext))!;
        return context.Enterprises.Any(x => x.Email == (string?)value) ? new ValidationResult(ErrorMessage) : ValidationResult.Success;
    }
}