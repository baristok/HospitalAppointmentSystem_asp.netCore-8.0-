namespace DataAccessLayer.EntityFramework;

public class RandevuWithDoctorDetailsDto
{
    public string RandevuId { get; set; }
    public DateTime RandevuTarihi { get; set; }
    public string DoktorAdSoyad { get; set; }
    public string DoktorAlani { get; set; }
}