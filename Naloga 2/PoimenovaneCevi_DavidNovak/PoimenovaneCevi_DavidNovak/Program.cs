using System.IO.Pipes;

var server = new NamedPipeServerStream("FERI-TOREK");
server.WaitForConnection();

Console.WriteLine("Povezava je bila vzpostavljena");

var writer = new StreamWriter(server);
var reader = new StreamReader(server);

var stevec = 0.0;

writer.WriteLine("Dobrodošli v števec. Vnesi število ali izberite X za nadeljevanje");
writer.Flush();

var ClientVnos = reader.ReadLine();
var stevilo = double.Parse(ClientVnos, System.Globalization.NumberStyles.AllowDecimalPoint);

stevec+= stevilo;

writer.WriteLine("Trenutna vrednost je: " + stevec);
writer.Flush();
