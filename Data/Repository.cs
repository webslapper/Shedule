using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
namespace Shedule.Data;

public class Repository<T> : IRepository<T> where T : class
{
    private SheduleDbContext _context;
    private DbSet<T> _dbset;
    public Repository(SheduleDbContext context)
    {
        _context = context;
        _dbset = context.Set<T>();
    }

    public async Task<T> GetByIdAsync(int id) => await _dbset.FindAsync(id);
    public async Task<List<T>> GetAllAsync() => await _dbset.ToListAsync();
    public async Task AddAsync(T entity)
    {
        await _dbset.AddAsync(entity);
        await _context.SaveChangesAsync();
    }
    public async Task UpdateAsync(T entity)
    {
        await _dbset.UpdateAsync(entity);
        await _context.SaveChangesAsync();
    }
    public async Task DeleteAsync(int id)
    {
        var entity = await _dbset.FindAsync(id);
        if (entity != null)
        {
            _dbset.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}