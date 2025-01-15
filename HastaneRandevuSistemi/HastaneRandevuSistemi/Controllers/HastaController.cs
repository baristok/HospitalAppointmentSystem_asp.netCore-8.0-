using BusinessLayer.Abstract;
using BusinessLayer.Concrate;
using DataAccessLayer.Concrate;
using EntityLayer.Concrate;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HastaneRandevuSistemi.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace HastaneRandevuSistemi.Controllers;
public class HastaController : Controller
{
    private readonly AppUserManager _appUserManager;
    private IRandevuService _randevuService;

        public HastaController(AppUserManager appUserManager, RandevuManager randevuManager)
        {
            _appUserManager = appUserManager;
            _randevuService = randevuManager;
            
        }

        // Login Sayfası
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // Login İşlemi
        [HttpPost]
        public async Task<IActionResult> Login(HastaLoginModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _appUserManager.TGetList().FirstOrDefault(u => u.TcKimlik == model.TcKimlik);
                if (user == null)
                {
                    ModelState.AddModelError("", "Geçersiz TC Kimlik.");
                    return View();
                }

                var result = await _appUserManager.PasswordSignInAsync(user, model.Password);
                if (result)
                {
                    HttpContext.Session.SetString("HastaId", user.Id.ToString());
                    return RedirectToAction("Index", "Hasta");
                }
                else
                {
                    ModelState.AddModelError("", "Şifre hatalı.");
                }
            }

            return View();
        }

        // Register Sayfası
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // Register İşlemi
        [HttpPost]
        public async Task<IActionResult> Register(HastaRegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new AppUser
                {
                    Name = model.Name,
                    Surname = model.Surname,
                    TcKimlik = model.TcKimlik,
                    Email = model.Email,
                    UserName = model.Email
                };

                try
                {
                    await _appUserManager.TAdd(user, model.Password);
                    await _appUserManager.AddToRole(user, "Hasta");
                    return RedirectToAction("Login", "Hasta");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
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
        public async Task<IActionResult> Index()
        {
            var hastaId = HttpContext.Session.GetString("HastaId"); // Oturumdaki Hasta ID'yi al
            if (string.IsNullOrEmpty(hastaId))
            {
                return RedirectToAction("Login"); // Oturum yoksa login sayfasına yönlendir
            }
            var hasta = await _appUserManager.TGetByID(hastaId);
            ViewBag.HastaAdSoyad = $"{hasta.Name} {hasta.Surname}"; 
            
            // Randevu ve doktor bilgilerini al
            var randevularWithDetails = await _randevuService.GetRandevularWithDoctorDetailsAsync(hastaId);

            // View'e DTO'yu gönder
            return View(randevularWithDetails);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(string randevuId)
        {
            if (string.IsNullOrEmpty(randevuId))
            {
                return BadRequest("Randevu ID eksik.");
            }

            try
            {
                await _randevuService.TDeleteAsync(new Randevu { RandevuId = randevuId });
                return Ok("Randevu başarıyla silindi.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Randevu silinirken bir hata oluştu: {ex.Message}");
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetAlanlar()
        {
            var alanlar = await _appUserManager.GetAllAlanlarAsync(); // AppUserService üzerinden alanları al
            return Json(alanlar); // Alanları JSON formatında döndür
        }
        [HttpGet]
        public async Task<IActionResult> GetDoktorlar(string alan)
        {
            var doktorlar = await _appUserManager.TGetDoctorsAsync(); // Tüm doktorları al
            var filteredDoktorlar = doktorlar.Where(d => d.Alan == alan).ToList(); // Alana göre filtrele
            return Json(filteredDoktorlar); // Doktor listesini JSON olarak döndür
        }
        [HttpGet]
        public IActionResult RandevuEkle()
        {
            var hastaId = HttpContext.Session.GetString("HastaId"); // Oturumdaki Hasta ID'yi al
            if (string.IsNullOrEmpty(hastaId))
            {
                return RedirectToAction("Login"); // Oturum yoksa login sayfasına yönlendir
            }
            return View(); // Sadece view döner. Gerekli veriler frontend tarafından AJAX ile alınabilir.
        }
        [HttpPost]
        public async Task<IActionResult> RandevuEkle([FromBody]RandevuViewModel model)
        {
            Console.WriteLine($"Doktor ID: {model.DoktorId}");
            Console.WriteLine($"Randevu Tarihi: {model.RandevuTarihi}");
            var hastaId = HttpContext.Session.GetString("HastaId");
            if (string.IsNullOrEmpty(hastaId))
            {
                return BadRequest("Kullanıcı oturumu yok. Lütfen tekrar giriş yapın.");
            }
            
            // if (!ModelState.IsValid)
            // {
            //     return BadRequest("Geçersiz veri");
            // }
            
            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage);
                }
                return BadRequest("Geçersiz veri");
            }

            var randevu = new Randevu
            {
                RandevuId = Guid.NewGuid().ToString(),
                DoktorId = model.DoktorId,
                HastaId = hastaId,
                RandevuTarihi = model.RandevuTarihi
            };

            try
            {
                await _randevuService.TAddAsync(randevu);
                return Ok("Randevu başarıyla kaydedildi.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpGet]
        public IActionResult ChangePassword()
        {
            var userId = HttpContext.Session.GetString("HastaId");
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Hasta");
            }

            var model = new ChangePasswordViewModel { UserId = userId };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = HttpContext.Session.GetString("HastaId");
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

        
}