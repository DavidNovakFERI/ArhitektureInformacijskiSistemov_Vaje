using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Net.Client;
using System.Data;
using Vaja6;


using var chanell = GrpcChannel.ForAddress("https://localhost:7178");

var client = new Voznik.VoznikClient(chanell);



var response = client.CreateVoznik(new CreateVoznikRequest { Ime = "Lando", Priimek = "Norris", Starost = 20, Stevilkaformule = 10, Stevilozmag = 0 });

//implementiraj za read
var response1 = client.ReadVoznik(new ReadVoznikRequest { Id = 5 });
Console.WriteLine("Voznik: " + response1.Ime + " " + response1.Priimek + " " + response1.Starost + " " + response1.Stevilkaformule + " " + response1.Stevilozmag);

//implementiraj za update
var response2 = client.UpdateVoznik(new UpdateVoznikRequest { Id = 11, Ime = "Lewis", Priimek = "Hamilton", Starost = 40, Stevilkaformule = 2, Stevilozmag = 100 });


//implementiraj za delete
var response3 = client.DeleteVoznik(new DeleteVoznikRequest { Id = 14 });









Console.ReadLine();