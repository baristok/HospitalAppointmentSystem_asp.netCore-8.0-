using System.Linq.Expressions;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrate;

namespace DataAccessLayer.Abstract;

public interface IRandevuDal : IGenericDal<Randevu>
{
    Task<List<Randevu>> GetRandevularByDoctorAsync(string doctorId); // Doktora göre randevuları getir
    Task<List<Randevu>> GetRandevularByDateAsync(DateTime date); // Belirli bir tarihe göre randevuları getir
    Task AddAsync(Randevu entity);
    Task DeleteAsync(Randevu entity);
    Task UpdateAsync(Randevu entity);
    Task<List<Randevu>> GetListAsync(Expression<Func<Randevu, bool>> filter = null);
    Task<Randevu> GetByIdAsync(int id);
    Task<List<RandevuWithDoctorDetailsDto>> GetRandevularWithDoctorDetailsAsync(string hastaId);
    Task<List<RandevuWithHastaDetailsDto>> GetRandevularWithHastaDetailsAsync(string doktorId);
    
    Task<List<RandevuWithDetailsDto>> GetListWithDetailsAsync();

}