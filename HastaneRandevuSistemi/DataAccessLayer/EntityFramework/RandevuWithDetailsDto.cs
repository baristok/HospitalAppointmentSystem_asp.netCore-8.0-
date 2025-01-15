namespace DataAccessLayer.EntityFramework;

public class RandevuWithDetailsDto
{
    public string RandevuId { get; set; }
    public string HastaAdSoyad { get; set; }
    public string HastaEmail { get; set; }
    public string DoktorAdSoyad { get; set; }
    public string DoktorEmail { get; set; }
    public string DoktorAlan { get; set; }
    public DateTime RandevuTarihi { get; set; }
}