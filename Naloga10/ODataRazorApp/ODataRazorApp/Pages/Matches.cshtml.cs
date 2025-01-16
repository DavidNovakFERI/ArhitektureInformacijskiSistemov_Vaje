using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ODataRazorApp.Models;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;

public class MatchesModel : PageModel
{
    private readonly HttpClient _httpClient;

    public MatchesModel(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient();
        _httpClient.BaseAddress = new Uri("https://localhost:7035/odata/");
    }

    // Initialize with an empty list to avoid null reference issues
    public IEnumerable<Match> Matches { get; set; } = new List<Match>();

    [BindProperty]
    public Match Match { get; set; } = new Match();

    public async Task OnGetAsync()
    {
        try
        {
            var response = await _httpClient.GetFromJsonAsync<ODataResponse<Match>>("Matches");
            Matches = response?.Value ?? new List<Match>();
        }
        catch (Exception ex)
        {
            // Log the exception or handle it as necessary
            Console.WriteLine($"Error fetching matches: {ex.Message}");
            Matches = new List<Match>();  // Fallback to an empty list
        }
    }

    public async Task<IActionResult> OnPostCreateAsync()
    {
        if (ModelState.IsValid)
        {
            var response = await _httpClient.PostAsJsonAsync("Matches", Match);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage();
            }
            ModelState.AddModelError(string.Empty, "Failed to create match.");
        }
        return Page();
    }

    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"Matches({id})");
        if (response.IsSuccessStatusCode)
        {
            return RedirectToPage();
        }
        ModelState.AddModelError(string.Empty, "Failed to delete match.");
        return Page();
    }
}
