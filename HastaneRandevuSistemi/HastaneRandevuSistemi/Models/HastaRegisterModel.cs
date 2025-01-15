using System.ComponentModel.DataAnnotations;

namespace HastaneRandevuSistemi.Models;

public class HastaRegisterModel
{
    [Required(ErrorMessage = "Ad gereklidir.")]
    [StringLength(50, ErrorMessage = "Ad 50 karakterden uzun olamaz.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Soyad gereklidir.")]
    [StringLength(50, ErrorMessage = "Soyad 50 karakterden uzun olamaz.")]
    public string Surname { get; set; }

    [Required(ErrorMessage = "TC Kimlik numarası gereklidir.")]
    [RegularExpression(@"\d{11}", ErrorMessage = "TC Kimlik numarası 11 haneli olmalıdır.")]
    public string TcKimlik { get; set; }

    [Required(ErrorMessage = "Email adresi gereklidir.")]
    [EmailAddress(ErrorMessage = "Geçerli bir email adresi giriniz.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Şifre gereklidir.")]
    [MinLength(6, ErrorMessage = "Şifre en az 6 karakter olmalıdır.")]
    public string Password { get; set; }
}