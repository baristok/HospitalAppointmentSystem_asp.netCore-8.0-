using System.Security.Claims;
using BusinessLayer.Concrate;
using EntityLayer.Concrate;
using HastaneRandevuSistemi.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HastaneRandevuSistemi.Controllers;

public class AdminController : Controller
{
    // public IActionResult Index()
    // {
    //     return View();
    // }
    
    private readonly AppUserManager _appUserManager;
    private RandevuManager _randevuManager;

    public AdminController(AppUserManager appUserManager, RandevuManager randevuManager)
    {
        _appUserManager = appUserManager;
        _randevuManager = randevuManager;
    }
    
    
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var adminId = HttpContext.Session.GetString("AdminId");
        if (string.IsNullOrEmpty(adminId))
        {
            return RedirectToAction("Login", "Admin");
        }
        
        var doctors = await _appUserManager.TGetDoctorsAsync(); // Doktorları al
        var hastalar = await _appUserManager.TGetHastalarAsync(); // Doktorları al
        var randevular = await _randevuManager.GetRandevuCountAsync();
        var doctorscount = doctors.Count;
        var hastalarcount = hastalar.Count;
        
        ViewBag.DoctorCount = doctorscount;
        ViewBag.HastalarCount = hastalarcount;
        ViewBag.RandevuCount = randevular;
        return View(doctors); // View'e gönder
    }
    
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }
    
    
    
    [HttpPost]
    public async Task<IActionResult> Login(AdminLoginModel model)
    {
        if (ModelState.IsValid)
        {
            // Kullanıcıyı kullanıcı adı (Username) ile bul
            var user = _appUserManager.TGetList().FirstOrDefault(u => u.UserName == model.Username);
            if (user == null)
            {
                ModelState.AddModelError("", "Geçersiz kullanıcı adı.");
                return View();
            }

            // Şifre doğrulama
            var result = await _appUserManager.PasswordSignInAsync(user, model.Password);
            if (result)
            {
                // Admin için oturum bilgisini sakla
                HttpContext.Session.SetString("AdminId", user.Id.ToString());
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                ModelState.AddModelError("", "Şifre hatalı.");
            }
        }

        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        HttpContext.Session.Clear();
            
        await HttpContext.SignOutAsync();
            
        return RedirectToAction("Login", "Hasta");
    }


    
    [HttpGet]
    public IActionResult AddDoctor()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AddDoctor(DoctorViewModel model)
    {
        if (ModelState.IsValid)
        {
            try
            {
                // Yeni doktor oluştur
                var doctor = new AppUser
                {
                    UserName = model.Email.Split('@')[0], // Kullanıcı adı
                    Email = model.Email,
                    PhoneNumber = model.Phone,
                    Name = model.Name,
                    Surname = model.Surname,
                    Alan = model.Alan,
                };

                // Kullanıcıyı oluştur
                await _appUserManager.TAdd(doctor, "Default123!"); // Varsayılan şifre
                await _appUserManager.AddToRole(doctor, "Doktor"); // Kullanıcıya "Doctor" rolü ata

                TempData["SuccessMessage"] = "Doktor başarıyla kaydedildi!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Hata: {ex.Message}";
            }
        }

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteDoctor(string doctorId)
    {
        if (string.IsNullOrEmpty(doctorId))
        {
            return BadRequest("Doktor ID eksik.");
        }

        try
        {
            var doctor = await _appUserManager.TGetByID(doctorId); // Doktoru ID ile bul
            if (doctor == null)
            {
                return NotFound("Doktor bulunamadı.");
            }

            await _appUserManager.TDelete(doctor); // Doktoru sil
            return Ok("Doktor başarıyla silindi.");
        }
        
        catch (Exception ex)
        {
            return BadRequest($"Doktor silinirken bir hata oluştu: {ex.Message}");
        }
    }
    
    [HttpGet]
    public async Task<IActionResult> GetDoctorListPartial()
    {
        var doctors = await _appUserManager.TGetDoctorsAsync();
        return PartialView("DoctorList", doctors);
    }
    
    [HttpGet]
    public async Task<IActionResult> RandevuListesi()
    {
        var adminId = HttpContext.Session.GetString("AdminId");
        if (string.IsNullOrEmpty(adminId))
        {
            return RedirectToAction("Login", "Admin");
        }
        
        var randevular = await _randevuManager.GetRandevularWithDetailsAsync();
        return View(randevular);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteRandevu(string randevuId)
    {
        if (string.IsNullOrEmpty(randevuId))
        {
            return BadRequest("Randevu ID eksik.");
        }

        try
        {
            await _randevuManager.TDeleteAsync(new Randevu { RandevuId = randevuId });
            return Ok("Randevu başarıyla silindi.");
        }
        catch (Exception ex)
        {
            return BadRequest($"Randevu silinirken bir hata oluştu: {ex.Message}");
        }
    }

    
}