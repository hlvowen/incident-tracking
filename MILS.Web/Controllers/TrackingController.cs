using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MILS.Domain;
using MILS.Infrastructure.Repositories.Interfaces;
using MILS.Web.Models.Tracking;

namespace MILS.Web.Controllers
{
    public class TrackingController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ITrackingRepository _trackingRepository;
        private readonly IIncidentLineRepository _incidentLineRepository;

        public TrackingController(ICategoryRepository categoryRepository, 
                                  ITrackingRepository trackingRepository,
                                  IIncidentLineRepository incidentLineRepository)
        {
            _categoryRepository = categoryRepository;
            _trackingRepository = trackingRepository;
            _incidentLineRepository = incidentLineRepository;
        }
        
        public IActionResult Index()
        {
            var trackings = _trackingRepository.GetAll();
            return View(trackings);
        }

        [HttpGet]
        public IActionResult CreateOrUpdate(int? id)
        {
            CreateOrUpdateTrackingFormVM model = new CreateOrUpdateTrackingFormVM();
            model.Categories = GetCategoryDropdownList();

            if (id.HasValue && id.Value > 0)
            {
                Tracking tracking = _trackingRepository.GetBy(id.Value);

                List<int> existingCategoryIds = tracking.Incidents
                                                        .Select(i => i.CategoryId)
                                                        .ToList();

                foreach (var categoryAlreadySelected in model.Categories.Where(item => existingCategoryIds.Contains(Int32.Parse(item.Value))))
                {
                    categoryAlreadySelected.Selected = true;
                    categoryAlreadySelected.Disabled = true;
                }

                model.CategoryIdsSelected = existingCategoryIds;

                model.Incidents = tracking.Incidents;
                model.Id = tracking.Id;
                model.IssueDate = tracking.IssueDate;
                model.IssuerName = tracking.IssuerName;
            }
            
            return View(model);
        }
        
        [HttpPost]
        public IActionResult CreateOrUpdate(CreateOrUpdateTrackingFormVM model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id <= 0)
                {
                    CreateTracking(model);
                    TempData["success"] = "Un nouveau suivi a été ajouté";
                }
                else
                {
                    UpdateTracking(model);
                    
                    if (model.Incidents is not null && model.Incidents.Count() > 0)
                    {
                        foreach (var incident in model.Incidents)
                        {
                            _incidentLineRepository.Update(incident.IncidentLines);
                        }
                    }
                    
                    TempData["success"] = "Modification réussie";
                }
                
                return RedirectToAction(nameof(Index));
            }

            model.Categories = GetCategoryDropdownList();
            TempData["error"] = "Une erreur est survenue";
            return View(model);
        }

        private void CreateTracking(CreateOrUpdateTrackingFormVM model)
        {
            Tracking tracking = new Tracking()
            {
                IssueDate = model.IssueDate,
                IssuerName = model.IssuerName
            };

            List<Incident> incidents = new List<Incident>();
                
            foreach (var categoryId in model.CategoryIds)
            {
                Category category = _categoryRepository.GetForUpsertBy(categoryId);
                    
                List<IncidentLine> incidentLines = category.Lines.Select(line => new IncidentLine()
                {
                    Line = line,
                    Yes = false,
                    No = false,
                    ToEvalute = false,
                    Comment = string.Empty
                }).ToList();
                    
                Incident incident = new Incident()
                {
                    Category = category,
                    IncidentLines = incidentLines
                };
                    
                incidents.Add(incident);
            }

            if (incidents is not null && incidents.Count() > 0)
            {
                tracking.Incidents = incidents;
            }

            int trackingId = _trackingRepository.AddOrUpdate(tracking);
        }

        private void UpdateTracking(CreateOrUpdateTrackingFormVM model)
        {
            Tracking tracking = _trackingRepository.GetForUpdateBy(model.Id);

            if (tracking is not null)
            {
                tracking.IssuerName = model.IssuerName;
                tracking.IssueDate = model.IssueDate;

                List<Incident> incidents = new List<Incident>();
                
                if (model.CategoryIds is not null && model.CategoryIds.Count() > 0)
                {
                    foreach (var categoryId in model.CategoryIds)
                    {
                        Category category = _categoryRepository.GetForUpsertBy(categoryId);
                    
                        List<IncidentLine> incidentLines = category.Lines.Select(line => new IncidentLine()
                        {
                            Line = line,
                            Yes = false,
                            No = false,
                            ToEvalute = false,
                            Comment = string.Empty
                        }).ToList();
                    
                        Incident incident = new Incident()
                        {
                            Category = category,
                            IncidentLines = incidentLines
                        };
                    
                        tracking.Incidents.Add(incident);
                    }
                }

                _trackingRepository.Update(tracking);
            }
        }
        private List<SelectListItem> GetCategoryDropdownList()
        {
            return _categoryRepository.GetAll().Select(category => new SelectListItem()
            {
                Text = category.ShortLabel,
                Value = category.Id.ToString()
            }).ToList();
        }
    }
}