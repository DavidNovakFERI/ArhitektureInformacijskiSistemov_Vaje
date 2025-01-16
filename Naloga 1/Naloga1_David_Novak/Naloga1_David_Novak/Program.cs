using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Program
{
    private static List<Dogodek> dogodki;

    static void Main(string[] args)
    {
    
    
    UrnikDogodkov urnik = new UrnikDogodkov();

    
    Koncert koncert1 = new Koncert("The Weeknd", "Arena Stožice", 15000);
    Koncert koncert2 = new Koncert("Eminem", "Webley", 90000);
    Koncert koncert3 = new Koncert("AC/DC", "Anfield", 70000);

    Predstava predstava1 = new Predstava("Quentin Tarantino", "Cankarjev dom", 1000);
    Predstava predstava2 = new Predstava("Christopher Nolan", "Cankarjev dom", 1000);
    Predstava predstava3 = new Predstava("Steven Spielberg", "Cankarjev dom", 1000);

    Poroka poroka1 = new Poroka("David Novak", "Nika Klemenčič", "Gostilna na hribu");
    Poroka poroka2 = new Poroka("Janez Kovač", "Marija Horvat", "Gostilna na hribu");
    Poroka poroka3 = new Poroka("Miha Kumin", "Maja Novak", "Gostilna na hribu");
    Poroka poroka4 = new Poroka("Tomaž Novak", "Ana Cartl", "Gostilna na hribu");

    List<Dogodek> dogodki = UrnikDogodkov.BeriCSV("datoteka.csv");

    List<Dogodek> iskaniDogodki = urnik.IskanjeDogodkovPoDatumuInLokaciji(DateTime.Parse("13.03.2023"), "Maribor");
    }

    
}


abstract class Dogodek
{
    public string Ime { get; set; }
    public DateTime Datum { get; set; }
    public int Cena { get; set; }

    public Dogodek(string ime, DateTime datum, int cena)
    {
        Ime = ime;
        Datum = datum;
        Cena = cena;
    }

}

class Koncert : Dogodek
{
    
    public string Izvajalec { get; set; }
    public string Lokacija { get; set; }
    public int SteviloObiskovalcev { get; set; }

    
    public Koncert(string izvajalec, string lokacija, int steviloObiskovalcev)
    {
        Izvajalec = izvajalec;
        Lokacija = lokacija;
        SteviloObiskovalcev = steviloObiskovalcev;
    }
    public Koncert()
    {

    }

}

class Predstava : Dogodek
{
    public string Reziser { get; set; }
    public string Lokacija { get; set; }
    public int SteviloGledalcev { get; set; }

    public Predstava(string reziser, string lokacija, int steviloGledalcev)
    {
        Reziser = reziser;
        Lokacija = lokacija;
        SteviloGledalcev = steviloGledalcev;
    }

    public Predstava()
    {

    }
}

class Poroka : Dogodek
{
    public string Mož { get; set; }
    public string Žena { get; set; }
    public string Lokacija { get; set; }

    public Poroka(string mož, string žena, string lokacija)
    {
        Mož = mož;
        Žena = žena;
        Lokacija = lokacija;
    }

    public Poroka()
    {

    }

}

class UrnikDogodkov
{
    public List<Koncert> Koncerti { get; set; }
    public List<Predstava> Predstave { get; set; }
    public List<Poroka> Poroke { get; set; }

    List<Dogodek> dogodki = new List<Dogodek>();

    

    public UrnikDogodkov()
    {
        Koncerti = new List<Koncert>();
        Predstave = new List<Predstava>();
        Poroke = new List<Poroka>();
    }
    
    public UrnikDogodkov(List<Koncert> koncerti, List<Predstava> predstave, List<Poroka> poroke)
    {
        Koncerti = koncerti;
        Predstave = predstave;
        Poroke = poroke;
    }

   

