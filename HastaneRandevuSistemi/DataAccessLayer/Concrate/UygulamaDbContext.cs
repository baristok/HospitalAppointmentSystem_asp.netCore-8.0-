using EntityLayer.Concrate;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrate
{
	public class UygulamaDbContext : IdentityDbContext<AppUser>
	{

		public UygulamaDbContext(DbContextOptions<UygulamaDbContext> options) : base(options)
		{

		}

		public DbSet<Randevu> Randevus { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			// Randevu İlişkileri
			modelBuilder.Entity<Randevu>()
				.HasOne(r => r.Doktor)
				.WithMany() // Doktor çok sayıda randevu alabilir
				.HasForeignKey(r => r.DoktorId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<Randevu>()
				.HasOne(r => r.Hasta)
				.WithMany() // Hasta çok sayıda randevu alabilir
				.HasForeignKey(r => r.HastaId)
				.OnDelete(DeleteBehavior.Restrict);
		}

	}
}
