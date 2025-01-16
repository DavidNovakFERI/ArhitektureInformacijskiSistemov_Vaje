using BlazorApp2;
using Blazorise;
using Blazorise.Charts;
using Blazorise.Bootstrap;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services
    .AddBlazorise(options =>
    {
        options.Immediate = true;
    })
    .AddBootstrapProviders();
    



builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7253/") });

await builder.Build().RunAsync();
