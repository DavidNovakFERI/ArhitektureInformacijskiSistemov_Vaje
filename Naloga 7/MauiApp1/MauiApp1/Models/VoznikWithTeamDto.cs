using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Models
{
    public class VoznikWithTeamDto
    {
        public string Ime { get; set; }
        public string Priimek { get; set; }
        public int LetoRojstva { get; set; }
        public int EkipaId { get; set; }
        public int LetoOd { get; set; }
        public int LetoDo { get; set; }
        public int SteviloZmag { get; set; }
    }
}
