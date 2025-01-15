using Microsoft.AspNetCore.Identity;

namespace EntityLayer.Concrate;

public class AppUser : IdentityUser
{
    public string Name { get; set; }
    public string Surname { get; set; }

    public string? TcKimlik { get; set; }
    //doktor için
    public string? Alan { get; set; }
}