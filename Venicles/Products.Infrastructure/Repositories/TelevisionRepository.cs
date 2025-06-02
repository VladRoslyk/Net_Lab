using Microsoft.EntityFrameworkCore;
using Products.Common.Contracts;
using Products.Infrastructure.DbContexts;
using Products.Infrastructure.Models;

namespace Products.Infrastructure.Repositories;

public class TelevisionRepository : IRepository<TelevisionModel>
{
    public TelevisionRepository(ProductsDbContext dbContext)
    {
        DbContext = dbContext;
    }

    public ProductsDbContext DbContext { get; }

    public async Task<bool> DeleteAsync(TelevisionModel element)
    {
        DbContext.Remove(element);
        return await DbContext.SaveChangesAsync() > 0;
    }

    public async Task<IEnumerable<TelevisionModel>> GetAll()
    {
        return await DbContext.Televisions
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<IEnumerable<TelevisionModel>> GetAll(int page, int count)
    {
        return await DbContext.Televisions
            .Skip(page * count)
            .Take(count)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<TelevisionModel?> GetById(Guid id)
    {
        return await DbContext.Televisions.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<bool> InsertAsync(TelevisionModel element)
    {
        DbContext.Add(element);
        return await DbContext.SaveChangesAsync() > 0;
    }

    public async Task<bool> SaveAsync(string path)
    {
        return await DbContext.SaveChangesAsync() > 0;
    }

    public async Task<bool> UpdateAsync(TelevisionModel element)
    {
        DbContext.Update(element);
        return await DbContext.SaveChangesAsync() > 0;
    }
}
