 using System.ComponentModel.DataAnnotations.Schema;

namespace MILS.Domain;

[Table("Incident", Schema = "dbo")]
public class Incident
{
    public int Id { get; set; }
    public int TrackingId { get; set; }
    public int CategoryId { get; set; }
    [ForeignKey("TrackingId")]
    public Tracking Tracking { get; set; }
    [ForeignKey("CategoryId")]
    public Category Category{ get; set; }
    public List<IncidentLine> IncidentLines { get; set; }
}