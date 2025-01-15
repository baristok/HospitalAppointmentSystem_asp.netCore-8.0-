using System.ComponentModel.DataAnnotations;

namespace HastaneRandevuSistemi.Models;

public class DoctorViewModel
{
    [Required(ErrorMessage = "Adı giriniz.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Soyadı giriniz.")]
    public string Surname { get; set; }

    [Required(ErrorMessage = "Telefon numarası giriniz.")]
    [Phone(ErrorMessage = "Geçerli bir telefon numarası giriniz.")]
    public string Phone { get; set; }

    [Required(ErrorMessage = "Email adresi giriniz.")]
    [EmailAddress(ErrorMessage = "Geçerli bir email adresi giriniz.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Uzmanlık alanı giriniz.")]
    public string Alan { get; set; }
}