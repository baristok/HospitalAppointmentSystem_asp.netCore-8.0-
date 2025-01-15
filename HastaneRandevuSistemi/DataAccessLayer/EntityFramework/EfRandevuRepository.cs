using System.Linq.Expressions;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrate;
using DataAccessLayer.Repository;
using EntityLayer.Concrate;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.EntityFramework;

public class EfRandevuRepository : GenericRepository<Randevu> , IRandevuDal
{
    private UygulamaDbContext _context;

    public EfRandevuRepository(UygulamaDbContext context) : base(context)
    {
        _context = context;
    }


    public async Task<List<Randevu>> GetRandevularByDoctorAsync(string doctorId)
    {
        return await _context.Set<Randevu>().Where(r => r.DoktorId == doctorId).ToListAsync();
    }

    public async Task<List<Randevu>> GetRandevularByDateAsync(DateTime date)
    {
        var startOfDay = date.Date; // Günün başlangıcı
        var endOfDay = date.Date.AddDays(1).AddTicks(-1); // Günün sonu

        return await _context.Set<Randevu>()
            .Where(r => r.RandevuTarihi >= startOfDay && r.RandevuTarihi <= endOfDay)
            .ToListAsync();
    }
    
    public async Task AddAsync(Randevu entity)
    {
        await _context.Randevus.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Randevu entity)
    {
        _context.Randevus.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Randevu entity)
    {
        _context.Randevus.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Randevu>> GetListAsync(Expression<Func<Randevu, bool>> filter = null)
    {
        return filter == null
            ? await _context.Randevus.ToListAsync()
            : await _context.Randevus.Where(filter).ToListAsync();
    }

    public async Task<Randevu> GetByIdAsync(int id)
    {
        return await _context.Randevus.FindAsync(id);
    }
    
    public async Task<List<RandevuWithDoctorDetailsDto>> GetRandevularWithDoctorDetailsAsync(string hastaId)
    {
        return await (from r in _context.Randevus
            join d in _context.Users on r.DoktorId equals d.Id
            where r.HastaId == hastaId
            select new RandevuWithDoctorDetailsDto
            {
                RandevuId = r.RandevuId,
                RandevuTarihi = r.RandevuTarihi,
                DoktorAdSoyad = d.Name + " " + d.Surname,
                DoktorAlani = d.Alan
            }).ToListAsync();
    }
    
    public async Task<List<RandevuWithHastaDetailsDto>> GetRandevularWithHastaDetailsAsync(string doktorId)
    {
        return await (from r in _context.Randevus
            join h in _context.Users on r.HastaId equals h.Id
            where r.DoktorId == doktorId
            select new RandevuWithHastaDetailsDto
            {
                RandevuId = r.RandevuId,
                RandevuTarihi = r.RandevuTarihi,
                HastaAdSoyad = h.Name + " " + h.Surname,
                HastaEmail = h.Email
            }).ToListAsync();
    }
    
    public async Task<List<RandevuWithDetailsDto>> GetListWithDetailsAsync()
    {
        return await _context.Randevus
            .Include(r => r.Hasta)
            .Include(r => r.Doktor)
            .Select(r => new RandevuWithDetailsDto
            {
                RandevuId = r.RandevuId,
                HastaAdSoyad = r.Hasta.Name + " " + r.Hasta.Surname,
                HastaEmail = r.Hasta.Email,
                DoktorAdSoyad = r.Doktor.Name + " " + r.Doktor.Surname,
                DoktorEmail = r.Doktor.Email,
                DoktorAlan = r.Doktor.Alan,
                RandevuTarihi = r.RandevuTarihi
            })
            .ToListAsync();

    }



}