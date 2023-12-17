using MILS.Domain;

namespace MILS.Infrastructure.Repositories.Interfaces;

public interface ITrackingRepository
{
    IEnumerable<Tracking> GetAll();
    Tracking GetBy(int id);
    Tracking GetForUpdateBy(int id);
    int AddOrUpdate(Tracking tracking);
    void Update(Tracking tracking);
}