using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MILS.Domain;

[Table("Tracking", Schema = "dbo")]
public class Tracking
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public DateTime IssueDate { get; set; }
    public string IssuerName { get; set; }

    public List<Incident> Incidents { get; set; }
    // public string CurrentState { get; set; }
    // public string FutureState { get; set; }
    // public string ReasonForModification { get; set; }
    // public string ProductDtId { get; set; }
    // public string ProductModel { get; set; }
    // public string ArticlesConcerned { get; set; }
    // public string TechnicalAgreement { get; set; }
    // public DateTime TechnicalAgreementDate { get; set; }
    // public string TechnicalInitials { get; set; }
    // public string QualityAgreement { get; set; }
    // public DateTime QualityAgreementDate { get; set; }
    // public string QualityInitials { get; set; }
    // public string ModifiedBy { get; set; }
    // public short ImpactRh { get; set; }
    // public short ImpactConception { get; set; }
    // public short ImpactProduction { get; set; }
    // public short ImpactLogistic { get; set; }
    // public short ImpactCommercial { get; set; }
    // public short ImpactOnRegulatoryQuality { get; set; }
    // public string SignificantModification { get; set; }
    // public string SubstantialModification { get; set; }
    // public string ClientsInfo { get; set; }
    // public string ActionPlan { get; set; }
    // public string VerificationResult { get; set; }
    // public DateTime VerificationDate { get; set; }
    // public string CheckerName { get; set; }
    // public DateTime ClosingDate { get; set; }
    // public string ClosingBy { get; set; }
}