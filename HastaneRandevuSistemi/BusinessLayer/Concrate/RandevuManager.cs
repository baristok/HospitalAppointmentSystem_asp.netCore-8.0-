using System.Linq.Expressions;
using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DataAccessLayer.EntityFramework; // Veri erişim katmanının olduğu yer
using EntityLayer.Concrate;

namespace BusinessLayer.Concrate;

public class RandevuManager : IRandevuService
{
    private readonly IRandevuDal _randevuDal;

    public RandevuManager(IRandevuDal randevuDal)
    {
        _randevuDal = randevuDal;
    }

    public async Task TAddAsync(Randevu t)
    {
        t.RandevuTarihi = t.RandevuTarihi.ToUniversalTime(); // UTC'ye çevir
        if (t.RandevuTarihi < DateTime.Now)
        {
            throw new Exception("Randevu tarihi geçmiş olamaz.");
        }

        var existingRandevu = await _randevuDal.GetListAsync(r =>
            r.DoktorId == t.DoktorId && r.RandevuTarihi == t.RandevuTarihi);

        if (existingRandevu.Any())
        {
            throw new Exception("Bu doktora bu tarihte zaten bir randevu var.");
        }

        await _randevuDal.AddAsync(t);
    }

    public async Task TDeleteAsync(Randevu t)
    {
        await _randevuDal.DeleteAsync(t);
    }

    public async Task TUpdateAsync(Randevu t)
    {
        await _randevuDal.UpdateAsync(t);
    }

    public async Task<List<Randevu>> TGetListAsync(Expression<Func<Randevu, bool>> filter = null)
    {
        if (filter == null)
        {
            return await _randevuDal.GetListAsync();
        }
        return await _randevuDal.GetListAsync(filter);
    }


    public async Task<Randevu> TGetByIDAsync(int id)
    {
        return await _randevuDal.GetByIdAsync(id);
    }
    public async Task<List<RandevuWithDoctorDetailsDto>> GetRandevularWithDoctorDetailsAsync(string hastaId)
    {
        return await _randevuDal.GetRandevularWithDoctorDetailsAsync(hastaId);
    }
    
    public async Task<int> GetRandevuCountAsync()
    {
        var randevular = await _randevuDal.GetListAsync();
        return randevular.Count;
    }
    
    public async Task<List<RandevuWithHastaDetailsDto>> GetRandevularWithHastaDetailsAsync(string doktorId)
    {
        return await _randevuDal.GetRandevularWithHastaDetailsAsync(doktorId);
    }
    public async Task<List<RandevuWithDetailsDto>> GetRandevularWithDetailsAsync()
    {
        return await _randevuDal.GetListWithDetailsAsync(); // Veri erişim katmanına yönlendirme
    }
    
}