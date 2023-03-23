using System.ComponentModel.DataAnnotations.Schema;

namespace guacactings.Models;

public class Job
{
    public int? Id { get; set; }
    public int? JobOfferId { get; set; }
    public int? EmployeeId { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    
    [ForeignKey("JobOfferId")]
    public JobOffer? JobOffer { get; set; }
    
    [ForeignKey("EmployeeId")]
    public Employee? Employee { get; set; }
    
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}