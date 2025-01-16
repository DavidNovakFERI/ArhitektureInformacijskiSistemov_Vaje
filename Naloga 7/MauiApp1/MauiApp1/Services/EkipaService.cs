using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using MauiApp1.Models;

namespace MauiApp1.Services
{
    public class EkipaService
    {
        private readonly HttpClient _httpClient;

        public EkipaService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ObservableCollection<Ekipa>> GetEkipeAsync()
        {
            return await _httpClient.GetFromJsonAsync<ObservableCollection<Ekipa>>("api/Ekipa");
        }

        public async Task<Ekipa> GetEkipaByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Ekipa>($"api/Ekipa/{id}");
        }

        public async Task<Ekipa> AddEkipaAsync(Ekipa ekipa)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Ekipa", ekipa);
            if (response.IsSuccessStatusCode)
            {
                var createdEkipa = await response.Content.ReadFromJsonAsync<Ekipa>();
                return createdEkipa;
            }
            return null;
        }

        public async Task<bool> UpdateEkipaAsync(int id, Ekipa ekipa)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Ekipa/{id}", ekipa);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteEkipaAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Ekipa/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
