using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MILS.Domain;

[Table("Category", Schema = "dbo")]
public class Category
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string ShortLabel { get; set; }
    public string LongLabel { get; set; }
    public string ImprovementProcess { get; set; }
    public string? ReferenceCc { get; set; }
    public List<Line> Lines { get; set; }
}