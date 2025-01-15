using DataAccessLayer.EntityFramework;
using EntityLayer.Concrate;

namespace BusinessLayer.Abstract;

public interface IRandevuService : IGenericService<Randevu>
{
    Task<List<RandevuWithDoctorDetailsDto>> GetRandevularWithDoctorDetailsAsync(string hastaId);
    Task<List<RandevuWithHastaDetailsDto>> GetRandevularWithHastaDetailsAsync(string doktorId);

    Task<List<RandevuWithDetailsDto>> GetRandevularWithDetailsAsync();
}