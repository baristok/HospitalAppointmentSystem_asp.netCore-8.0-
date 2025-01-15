using System.Linq.Expressions;

namespace DataAccessLayer.Abstract;

public interface IGenericDal <T> where T: class
{
    Task InsertAsync(T t);
    Task DeleteAsync(T t);
    Task UpdateAsync(T t);
    Task<List<T>> GetListAsync();
    Task<T> GetByIDAsync(object id); // ID türü esnek
    Task<List<T>> GetbyFilterAsync(Expression<Func<T, bool>> filter);
}