using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using Microsoft.EntityFrameworkCore;
using WebApplication1;
using static WebApplication1.Models.Razredi;
using System.IO.Pipelines;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Http;



var builder = WebApplication.CreateBuilder(args);

var AllowAny = "_allowAny";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: AllowAny, policy =>
    {
        policy.AllowAnyOrigin();
        policy.AllowAnyHeader();
    });
});

//API
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1", Description = "F1 API" });
    c.EnableAnnotations();
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

app.UseCors(AllowAny);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

using var db = new RacingDbContext();
db.Vozniki.RemoveRange(db.Vozniki);
db.Ekipe.RemoveRange(db.Ekipe);
db.VoznikVEkipi.RemoveRange(db.VoznikVEkipi);
db.SaveChanges();

Voznik voznik1 = new Voznik("Lewis Hamilton", 44, 1985, false);
db.Vozniki.Add(voznik1);
Voznik voznik2 = new Voznik("Valtteri Bottas", 77, 1989, false);
db.Vozniki.Add(voznik2);
Voznik voznik3 = new Voznik("Max Verstappen", 33, 1997, false);
db.Vozniki.Add(voznik3);
Voznik voznik4 = new Voznik("Sergio Perez", 11, 1990, false);
db.Vozniki.Add(voznik4);
Voznik voznik5 = new Voznik("Lando Norris", 4, 2000, true);
db.Vozniki.Add(voznik5);


Ekipa ekipa1 = new Ekipa("Mercedes", "Nemèija", 2010);
db.Ekipe.Add(ekipa1);
Ekipa ekipa2 = new Ekipa("Red Bull Racing", "Avstrija", 2005);
db.Ekipe.Add(ekipa2);
Ekipa ekipa3 = new Ekipa("McLaren", "Velika Britanija", 1966);
db.Ekipe.Add(ekipa3);
Ekipa ekipa4 = new Ekipa("Ferrari", "Italija", 1950);
db.Ekipe.Add(ekipa4);
Ekipa ekipa5 = new Ekipa("Alpine", "Francija", 1977);
db.Ekipe.Add(ekipa5);

VoznikVEkipi voznikVEkipi1 = new VoznikVEkipi(voznik1, ekipa1, 2013, 0, 270, 103);
db.VoznikVEkipi.Add(voznikVEkipi1);
VoznikVEkipi voznikVEkipi2 = new VoznikVEkipi(voznik2, ekipa1, 2017, 0, 161, 10);
db.VoznikVEkipi.Add(voznikVEkipi2);
VoznikVEkipi voznikVEkipi3 = new VoznikVEkipi(voznik3, ekipa2, 2016, 0, 128, 18);
db.VoznikVEkipi.Add(voznikVEkipi3);
VoznikVEkipi voznikVEkipi4 = new VoznikVEkipi(voznik4, ekipa2, 2021, 0, 205, 2);
db.VoznikVEkipi.Add(voznikVEkipi4);
VoznikVEkipi voznikVEkipi5 = new VoznikVEkipi(voznik5, ekipa3, 2019, 0, 60, 1);
db.VoznikVEkipi.Add(voznikVEkipi5);

db.SaveChanges();
app.MapGet("/vozniki", () =>
{
    return db.Vozniki;
}
);

app.MapGet("/PodrobnostiVoznikov/{ImePriimek}", (string ImePriimek) =>
{
    List<Voznik> vozniki2 = new List<Voznik>();
    foreach (var item in db.Vozniki)
    {
        if (item.ImePriimek == ImePriimek)
        {
            vozniki2.Add(item);
        }
    }
    return vozniki2;
}
).WithMetadata(new SwaggerOperationAttribute("PodrobnostiVoznikov", "Metoda ki vrne podrobnosti o voznikih"));

app.MapGet("/najstarejsiVoznik", () =>
{
    Voznik NajStarejsi = db.Vozniki.OrderByDescending(x => x.letoRojstva).Last();
    return NajStarejsi;
}
).WithMetadata(new SwaggerOperationAttribute("najstarejsiVoznik", "Metoda ki vrne najstarejsega voznika"));

app.MapGet("/povprecnaStarost", () =>
{
    int vsotaLet = 0;
    int povprecnaStarost = 0;
    foreach (var item in db.Vozniki)
    {
        vsotaLet += item.letoRojstva;
    }
    povprecnaStarost = 2023 - (vsotaLet / db.Vozniki.Count());
    return povprecnaStarost;
}
).WithMetadata(new SwaggerOperationAttribute("povprecnaStarost", "Metoda ki vrne povprecno starost voznikov"));

app.MapGet("/ekipa", () =>
{
    return db.Ekipe;
}
).WithMetadata(new SwaggerOperationAttribute("ekipa", "Metoda ki vrne ekipe"));


app.MapGet("/voznikVEkipi", () =>
{
    return db.VoznikVEkipi;
}
).WithMetadata(new SwaggerOperationAttribute("voznikVEkipi", "Metoda ki vrne voznike v ekipi"));


