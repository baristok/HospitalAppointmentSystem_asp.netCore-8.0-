using BusinessLayer.Abstract;
using EntityLayer.Concrate;
using Microsoft.AspNetCore.Identity;

namespace BusinessLayer.Concrate;

public class AppUserManager : IAppUserService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public AppUserManager(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task TAdd(AppUser user, string password)
    {
        var result = await _userManager.CreateAsync(user, password);
        if (!result.Succeeded)
        {
            throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
        }
    }

    public async Task TDelete(AppUser user)
    {
        var result = await _userManager.DeleteAsync(user);
        if (!result.Succeeded)
        {
            throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
        }
    }

    public async Task TUpdate(AppUser user)
    {
        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded)
        {
            throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
        }
    }

    public List<AppUser> TGetList()
    {
        return _userManager.Users.ToList();
    }

    public async Task<AppUser> TGetByID(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
        {
            throw new Exception("Kullanıcı bulunamadı.");
        }
        return user;
    }

    

    public async Task AddToRole(AppUser user, string role)
    {
        var roleExists = await _roleManager.RoleExistsAsync(role);
        if (!roleExists)
        {
            throw new Exception("Belirtilen rol mevcut değil.");
        }

        var result = await _userManager.AddToRoleAsync(user, role);
        if (!result.Succeeded)
        {
            throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
        }
    }

    public async Task<List<string>> GetUserRoles(AppUser user)
    {
        return (await _userManager.GetRolesAsync(user)).ToList();
    }

	public async Task<List<AppUser>> TGetDoctorsAsync()
	{
		var allUsers = _userManager.Users.ToList();
		

		var doctors = new List<AppUser>();
		foreach (var user in allUsers)
		{
			var roles = await _userManager.GetRolesAsync(user);
			

			if (roles.Contains("Doktor")) // "Doktor" rolüne sahip mi kontrol et
			{
				
				doctors.Add(user);
			}
		}

		
		return doctors;
	}
    
    public async Task<List<AppUser>> TGetHastalarAsync()
    {
        var allUsers = _userManager.Users.ToList();
		

        var hastalar = new List<AppUser>();
        foreach (var user in allUsers)
        {
            var roles = await _userManager.GetRolesAsync(user);
			

            if (roles.Contains("Hasta")) // "Hasta" rolüne sahip mi kontrol et
            {
				
                hastalar.Add(user);
            }
        }

		
        return hastalar;
    }
    
    
    public async Task<bool> PasswordSignInAsync(AppUser user, string password)
    {
        var result = await _userManager.CheckPasswordAsync(user, password);
        return result;
    }

    public async Task<List<string>> GetAllAlanlarAsync()
    {
        var allUsers = _userManager.Users.ToList();
        
        var alanlar = new List<string>();
        
        foreach (var user in allUsers)
        {

            var roles = await _userManager.GetRolesAsync(user);
            
            if (roles.Contains("Doktor") && !string.IsNullOrEmpty(user.Alan))
            {
                alanlar.Add(user.Alan);
            }
        }
        
        return alanlar.Distinct().ToList();
    }
    public async Task<IdentityResult> ChangePasswordAsync(AppUser user, string currentPassword, string newPassword)
    {
        if (user == null)
        {
            throw new ArgumentNullException(nameof(user), "Kullanıcı bulunamadı.");
        }

        // Mevcut şifreyi kontrol ederek yeni şifreyi güncelle
        var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
        return result;
    }





}