using Microsoft.EntityFrameworkCore;
using static WebApplication1.Models.Razredi;

namespace WebApplication1
{
    public class RacingDbContext : DbContext
    {
        string DbPath = @"D:\2LETNIK\_AIS\Naloga 4\Nova mapa\data.db";
        public DbSet<Voznik> Vozniki { get; set; }
        public DbSet<Ekipa> Ekipe { get; set; }
        public DbSet<VoznikVEkipi> VoznikVEkipi { get; set; }

       protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
       {
            optionsBuilder.UseSqlite($"Filename={DbPath}");
       }

       protected override void OnModelCreating(ModelBuilder modelBuilder)
       {
            modelBuilder.Entity<Voznik>().Property(p => p.StevilkaAvta).HasColumnName("Stevilka");
            modelBuilder.Entity<Ekipa>().Property(p => p.letoUstanovitve).HasColumnName("Leto");
            modelBuilder.Entity<Voznik>().Property(p => p.letoRojstva).HasDefaultValue(1990);
            
       }
    }
}
