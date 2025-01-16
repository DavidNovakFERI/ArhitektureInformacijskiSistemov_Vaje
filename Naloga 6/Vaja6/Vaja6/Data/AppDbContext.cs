using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;
using static System.Net.WebRequestMethods;


namespace Vaja6.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { 
                 
        }

        string DbPath = @"D:\2LETNIK\_AIS\Naloga 6\data.db";
        
        public AppDbContext(){}

        public DbSet<Vozniki> Vozniki { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            Console.WriteLine("Database File: " + DbPath);
            optionsBuilder.UseSqlite($"Filename={DbPath}");
        }

        

    }
}
