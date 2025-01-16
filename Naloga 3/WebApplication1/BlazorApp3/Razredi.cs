namespace BlazorApp3.Razredi
{
    public class Razredi
    {
        public class Voznik
        {
            public int Id { get; set; }
            public string ImePriimek { get; set; }
            public int StevilkaAvta { get; set; }
            public int letoRojstva { get; set; }

            public bool Novinec { get; set; }

            public Voznik(string ImePriimek, int StevilkaAvta, int letoRojstva, bool novinec)
            {
                this.ImePriimek = ImePriimek;
                this.StevilkaAvta = StevilkaAvta;
                this.letoRojstva = letoRojstva;
                Novinec = novinec;
            }
            public Voznik() { }

            public Voznik(string ImePriimek, int StevilkaAvta)
            {
                this.ImePriimek = ImePriimek;
                this.StevilkaAvta = StevilkaAvta;
            }
        }

        public class Ekipa
        {
            public int Id { get; set; }
            public string Ime { get; set; }
            public string Drzava { get; set; }
            public int letoUstanovitve { get; set; }

            public Ekipa(string Ime, string Drzava, int letoUstanovitve)
            {
                this.Ime = Ime;
                this.Drzava = Drzava;
                this.letoUstanovitve = letoUstanovitve;
            }

            public Ekipa() { }
        }

        public class VoznikVEkipi
        {
            public int Id { get; set; }
            public Voznik Voznik { get; set; }
            public Ekipa Ekipa { get; set; }
            public int letoVstopa { get; set; }
            public int letoIzpada { get; set; }
            public int steviloDirk { get; set; }
            public int steviloZmag { get; set; }
        }
    }
}

