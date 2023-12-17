using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using MILS.Domain;

namespace MILS.Web.Models.Tracking;

public class CreateOrUpdateTrackingFormVM
{
    public int Id { get; set; }
    public DateTime IssueDate { get; set; }
    public string IssuerName { get; set; }
    [ValidateNever]
    public List<SelectListItem> Categories { get; set; }
    [ValidateNever]
    public List<int> CategoryIds { get; set; }
    [ValidateNever]
    public List<int> CategoryIdsSelected { get; set; }
    [ValidateNever]
    public List<Incident> Incidents { get; set; }
}