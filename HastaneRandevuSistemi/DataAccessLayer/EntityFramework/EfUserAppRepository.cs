using DataAccessLayer.Abstract;
using DataAccessLayer.Concrate;
using DataAccessLayer.Repository;
using EntityLayer.Concrate;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.EntityFramework;

public class EfUserAppRepository : GenericRepository<AppUser>, IUserAppDal
{
    private UygulamaDbContext _context;

    public EfUserAppRepository(UygulamaDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<AppUser>> GetDoctorsAsync()
    {
        return await _context.Set<AppUser>().Where(user => user.Alan != null).ToListAsync();   // Doktor Alan adına  sahip mi????
    }
}