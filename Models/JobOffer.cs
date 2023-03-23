using System.ComponentModel.DataAnnotations.Schema;

namespace guacactings.Models;

public class JobOffer
{
    public int? Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public int MoneyPerHour { get; set; }
    public int HoursPerWeek { get; set; }
    public int? SiteId { get; set; }

    [ForeignKey("SiteId")]
    public Site? Site { get; set; }
    
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}