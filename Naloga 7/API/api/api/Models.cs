using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace api
{
    public class Voznik
    {
        public int Id { get; set; }
        public string Ime { get; set; }
        public string Priimek { get; set; }
        public int LetoRojstva { get; set; }
    }

    // DTO za POST operacijo
    public class VoznikWithTeamDto
    {
        [Required(ErrorMessage = "Ime je obvezno.")]
        public string Ime { get; set; }

        [Required(ErrorMessage = "Priimek je obvezen.")]
        public string Priimek { get; set; }

        [Range(1900, 2023, ErrorMessage = "Leto rojstva mora biti med 1900 in 2023.")]
        public int LetoRojstva { get; set; }

        [Required(ErrorMessage = "EkipaId je obvezen.")]
        public int EkipaId { get; set; }

        [Required(ErrorMessage = "LetoOd je obvezno.")]
        public int LetoOd { get; set; }

        [Required(ErrorMessage = "LetoDo je obvezno.")]
        public int LetoDo { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Stevilo zmag mora biti nenegativno.")]
        public int SteviloZmag { get; set; }
    }

    public class Ekipa
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public string Drzava { get; set; }
        public int LetoUstanovitve { get; set; }
    }

    public class VoznikVEkipi
    {
        public int Id { get; set; }  // Primarni ključ
        public int VoznikId { get; set; }  // Tuji ključ
        public Voznik Voznik { get; set; }
        public int EkipaId { get; set; }  // Tuji ključ
        public Ekipa Ekipa { get; set; }
        public int LetoOd { get; set; }
        public int LetoDo { get; set; }
        public int SteviloZmag { get; set; } = 0;   
    }

    public class VoznikVEkipiDto
    {
        public int VoznikId { get; set; }
        public int EkipaId { get; set; }
        public int LetoOd { get; set; }
        public int LetoDo { get; set; }
        public int SteviloZmag { get; set; }
    }


    public class Formula1Context : DbContext
    {
        public Formula1Context(DbContextOptions<Formula1Context> options) : base(options) { }

        public DbSet<Voznik> Vozniki { get; set; }
        public DbSet<Ekipa> Ekipe { get; set; }
        public DbSet<VoznikVEkipi> VoznikiVEkipi { get; set; }
    }


}
