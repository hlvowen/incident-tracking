using Microsoft.EntityFrameworkCore;
using MILS.Domain;
using MILS.Infrastructure.DataAccess;
using MILS.Infrastructure.Repositories.Interfaces;

namespace MILS.Infrastructure.Repositories;

public class IncidentRepository : IIncidentRepository
{
    private readonly ApplicationDbContext _context;

    public IncidentRepository(ApplicationDbContext applicationDbContext)
    {
        _context = applicationDbContext;
    }
    public IEnumerable<Incident> GetAllByTrackingId(int trackingId)
    {
        return _context.Incidents
                       .AsNoTracking()
                       .Include(incident => incident.IncidentLines)
                       .ThenInclude(incidentLine => incidentLine.Line)
                       .Include(incident => incident.Category)
                       .Include(incident => incident.Tracking)
                       .Where(incident => incident.TrackingId == trackingId);
    }
}