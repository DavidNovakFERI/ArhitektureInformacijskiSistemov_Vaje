using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ODataRazorApp.Models;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;

public class PlayersModel : PageModel
{
    private readonly HttpClient _httpClient;

    public PlayersModel(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient();
        _httpClient.BaseAddress = new Uri("https://localhost:7035/odata/");
    }

    public IEnumerable<Player> Players { get; set; }

    [BindProperty]
    public Player Player { get; set; } = new Player();

    public async Task OnGetAsync()
    {
        var response = await _httpClient.GetFromJsonAsync<ODataResponse<Player>>("Players");
        Players = response?.Value ?? new List<Player>();
    }

    public async Task<IActionResult> OnPostCreateAsync()
    {
        if (ModelState.IsValid)
        {
            var response = await _httpClient.PostAsJsonAsync("Players", Player);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage();
            }
            ModelState.AddModelError(string.Empty, "Failed to create player.");
        }
        return Page();
    }

    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"Players({id})");
        if (response.IsSuccessStatusCode)
        {
            return RedirectToPage();
        }
        ModelState.AddModelError(string.Empty, "Failed to delete player.");
        return Page();
    }
}
