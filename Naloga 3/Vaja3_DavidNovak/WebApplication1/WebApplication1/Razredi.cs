namespace WebApplication1.Models
{
    public class Razredi { 
    public partial class Voznik
    {
        public string ImePriimek { get; set; }
        public int StevilkaAvta { get; set; }
        public int letoRojstva { get; set; }
        
        public Voznik(string ImePriimek, int StevilkaAvta, int letoRojstva)
            {
            this.ImePriimek = ImePriimek;
            this.StevilkaAvta = StevilkaAvta;
            this.letoRojstva = letoRojstva;
        }
        internal record voznik(string ImePriimek, int StevilkaAvta, int letoRojstva);
            
            public static void vozniki(WebApplication app)
            {
                
                List<voznik> vozniki = new List<voznik>();
                vozniki.Add(new voznik("Lewis Hamilton", 44, 1985));
                vozniki.Add(new voznik("Valtteri Bottas", 77, 1989));
                vozniki.Add(new voznik("Max Verstappen", 33, 1997));
                vozniki.Add(new voznik("Sergio Perez", 11, 1990));
                vozniki.Add(new voznik("Lando Norris", 4, 1999));

                
                app.MapGet("/vozniki", () =>
                    {
                    return vozniki;
                    }
                );

                
                app.MapGet("/PodrobnostiVoznikov/{ImePriimek}", (string ImePriimek) =>
                {
                    List<voznik> vozniki2 = new List<voznik>();
                    foreach (var item in vozniki)
                    {
                        if (item.ImePriimek == ImePriimek)
                        {
                            vozniki2.Add(item);
                        }
                    }
                    return vozniki2;
                }
                               );

                
                app.MapGet("/najstarejsiVoznik", () =>
                {
                    int letoRojstva = 2021;
                    string ime = "";
                    foreach (var item in vozniki)
                    {
                        if (letoRojstva > item.letoRojstva)
                        {
                            letoRojstva = item.letoRojstva;
                            ime = item.ImePriimek;
                        }
                    }
                    return ime;
                }
                                   );

                
                app.MapGet("/povprecnaStarost", () =>
                {
                    int vsotaLet = 0;
                    int povprecnaStarost = 0;
                    foreach (var item in vozniki)
                    {
                        vsotaLet += item.letoRojstva;
                    }
                    povprecnaStarost = 2023 - (vsotaLet / vozniki.Count());
                    return povprecnaStarost;
                }
                                                  );
                

                
            }
    }

    public partial class Ekipa
    {
        public string Ime { get; set; }
        public string Drzava { get; set; }
        public int letoUstanovitve { get; set; }
        
        public Ekipa(string Ime, string Drzava, int letoUstanovitve)
            {
            this.Ime = Ime;
            this.Drzava = Drzava;
            this.letoUstanovitve = letoUstanovitve;
        }
        internal record ekipa(string Ime, string Drzava, int letoUstanovitve);
            public static void ekipe(WebApplication app)
            {
                
                List<ekipa> ekipe = new List<ekipa>();
                ekipe.Add(new ekipa("Mercedes", "Nemčija", 1970));
                ekipe.Add(new ekipa("Red Bull Racing", "Avstrija", 2005));
                ekipe.Add(new ekipa("McLaren", "Velika Britanija", 1963));
                ekipe.Add(new ekipa("Ferrari", "Italija", 1950));
                ekipe.Add(new ekipa("Alpine", "Francija", 1977));

                app.MapGet("/ekipe", () =>
                {
                    return ekipe;
                    }
                               );

                
            }

    }

    public partial class VoznikVEkipi
    {
        public Voznik Voznik { get; set; }
        public Ekipa Ekipa { get; set; }
        public int letoVstopa { get; set; }
        public int letoIzpada { get; set; }
        public int steviloDirk { get; set; }
        public int steviloZmag { get; set; }
        
        //konstruktor
        public VoznikVEkipi(Voznik Voznik, Ekipa Ekipa, int letoVstopa, int letoIzpada, int steviloDirk, int steviloZmag)
            {
                this.Voznik = Voznik;
                this.Ekipa = Ekipa;
                this.letoVstopa = letoVstopa;
                this.letoIzpada = letoIzpada;
                this.steviloDirk = steviloDirk;
                this.steviloZmag = steviloZmag;
            }

        internal record voznikVEkipe(Voznik Voznik, Ekipa Ekipa, int letoVstopa, int letoIzpada, int steviloDirk, int steviloZmag);
            public static void voznikVEkipi(WebApplication app)
            {
                //list
                List<voznikVEkipe> voznikVEkipi = new List<voznikVEkipe>();
                voznikVEkipi.Add(new voznikVEkipe(new Voznik("Lewis Hamilton", 44, 1985), new Ekipa("Mercedes", "Nemčija", 1970), 2013, 0, 270, 98));
                voznikVEkipi.Add(new voznikVEkipe(new Voznik("Valtteri Bottas", 77, 1989), new Ekipa("Ferrari", "Nemčija", 1970), 2017, 0, 161, 9));
                voznikVEkipi.Add(new voznikVEkipe(new Voznik("Max Verstappen", 33, 1997), new Ekipa("Red Bull Racing", "Avstrija", 2005), 2016, 0, 123, 10));
                voznikVEkipi.Add(new voznikVEkipe(new Voznik("Sergio Perez", 11, 1990), new Ekipa("Red Bull Racing", "Avstrija", 2005), 2021, 0, 198, 2));
                voznikVEkipi.Add(new voznikVEkipe(new Voznik("Lando Norris", 4, 1999), new Ekipa("McLaren", "Velika Britanija", 1963), 2019, 0, 46, 0));


                app.MapGet("/voznikVEkipi", () =>
                {
                    return voznikVEkipi;
                }
                               );

                
                app.MapGet("/voznikVEkipi/{ImePriimek}", (string ImePriimek) =>
                {
                    List<voznikVEkipe> voznikVEkipi2 = new List<voznikVEkipe>();
                    foreach (var item in voznikVEkipi)
                    {
                        if (item.Voznik.ImePriimek == ImePriimek)
                        {
                            voznikVEkipi2.Add(item);
                        }
                    }
                    return voznikVEkipi2;
                }
                                              );

                
                app.MapGet("/najvecVoznikov", ()=>
                {
                    int steviloVoznikov = 1;
                    string imeEkipe = "";
                    foreach (var item in voznikVEkipi)
                    {
                        if(steviloVoznikov < item.Ekipa.Ime.Count())
                        {
                            steviloVoznikov = item.Ekipa.Ime.Count();
                            imeEkipe = item.Ekipa.Ime;
                        }
                    }
                   return imeEkipe;
                }
                    );








            }
       

    }

} 
    
}
