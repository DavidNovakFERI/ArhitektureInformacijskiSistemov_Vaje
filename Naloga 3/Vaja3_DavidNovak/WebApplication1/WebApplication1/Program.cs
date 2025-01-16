using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();


Razredi.Voznik.vozniki(app);
Razredi.Ekipa.ekipe(app);
Razredi.VoznikVEkipi.voznikVEkipi(app);

    

    app.Run();

