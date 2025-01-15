using System.ComponentModel.DataAnnotations;

namespace HastaneRandevuSistemi.Models;

public class RandevuViewModel
{
    [Required(ErrorMessage = "Doktor ID gereklidir.")]
    public string DoktorId { get; set; } = "0";

    [Required(ErrorMessage = "Randevu tarihi gereklidir.")]
    public DateTime RandevuTarihi { get; set; }
}