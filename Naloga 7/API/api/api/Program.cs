using api;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();  // Registracija kontrolerjev
builder.Services.AddDbContext<Formula1Context>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("Formula1Database")));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Definicija CORS pravilnika
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      builder =>
                      {
                          builder.WithOrigins("https://localhost:7192", "https://localhost:7253")  // Dodajte tudi izvor Blazor aplikacije
                                 .AllowAnyHeader()
                                 .AllowAnyMethod()
                                 .AllowCredentials();
                      });
});


var app = builder.Build();

// Uporaba CORS pravilnika
app.UseCors(MyAllowSpecificOrigins);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();  // Registracija poti za kontrolerje

app.Run();
