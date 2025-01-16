using System.IO.Pipes;

var client = new NamedPipeClientStream("FERI-TOREK");
client.Connect();
Console.WriteLine("Povezava je bila vzpostavljena");

var writer = new StreamWriter(client);
var reader = new StreamReader(client);

Console.WriteLine(reader.ReadLine());

var vnos = Console.ReadLine();
writer.WriteLine(vnos);

Console.WriteLine(reader.ReadLine());



Console.ReadLine();

