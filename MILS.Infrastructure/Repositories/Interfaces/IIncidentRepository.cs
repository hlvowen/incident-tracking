using MILS.Domain;

namespace MILS.Infrastructure.Repositories.Interfaces;

public interface IIncidentRepository
{
    IEnumerable<Incident> GetAllByTrackingId(int trackingId);
}