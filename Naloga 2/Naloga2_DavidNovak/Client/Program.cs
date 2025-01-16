using System;
using System.IO.Pipes;
using System.Runtime.CompilerServices;

var client = new NamedPipeClientStream("Naloga2_DavidNovak");

client.Connect();
Console.WriteLine("Connected to server");

var reader = new StreamReader(client);
var writer = new StreamWriter(client) { AutoFlush = true };

for (int i = 0; i <= 6; i++)
{
    Console.WriteLine(reader.ReadLine());
}

var konec = false;
while (!konec)
{   
    var vnos = Console.ReadLine();
    writer.WriteLine(vnos);

    var odgovor = reader.ReadLine();
    
    

    if (odgovor == "IZHOD")
    {
        konec = true;
        Console.WriteLine(odgovor);
        client.Close();
    }
    else if (odgovor == "Modus: ")
    {
        var konec2 = false;
        Console.WriteLine(odgovor);
        while (!konec2)
        {
            var vnos2 = Console.ReadLine();
            writer.WriteLine(vnos2);
            var odgovor2 = reader.ReadLine();
            if (odgovor2 == "X")
            {
                Console.WriteLine(odgovor2);
            }
            else
            {
                Console.WriteLine(odgovor2);
            }

        }

    }else if (odgovor == "Povprečna vrednost: ")
    {
        var konec3 = false;
        Console.WriteLine(odgovor);
        while (!konec3)
        {
            var vnos3 = Console.ReadLine();
            writer.WriteLine(vnos3);
            var odgovor3 = reader.ReadLine();
            if (odgovor3 == "X")
            {
                //konec3 = true;
                Console.WriteLine(odgovor3);
            }
            else
            {
                Console.WriteLine(odgovor3);
            }

        }
    }
    else if (odgovor == "Mediana: ")
    {
        var konec4 = false;
        Console.WriteLine(odgovor);
        while (!konec4)
        {
            var vnos4 = Console.ReadLine();
            writer.WriteLine(vnos4);
            var odgovor4 = reader.ReadLine();
            if (odgovor4 == "X")
            {
                //konec4 = true;
                Console.WriteLine(odgovor4);
                
            }
            else
            {
                Console.WriteLine(odgovor4);
            }

        }
    }
    else if (odgovor == "Standardni odklon: ")
    {
        var konec5 = false;
        Console.WriteLine(odgovor);
        while (!konec5)
        {
            var vnos5 = Console.ReadLine();
            writer.WriteLine(vnos5);
            var odgovor5 = reader.ReadLine();
            if (odgovor5 == "X")
            {
                //konec5 = true;
                Console.WriteLine(odgovor5);
                
            }
            else
            {
                Console.WriteLine(odgovor5);
            }

        }
    }
    else if (odgovor == "Razpon podatkov: ")
    {
        var konec6 = false;
        Console.WriteLine(odgovor);
        while (!konec6)
        {
            var vnos6 = Console.ReadLine();
            writer.WriteLine(vnos6);
            var odgovor6 = reader.ReadLine();
            if (odgovor6 == "X")
            {
                //konec6 = true;
                Console.WriteLine(odgovor6);
                
            }
            else
            {
                Console.WriteLine(odgovor6);
            }

        }
    }
    

    client.Close();
}


