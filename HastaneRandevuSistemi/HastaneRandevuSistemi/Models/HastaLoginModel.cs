using System.ComponentModel.DataAnnotations;

namespace HastaneRandevuSistemi.Models;

public class HastaLoginModel
{
    [Required(ErrorMessage = "TC Kimlik numarası gereklidir.")]
    [RegularExpression(@"\d{11}", ErrorMessage = "TC Kimlik numarası 11 haneli olmalıdır.")]
    public string TcKimlik { get; set; }

    [Required(ErrorMessage = "Şifre gereklidir.")]
    [MinLength(6, ErrorMessage = "Şifre en az 6 karakter olmalıdır.")]
    public string Password { get; set; }
}