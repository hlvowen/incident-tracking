using Microsoft.EntityFrameworkCore;
using MILS.Domain;
using MILS.Infrastructure.DataAccess;
using MILS.Infrastructure.Repositories.Interfaces;

namespace MILS.Infrastructure.Repositories;

public class TrackingRepository : ITrackingRepository
{
    private readonly ApplicationDbContext _context;

    public TrackingRepository(ApplicationDbContext applicationDbContext)
    {
        _context = applicationDbContext;
    }
    
    public IEnumerable<Tracking> GetAll()
    {
        return _context.Trackings.AsNoTracking();
    }

    public Tracking GetBy(int id)
    {
        return _context.Trackings.AsNoTracking()
                                 .Include(tracking => tracking.Incidents)
                                 .ThenInclude(incident => incident.Category)
                                 .Include(tracking => tracking.Incidents)
                                 .ThenInclude(incident => incident.IncidentLines)
                                 .ThenInclude(incidentLine => incidentLine.Line)
                                 .SingleOrDefault(tracking => tracking.Id == id);
    }

    public Tracking GetForUpdateBy(int id)
    {
        return _context.Trackings
                       .Include(tracking => tracking.Incidents)
                       .ThenInclude(incident => incident.Category)
                       .SingleOrDefault(tracking => tracking.Id == id);
    }

    public int AddOrUpdate(Tracking tracking)
    {
        if (tracking is null || tracking.Id < 0)
        {
            throw new ArgumentException(nameof(tracking));
        }

        var trackingToUpdate = _context.Trackings.SingleOrDefault(t => t.Id == tracking.Id);

        if (trackingToUpdate is null)
        {
            _context.Trackings.Add(tracking);
        }
        else
        {
            if (trackingToUpdate.IssueDate != tracking.IssueDate) { trackingToUpdate.IssueDate = tracking.IssueDate; }
            if (trackingToUpdate.IssuerName != tracking.IssuerName) { trackingToUpdate.IssuerName = tracking.IssuerName; }
        }

        _context.SaveChanges();

        return trackingToUpdate is null ? tracking.Id : trackingToUpdate.Id;
    }

    public void Update(Tracking tracking)
    {
        if (tracking is not null && tracking.Id <= 0)
        {
            throw new ArgumentException(nameof(tracking));
        }

        _context.Trackings.Update(tracking);
    }
}