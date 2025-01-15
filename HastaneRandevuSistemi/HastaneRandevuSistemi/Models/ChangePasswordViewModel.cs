namespace HastaneRandevuSistemi.Models;

public class ChangePasswordViewModel
{
    public string UserId { get; set; } // Kullanıcı ID
    public string CurrentPassword { get; set; } // Mevcut şifre
    public string NewPassword { get; set; } // Yeni şifre
    public string ConfirmPassword { get; set; } // Yeni şifre doğrulama
}