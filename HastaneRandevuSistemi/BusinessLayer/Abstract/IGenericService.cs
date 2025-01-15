using System.Linq.Expressions;
using EntityLayer.Concrate;

namespace BusinessLayer.Abstract;

public interface IGenericService<T>
{
    Task TAddAsync(Randevu t);
    Task TDeleteAsync(Randevu t);
    Task TUpdateAsync(Randevu t);
    Task<Randevu> TGetByIDAsync(int id);
    Task<List<Randevu>> TGetListAsync(Expression<Func<Randevu, bool>> filter = null);
    
}