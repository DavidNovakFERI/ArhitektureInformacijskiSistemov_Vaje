using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ODataRazorApp.Models;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;

public class StatisticsModel : PageModel
{
    private readonly HttpClient _httpClient;

    public StatisticsModel(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient();
        _httpClient.BaseAddress = new Uri("https://localhost:7035/odata/");
    }

    public IEnumerable<Statistic> Statistics { get; set; }

    [BindProperty]
    public Statistic Statistic { get; set; } = new Statistic();

    public async Task OnGetAsync()
    {
        var response = await _httpClient.GetFromJsonAsync<ODataResponse<Statistic>>("Statistics");
        Statistics = response?.Value ?? new List<Statistic>();
    }

    public async Task<IActionResult> OnPostCreateAsync()
    {
        if (ModelState.IsValid)
        {
            var response = await _httpClient.PostAsJsonAsync("Statistics", Statistic);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage();
            }
            ModelState.AddModelError(string.Empty, "Failed to create statistic.");
        }
        return Page();
    }

    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"Statistics({id})");
        if (response.IsSuccessStatusCode)
        {
            return RedirectToPage();
        }
        ModelState.AddModelError(string.Empty, "Failed to delete statistic.");
        return Page();
    }
}
