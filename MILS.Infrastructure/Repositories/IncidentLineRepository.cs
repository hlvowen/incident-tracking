using MILS.Domain;
using MILS.Infrastructure.DataAccess;
using MILS.Infrastructure.Repositories.Interfaces;

namespace MILS.Infrastructure.Repositories;

public class IncidentLineRepository : IIncidentLineRepository
{
    private readonly ApplicationDbContext _context;

    public IncidentLineRepository(ApplicationDbContext applicationDbContext)
    {
        _context = applicationDbContext;
    }


    public void Update(List<IncidentLine> incidentLines)
    {
        List<int> incidentLineIds = incidentLines.Select(x => x.Id).ToList();
        
        List<IncidentLine> incidentLinesToUpdate = _context.IncidentLines
                                                           .Where(x => incidentLineIds.Contains(x.Id))
                                                           .ToList();
        
        foreach (IncidentLine incidentLineToUpdate in incidentLinesToUpdate)
        {
            IncidentLine incidentLine = incidentLines.Single(x => x.Id == incidentLineToUpdate.Id);

            if (incidentLineToUpdate.Yes != incidentLine.Yes) incidentLineToUpdate.Yes = incidentLine.Yes;
            if (incidentLineToUpdate.No != incidentLine.No) incidentLineToUpdate.No = incidentLine.No;
            if (incidentLineToUpdate.ToEvalute != incidentLine.ToEvalute) incidentLineToUpdate.ToEvalute = incidentLine.ToEvalute;
            if (incidentLineToUpdate.Comment != incidentLine.Comment) incidentLineToUpdate.Comment = incidentLine.Comment;
        }

        _context.SaveChanges();
    }
}