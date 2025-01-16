using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ODataRazorApp.Models;
using System.Net.Http.Json;

public class TeamsModel : PageModel
{
    private readonly HttpClient _httpClient;

    public TeamsModel(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient();
        _httpClient.BaseAddress = new Uri("https://localhost:7035/odata/");
    }

    public IEnumerable<Team> Teams { get; set; } = new List<Team>();

    [BindProperty(SupportsGet = true)]
    public string Filter { get; set; } = string.Empty;

    [BindProperty(SupportsGet = true)]
    public string SortOrder { get; set; } = "Name";

    [BindProperty(SupportsGet = true)]
    public int PageNumber { get; set; } = 1;

    [BindProperty(SupportsGet = true)]
    public int PageSize { get; set; } = 5;

    public int TotalCount { get; set; }
    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);

    [BindProperty]
    public Team Team { get; set; } = new Team();

    public async Task OnGetAsync()
    {
        Console.WriteLine($"Current Page: {PageNumber}");
        Console.WriteLine($"Page Size: {PageSize}");
        Console.WriteLine($"Filter: {Filter}");
        Console.WriteLine($"Sort Order: {SortOrder}");

        var queryParameters = new List<string>();

        if (!string.IsNullOrEmpty(Filter))
        {
            queryParameters.Add($"$filter=contains(Name,'{Filter}') or contains(Coach,'{Filter}')");
        }

        if (!string.IsNullOrEmpty(SortOrder))
        {
            queryParameters.Add($"$orderby={SortOrder}");
        }

        queryParameters.Add($"$skip={(PageNumber - 1) * PageSize}");
        queryParameters.Add($"$top={PageSize}");
        queryParameters.Add($"$count=true");

        var requestUri = "Teams?" + string.Join("&", queryParameters);

        try
        {
            var response = await _httpClient.GetFromJsonAsync<ODataResponse<Team>>(requestUri);
            if (response != null)
            {
                Teams = response.Value;
                TotalCount = response.Count;
            }
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Napaka pri pridobivanju podatkov: {ex.Message}");
            Teams = new List<Team>();
            TotalCount = 0;
        }
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (Team.TeamId == 0)
        {
            var response = await _httpClient.PostAsJsonAsync("Teams", Team);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage();
            }
        }
        else
        {
            var response = await _httpClient.PutAsJsonAsync($"Teams({Team.TeamId})", Team);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage();
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                ModelState.AddModelError(string.Empty, "Neveljavni podatki.");
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                ModelState.AddModelError(string.Empty, "Ekipa ni bila najdena.");
            }
        }

        return Page();
    }

    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"Teams({id})");
        if (response.IsSuccessStatusCode)
        {
            return RedirectToPage(new { PageNumber, PageSize, Filter, SortOrder });
        }

        ModelState.AddModelError(string.Empty, "Napaka pri brisanju ekipe.");
        return Page();
    }

    public async Task<IActionResult> OnPostLoadEditAsync(int id)
    {
        try
        {
            Team = await _httpClient.GetFromJsonAsync<Team>($"Teams({id})");
            if (Team == null)
            {
                return NotFound();
            }
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Error fetching team: {ex.Message}");
            return NotFound();
        }

        await OnGetAsync();
        return Page();
    }
}

public class ODataResponse<T>
{
    public List<T> Value { get; set; } = new List<T>();
    public int Count { get; set; }
}
