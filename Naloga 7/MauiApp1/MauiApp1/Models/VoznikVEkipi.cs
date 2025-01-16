using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Models
{
    public class VoznikVEkipi
    {
        public int Id { get; set; }
        public int VoznikId { get; set; }
        public Voznik Voznik { get; set; }
        public int EkipaId { get; set; }
        public Ekipa Ekipa { get; set; }
        public int LetoOd { get; set; }
        public int LetoDo { get; set; }
        public int SteviloZmag { get; set; } = 0;
    }
}
