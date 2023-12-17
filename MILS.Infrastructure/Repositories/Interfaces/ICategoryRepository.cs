using MILS.Domain;

namespace MILS.Infrastructure.Repositories.Interfaces;

public interface ICategoryRepository
{
    IEnumerable<Category> GetAll();
    Category GetBy(int id);
    Category GetForUpsertBy(int id);
}