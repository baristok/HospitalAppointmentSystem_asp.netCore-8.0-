using System.Linq.Expressions;
using DataAccessLayer.Abstract;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repository;

public class GenericRepository <T> : IGenericDal<T> where T : class
{
    
    private readonly DbContext _context;
    private readonly DbSet<T> _dbSet;

    public GenericRepository(DbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }
    
    
    public async Task InsertAsync(T t)
    {
        await _dbSet.AddAsync(t);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(T t)
    {
        _dbSet.Remove(t);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(T t)
    {
        _dbSet.Update(t);
        await _context.SaveChangesAsync();
    }

    public async Task<List<T>> GetListAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<T> GetByIDAsync(object id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<List<T>> GetbyFilterAsync(Expression<Func<T, bool>> filter)
    {
        return await _dbSet.Where(filter).ToListAsync();
    }
}