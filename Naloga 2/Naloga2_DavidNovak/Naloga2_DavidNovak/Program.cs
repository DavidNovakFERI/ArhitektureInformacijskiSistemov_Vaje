using System;
using System.IO.Pipes;

var server = new NamedPipeServerStream("Naloga2_DavidNovak");

server.WaitForConnection();
Console.WriteLine("Client connected");

var reader = new StreamReader(server);
var writer = new StreamWriter(server) { AutoFlush = true };

writer.WriteLine("IZBERITE NASLEDNJE MOŽNE OPERACIJE:  ");
writer.WriteLine("1. Modus");
writer.WriteLine("2. Povprečna vrednost");
writer.WriteLine("3. Mediana");
writer.WriteLine("4. Standardni odklon");
writer.WriteLine("5. Razpon podatkov");
writer.WriteLine("6. Izhod");

var konec = false;

while(konec != true)
{
    var clientInput = reader.ReadLine();
    switch (clientInput)
    {
        case "1":
            var loop = false;
            writer.WriteLine("Modus: ");

            List<int> modus = new List<int>();
            while (loop != true)
            {
                
                
                var clientInput2 = reader.ReadLine();
                if(clientInput2 == "X")
                {
                    writer.WriteLine("IZHOD iz Modus");
                    loop = true;
                    modus.Clear();

                }
                else if(int.TryParse(clientInput2, out int numericValue))
                {
                    modus.Add(numericValue);

                    var mode = modus.GroupBy(n => n).OrderByDescending(g => g.Count()).Select(g => g.Key).FirstOrDefault();
                    writer.WriteLine("Modus: " + mode);

                }
                else
                {
                    writer.WriteLine("Vnesli ste napačen znak");
                }
                
            }
            break;
        case "2":
            var loop2 = false;
            List<int> PovVrednost = new List<int>();
            writer.WriteLine("Povprečna vrednost: ");
            while (loop2 != true)
            {

                var clientInput3 = reader.ReadLine();
                if (clientInput3 == "X")
                {
                    writer.WriteLine("IZHOD iz Povprečne vrednosti");
                    loop2 = true;

                }
                else if (int.TryParse(clientInput3, out int numericValue))
                {
                    
                    PovVrednost.Add(numericValue);
                    var povprecnaVrednost = PovVrednost.Average();
                    writer.WriteLine("Povprečna vrednost: " + povprecnaVrednost);

                }
                else
                {
                    writer.WriteLine("Vnesli ste napačen znak");
                }

            }
            break;
        case "3":
            var loop3 = false;
            List<int> Mediana = new List<int>();
            writer.WriteLine("Mediana: ");
            while (loop3 != true)
            {

                var clientInput4 = reader.ReadLine();
                if (clientInput4 == "X")
                {
                    writer.WriteLine("IZHOD iz Mediane");
                    loop3 = true;

                }
                else if (int.TryParse(clientInput4, out int numericValue))
                {
                    
                    Mediana.Add(numericValue);
                    var median = Mediana.OrderBy(n => n).Skip((Mediana.Count - 1) / 2).Take(2 - (Mediana.Count % 2)).Average();
                    writer.WriteLine("Mediana: " + median);

                }
                else
                {
                    writer.WriteLine("Vnesli ste napačen znak");
                }

            }
            break;
        case "4":
            var loop4 = false;
            List<int> StdOdklon = new List<int>();
            writer.WriteLine("Standardni odklon: ");
            while (loop4 != true)
            {

                var clientInput5 = reader.ReadLine();
                if (clientInput5 == "X")
                {
                    writer.WriteLine("IZHOD iz Standardnega odklona");
                    loop4 = true;

                }
                else if (int.TryParse(clientInput5, out int numericValue))
                {
                    StdOdklon.Add(numericValue);
                    var povprecje = StdOdklon.Average();
                    var vsota = StdOdklon.Sum(a => Math.Pow(a - povprecje, 2));
                    var standardDeviation = Math.Sqrt((vsota) / (StdOdklon.Count() - 1));
                    writer.WriteLine("Standardni odklon: " + standardDeviation);


                }
                else
                {
                    writer.WriteLine("Vnesli ste napačen znak");
                }

            }
            break;
        case "5":
            var loop5 = false;
            List<int> Razpon = new List<int>();
            writer.WriteLine("Razpon podatkov: ");
            while (loop5 != true)
            {

                var clientInput6 = reader.ReadLine();
                if (clientInput6 == "X")
                {
                    writer.WriteLine("IZHOD iz Razpona podatkov");
                    loop5 = true;

                }
                else if (int.TryParse(clientInput6, out int numericValue))
                {
                    Razpon.Add(numericValue);
                    if(Razpon.Count == 0)
                    {
                        throw new Exception("Napaka");
                    }
                    int min = Razpon.Min();
                    int max = Razpon.Max();
                    int razpon = max - min;
                    writer.WriteLine("Razpon podatkov: " + razpon);
                }
                else
                {
                    writer.WriteLine("Vnesli ste napačen znak");
                }

            }
            break;
        case "6":
            writer.WriteLine("IZHOD");
            konec = true;
            break;     
    }

    
}


server.Disconnect();
Thread.Sleep(1000);
server.Close();




