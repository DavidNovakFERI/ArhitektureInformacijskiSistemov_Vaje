using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ODataRazorApp.Models;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;

public class LeaguesModel : PageModel
{
    private readonly HttpClient _httpClient;

    public LeaguesModel(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient();
        _httpClient.BaseAddress = new Uri("https://localhost:7035/odata/");
    }

    public IEnumerable<League> Leagues { get; set; }

    [BindProperty]
    public League League { get; set; } = new League();

    public async Task OnGetAsync()
    {
        var response = await _httpClient.GetFromJsonAsync<ODataResponse<League>>("Leagues");
        Leagues = response?.Value ?? new List<League>();
    }

    public async Task<IActionResult> OnPostCreateAsync()
    {
        if (ModelState.IsValid)
        {
            var response = await _httpClient.PostAsJsonAsync("Leagues", League);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage();
            }
            ModelState.AddModelError(string.Empty, "Failed to create league.");
        }
        return Page();
    }

    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"Leagues({id})");
        if (response.IsSuccessStatusCode)
        {
            return RedirectToPage();
        }
        ModelState.AddModelError(string.Empty, "Failed to delete league.");
        return Page();
    }
}