app.MapGet("/voznikVEkipi/{ImePriimek}", (string ImePriimek) =>
{
    List<VoznikVEkipi> voznikVEkipi2 = new List<VoznikVEkipi>();
    foreach (var item in db.VoznikVEkipi)
    {
        if (item.Voznik.ImePriimek == ImePriimek)
        {
            voznikVEkipi2.Add(item);
        }
    }
    return voznikVEkipi2;
}
).WithMetadata(new SwaggerOperationAttribute("voznikVEkipi", "Metoda ki vrne voznike v ekipi"));


app.MapGet("/najvecVoznikov", () =>
{
    int steviloVoznikov = 1;
    string imeEkipe = "";
    foreach (var item in db.VoznikVEkipi)
    {
        if (steviloVoznikov < item.Ekipa.Ime.Count())
        {
            steviloVoznikov = item.Ekipa.Ime.Count();
            imeEkipe = item.Ekipa.Ime;
        }
    }
    return imeEkipe;
}
).WithMetadata(new SwaggerOperationAttribute("najvecVoznikov", "Metoda ki vrne ekipo z najvec vozniki"));

//naloga 4


//1
app.MapPost("/DodajVoznika", ([FromBody] Voznik voznik) =>
{
    db.Vozniki.Add(voznik);
    db.SaveChanges();
    return Results.Ok(voznik);
}
).WithMetadata(new SwaggerOperationAttribute("DodajVoznika", "Metoda ki doda voznika"));

app.MapPost("/DodajEkipo", ([FromBody] Ekipa ekipa) =>
{
    db.Ekipe.Add(ekipa);
    db.SaveChanges();
    return Results.Ok(ekipa);
}
).WithMetadata(new SwaggerOperationAttribute("DodajEkipo", "Metoda ki doda ekipo"));

app.MapPost("/DodajVoznikaVEkipo", ([FromBody] VoznikVEkipi voznikVEkipi) =>
{
    db.VoznikVEkipi.Add(voznikVEkipi);
    db.SaveChanges();
    return voznikVEkipi;
}
).WithMetadata(new SwaggerOperationAttribute("DodajVoznikaVEkipo", "Metoda ki doda voznika v ekipo"));

//2

app.MapPut("/UrediVoznika/{id}", (int id, [FromBody] Voznik voznik) =>
{
    var voznik2 = db.Vozniki.Find(id);
    voznik2.ImePriimek = voznik.ImePriimek;
    voznik2.StevilkaAvta = voznik.StevilkaAvta;
    voznik2.letoRojstva = voznik.letoRojstva;
    db.SaveChanges();
    return Results.Ok(voznik2);
}
).WithMetadata(new SwaggerOperationAttribute("UrediVoznika", "Metoda ki uredi voznika"));

//3

app.MapDelete("/IzbrisiVoznika/{id}", (int id) =>
{
    var voznik = db.Vozniki.Find(id);
    db.Vozniki.Remove(voznik);
    db.SaveChanges();
    return Results.Ok(voznik);
}
).WithMetadata(new SwaggerOperationAttribute("IzbrisiVoznika", "Metoda ki izbrise voznika")).Produces(StatusCodes.Status201Created);

//4

app.MapPost("/DodajVoznikaVEkipo/{idVoznika}/{idEkipe}", (int idVoznika, int idEkipe) =>
{
    var voznik = db.Vozniki.Find(idVoznika);
    var ekipa = db.Ekipe.Find(idEkipe);
    VoznikVEkipi voznikVEkipi = new VoznikVEkipi(voznik, ekipa, 0, 0, 0, 0);
    db.VoznikVEkipi.Add(voznikVEkipi);
    db.SaveChanges();
    return Results.Ok(voznikVEkipi);
}
).WithMetadata(new SwaggerOperationAttribute("DodajVoznikaVEkipo", "Metoda ki doda voznika v ekipo")).Produces(StatusCodes.Status201Created);

//5

app.MapDelete("/IzbrisiVoznikaVEkipo/{idVoznika}/{idEkipe}", (int idVoznika, int idEkipe) =>
{
    var voznikVEkipi = db.VoznikVEkipi.Find(idVoznika, idEkipe);
    db.VoznikVEkipi.Remove(voznikVEkipi);
    db.SaveChanges();
    return Results.Ok(voznikVEkipi);
}
).WithMetadata(new SwaggerOperationAttribute("IzbrisiVoznikaVEkipo", "Metoda ki izbrise voznika iz ekipe")).Produces(StatusCodes.Status201Created);

//app.MapPost("/DodajVoznikaVEkipo", (VoznikVEkipi voznikVEkipi) =>
//{
//    using var transaction = db.Database.BeginTransaction();

//    try
//    {
//        db.Vozniki.Add(voznikVEkipi.Voznik);
//        db.SaveChanges();

//        db.VoznikVEkipi.Add(voznikVEkipi);
//        db.SaveChanges();

//        transaction.Commit();
//        return Results.Ok(voznikVEkipi);
//    }
//    catch (Exception)
//    {
//        transaction.Rollback();
//        return Results.BadRequest();
//    }

//}
//);










app.Run();

