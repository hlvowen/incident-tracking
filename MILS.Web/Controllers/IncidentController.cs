using Microsoft.AspNetCore.Mvc;
using MILS.Domain;
using MILS.Infrastructure.Repositories.Interfaces;

namespace MILS.Web.Controllers;

public class IncidentController : Controller
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IIncidentRepository _incidentRepository;
    public IncidentController(ICategoryRepository categoryRepository, IIncidentRepository incidentRepository)
    {
        _categoryRepository = categoryRepository;
        _incidentRepository = incidentRepository;
    }
    
    [HttpGet]
    public IActionResult Index(int trackingId)
    {
        var incidents = _incidentRepository.GetAllByTrackingId(trackingId)
                                                       .ToList();
        return View(incidents);
    }

    [HttpPost]
    public IActionResult Edit(List<Incident> incidents)
    {
        return Ok();
    }
}