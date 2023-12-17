using MILS.Domain;

namespace MILS.Infrastructure.Repositories.Interfaces;

public interface IIncidentLineRepository
{
    void Update(List<IncidentLine> incidentLines);
}