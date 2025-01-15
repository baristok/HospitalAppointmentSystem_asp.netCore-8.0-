namespace EntityLayer.Concrate;

public class Randevu
{
    public string RandevuId { get; set; }
    public string DoktorId { get; set; }
    public string HastaId { get; set; }
    public DateTime RandevuTarihi { get; set; }


    public AppUser Doktor { get; set; }
    public AppUser Hasta { get; set; }
}