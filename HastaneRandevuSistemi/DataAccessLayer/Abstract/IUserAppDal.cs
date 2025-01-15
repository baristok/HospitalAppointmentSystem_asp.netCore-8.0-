using EntityLayer.Concrate;

namespace DataAccessLayer.Abstract;

public interface IUserAppDal : IGenericDal<AppUser>
{
    Task<List<AppUser>> GetDoctorsAsync(); // Sadece doktorları listele
}