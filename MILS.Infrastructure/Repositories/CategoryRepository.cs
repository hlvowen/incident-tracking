using Microsoft.EntityFrameworkCore;
using MILS.Domain;
using MILS.Infrastructure.DataAccess;
using MILS.Infrastructure.Repositories.Interfaces;

namespace MILS.Infrastructure.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly ApplicationDbContext _context;

    public CategoryRepository(ApplicationDbContext applicationDbContext)
    {
        _context = applicationDbContext;
    }
    
    public IEnumerable<Category> GetAll()
    {
        return _context.Categories
                       .AsNoTracking()
                       .Include(category => category.Lines);
    }
    

    public Category GetBy(int id)
    {
        return _context.Categories.AsNoTracking()
                                  .Include(category => category.Lines)
                                  .SingleOrDefault(category => category.Id == id);
    }

    public Category GetForUpsertBy(int id)
    {
        return _context.Categories
                       .Include(category => category.Lines)
                       .SingleOrDefault(category => category.Id == id);
    }
}