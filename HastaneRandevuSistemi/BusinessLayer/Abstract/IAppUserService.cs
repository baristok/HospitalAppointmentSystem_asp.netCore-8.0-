using EntityLayer.Concrate;

namespace BusinessLayer.Abstract;

public interface IAppUserService
{
    Task TAdd(AppUser user, string password); // Kullanıcı Ekle
    Task TDelete(AppUser user); // Kullanıcı Sil
    Task TUpdate(AppUser user); // Kullanıcı Güncelle
    List<AppUser> TGetList(); // Tüm Kullanıcıları Listele
    Task<AppUser> TGetByID(string id); // Asenkron ID ile Kullanıcı Getir
    Task AddToRole(AppUser user, string role); // Kullanıcıya Rol Ekle
    Task<List<string>> GetUserRoles(AppUser user); // Kullanıcının Rollerini Getir

    Task<List<AppUser>> TGetDoctorsAsync();
    Task<List<AppUser>> TGetHastalarAsync();
}