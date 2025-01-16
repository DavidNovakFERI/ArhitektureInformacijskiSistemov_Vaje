var builder = WebApplication.CreateBuilder(args);

// Dodajte Razor Pages storitev
builder.Services.AddRazorPages();

// Dodajte HttpClient storitev
builder.Services.AddHttpClient();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
