using BusinessLayer.Abstract;
using BusinessLayer.Concrate;
using EntityLayer.Concrate;
using HastaneRandevuSistemi.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HastaneRandevuSistemi.Controllers;

public class DoktorController : Controller
{
     private readonly AppUserManager _appUserManager;
     private IRandevuService _randevuManager;

     public DoktorController(AppUserManager appUserManager, RandevuManager randevuManager)
     {
          _appUserManager = appUserManager;
          _randevuManager = randevuManager;
     }
     
     public async Task<IActionResult> Index()
     {
          var doktorId = HttpContext.Session.GetString("DoktorId"); // Oturumdaki Doktor ID'yi al
          if (string.IsNullOrEmpty(doktorId))
          {
               return RedirectToAction("Login"); // Oturum yoksa login sayfasına yönlendir
          }
          var doktor = await _appUserManager.TGetByID(doktorId);
          ViewBag.DoktorAdSoyad = $"{doktor.Name} {doktor.Surname}"; 
          // Doktorun randevularını ve hasta bilgilerini al
          var doktorRandevulari = await _randevuManager.GetRandevularWithHastaDetailsAsync(doktorId);

          // View'e gönder
          return View(doktorRandevulari);
     }
     [HttpGet]
     public IActionResult Login()
     {
          return View();
     }
     
     [HttpPost]
     public async Task<IActionResult> Login(DoktorLoginModel model)
     {
          if (ModelState.IsValid)
          {
               // Email ile kullanıcıyı bul
               var user = _appUserManager.TGetList().FirstOrDefault(u => u.Email == model.Email);
               if (user == null)
               {
                    ModelState.AddModelError("", "Geçersiz email adresi.");
                    return View();
               }

               // Kullanıcının "Doktor" rolüne sahip olduğunu kontrol et
               var roles = await _appUserManager.GetUserRoles(user);
               if (!roles.Contains("Doktor"))
               {
                    ModelState.AddModelError("", "Bu hesap doktor girişi için yetkili değil.");
                    return View();
               }

               // Şifre doğrulama
               var passwordHasher = new PasswordHasher<AppUser>();
               var passwordResult = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, model.Password);

               if (passwordResult == PasswordVerificationResult.Success)
               {
                    //BİR HATA VAR BU FONKSİYONDA DAHA SONRA BAK TEKRARDAN
                    // // Varsayılan şifre kontrolü
                    // if (model.Password == "Default123!")
                    // {
                    //      TempData["WarningMessage"] = "Lütfen şifrenizi değiştirin.";
                    //      return RedirectToAction("ChangePassword", new { userId = user.Id });
                    // }

                    // Giriş başarılı
                    HttpContext.Session.SetString("DoktorId", user.Id.ToString());
                    return RedirectToAction("Index", "Doktor");
               }
               else
               {
                    ModelState.AddModelError("", "Geçersiz şifre.");
               }
          }

          return View();
     }
     
     [HttpGet]
     public async Task<IActionResult> Logout()
     {
          HttpContext.Session.Clear();
            
          await HttpContext.SignOutAsync();
            
          return RedirectToAction("Login", "Doktor");
     }
     
     [HttpGet]
     public IActionResult ChangePassword()
     {
          var userId = HttpContext.Session.GetString("DoktorId");
          return View(new ChangePasswordViewModel { UserId = userId });
     }

     [HttpPost]
     public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
     {
          if (ModelState.IsValid)
          {
               var userId = HttpContext.Session.GetString("DoktorId");
               if (string.IsNullOrEmpty(userId))
               {
                    TempData["ErrorMessage"] = "Oturum süresi dolmuş, lütfen tekrar giriş yapın.";
                    return RedirectToAction("Login", "Hasta");
               }

               var user = await _appUserManager.TGetByID(userId);
               if (user != null)
               {
                    var result = await _appUserManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);

                    if (result.Succeeded)
                    {
                         HttpContext.Session.Clear();
                         await HttpContext.SignOutAsync();
                         TempData["SuccessMessage"] = "Şifreniz başarıyla değiştirildi. Lütfen tekrar giriş yapın.";
                    }
                    else
                    {
                         foreach (var error in result.Errors)
                         {
                              ModelState.AddModelError("", error.Description);
                         }
                    }
               }
               else
               {
                    ModelState.AddModelError("", "Kullanıcı bulunamadı.");
               }
          }

          return View(model);
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