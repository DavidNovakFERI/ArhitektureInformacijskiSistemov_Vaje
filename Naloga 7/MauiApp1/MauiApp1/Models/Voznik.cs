using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Models
{
    public class Voznik
    {
        public int Id { get; set; }

        [Required]
        public string Ime { get; set; } = string.Empty; 

        [Required]
        public string Priimek { get; set; } = string.Empty; 

        public int LetoRojstva { get; set; }
    }

}
