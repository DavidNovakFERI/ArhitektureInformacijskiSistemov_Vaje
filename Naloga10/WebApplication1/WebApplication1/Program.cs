using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using WebApplication1;

var builder = WebApplication.CreateBuilder(args);

// Konfigurira EF Core za uporabo SQLite z doloèeno povezavo
builder.Services.AddDbContext<FootballContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Konfiguracija OData z omogoèanjem glavnih poizvedovalnih možnosti
builder.Services.AddControllers().AddOData(opt =>
    opt.Select()
        .Filter()
        .Expand()
        .Count()
        .OrderBy()
        .SetMaxTop(100)
        .AddRouteComponents("odata", GetEdmModel()));

// Definira OData Entity Data Model (EDM)
IEdmModel GetEdmModel()
{
    var odataBuilder = new ODataConventionModelBuilder();
    odataBuilder.EntitySet<Team>("Teams"); // Definira entiteto "Teams" kot del modela
    odataBuilder.EntitySet<Player>("Players"); // Definira entiteto "Players"
    odataBuilder.EntitySet<Match>("Matches"); // Definira entiteto "Matches"
    odataBuilder.EntitySet<League>("Leagues"); // Definira entiteto "Leagues"
    odataBuilder.EntitySet<Statistic>("Statistics"); // Definira entiteto "Statistics"
    return odataBuilder.GetEdmModel(); // Vrne zgrajen EDM model
}

var app = builder.Build();

app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
