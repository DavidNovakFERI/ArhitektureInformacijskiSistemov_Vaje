namespace Vaja6
{
    public class Vozniki
    {
        public int Id { get; set; }
        public string Ime { get; set; }
        public string Priimek { get; set; }
        public int Starost { get; set; }
        public int ŠtevilkaFormule { get; set; }
        public int ŠteviloZmag { get; set; }

        public Vozniki(int id, string ime, string priimek, int starost, int številkaFormule, int številoZmag)
        {
            Id = id;
            Ime = ime;
            Priimek = priimek;
            Starost = starost;
            ŠtevilkaFormule = številkaFormule;
            ŠteviloZmag = številoZmag;
        }

        public Vozniki() { }
    }
}
