using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MILS.Domain;

[Table("IncidentLine", Schema = "dbo")]
public class IncidentLine
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int IncidentId { get; set; }
    public int? LineId { get; set; }
    public bool Yes { get; set; }
    public bool No { get; set; }
    public bool ToEvalute { get; set; }
    public string? Comment { get; set; }
    [ForeignKey("IncidentId")]
    public Incident Incident { get; set; }
    [ForeignKey("LineId")]
    public virtual Line Line { get; set; }
}