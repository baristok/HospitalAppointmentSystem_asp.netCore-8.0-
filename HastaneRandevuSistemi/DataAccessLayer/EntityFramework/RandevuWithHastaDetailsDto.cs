namespace DataAccessLayer.EntityFramework;

public class RandevuWithHastaDetailsDto
{
    public string RandevuId { get; set; }
    public DateTime RandevuTarihi { get; set; }
    public string HastaAdSoyad { get; set; }
    public string HastaEmail { get; set; }
}