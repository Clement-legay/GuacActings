using System.ComponentModel.DataAnnotations.Schema;

namespace guacactings.Models;

public class Employee
{
    public int Id { get; set; }
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public int AddressId { get; set; }

    [ForeignKey("AddressId")]
    public Address Address { get; set; }
}