    public List<Dogodek> IskanjeDogodkovPoDatumuInLokaciji(DateTime datum, string lokacija)
    {

        List<Dogodek> iskaniDogodki = new List<Dogodek>();

        try
        {
            foreach (var dogodek in Koncerti)
            {
                if (dogodek.Datum == datum && dogodek.Lokacija.Equals(lokacija, StringComparison.OrdinalIgnoreCase))
                {
                    iskaniDogodki.Add(dogodek);
                }
            }

            foreach (var dogodek in Predstave)
            {
                if (dogodek.Datum == datum && dogodek.Lokacija.Equals(lokacija, StringComparison.OrdinalIgnoreCase))
                {
                    iskaniDogodki.Add(dogodek);
                }
            }

            foreach (var dogodek in Poroke)
            {
                if (dogodek.Datum == datum && dogodek.Lokacija.Equals(lokacija, StringComparison.OrdinalIgnoreCase))
                {
                    iskaniDogodki.Add(dogodek);
                }
            }

            if (iskaniDogodki.Count == 0)
            {
                throw new Exception("Dogodek ne obstaja");
            }
        }
        catch(Exception e)
        {
            Console.WriteLine(e.Message);
        }

        return iskaniDogodki;
    }


    public List<Dogodek> IskanjeDogodkovDatum(DateTime datum, List<Dogodek> dogodki)
    {
        List<Dogodek> iskaniDogodki = new List<Dogodek>();

        foreach (Dogodek dogodek in dogodki)
        {
            if (dogodek.Datum == datum)
            {
                iskaniDogodki.Add(dogodek);
            }
        }

        if (iskaniDogodki.Count == 0)
        {
            throw new Exception("Dogodek ne obstaja");
        }
        return iskaniDogodki;
    }
       
    public List<Dogodek> IskanjeDogodkovIme(string ime, List<Dogodek> dogodki)
    {
        List<Dogodek> iskaniDogodki = new List<Dogodek>();

        foreach (Dogodek dogodek in dogodki)
        {
            if (dogodek.Ime == ime)
            {
                iskaniDogodki.Add(dogodek);
            }
        }

        if (iskaniDogodki.Count == 0)
        {
            throw new Exception("Dogodek ne obstaja");
        }
        return iskaniDogodki;
    }


    public List<Dogodek> IskanjeDogodkovCena(int cena, List<Dogodek> dogodki)
    {
        List<Dogodek> iskaniDogodki = new List<Dogodek>();

        foreach (Dogodek dogodek in dogodki)
        {
            if (dogodek.Cena == cena)
            {
                iskaniDogodki.Add(dogodek);
            }
        }

        if (iskaniDogodki.Count == 0)
        {
            throw new Exception("Dogodek ne obstaja");
        }
        return iskaniDogodki;
    }

    
    public static List<Dogodek> BeriCSV(string imeDatoteke)
    {
        List<Dogodek> dogodki = new List<Dogodek>();
        string[] vrstice = System.IO.File.ReadAllLines(imeDatoteke);

        foreach (string vrstica in vrstice)
        {
            string[] podatki = vrstica.Split(',');
            string tip = podatki[0];

            switch (tip)
            {
                case "KONCERT":
                    Koncert koncert = new Koncert
                    {
                        Ime = podatki[1],
                        Datum = DateTime.Parse(podatki[2]),
                        Cena = int.Parse(podatki[3]),
                        Izvajalec = podatki[4],
                        Lokacija = podatki[5],
                        SteviloObiskovalcev = int.Parse(podatki[6])
                    };
                    dogodki.Add(koncert);
                    break;
                case "PREDSTAVA":
                    Predstava predstava = new Predstava
                    {
                        Ime = podatki[1],
                        Datum = DateTime.Parse(podatki[2]),
                        Cena = int.Parse(podatki[3]),
                        Reziser = podatki[4],
                        Lokacija = podatki[5],
                        SteviloGledalcev = int.Parse(podatki[6])
                    };
                    dogodki.Add(predstava);
                    break;
                case "POROKA":
                    Poroka poroka = new Poroka
                    {
                        Ime = podatki[1],
                        Datum = DateTime.Parse(podatki[2]),
                        Cena = int.Parse(podatki[3]),
                        Mož = podatki[4],
                        Žena = podatki[5],
                        Lokacija = podatki[6]
                    };
                    dogodki.Add(poroka);
                    break;
                default:
                    
                    break;
            }
        }
        return dogodki;
    }
}
    
    











