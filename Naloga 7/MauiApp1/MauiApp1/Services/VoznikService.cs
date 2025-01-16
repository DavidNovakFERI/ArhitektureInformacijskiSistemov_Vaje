using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using MauiApp1.Models;

namespace MauiApp1.Services
{
    public class VoznikService
    {
        private readonly HttpClient _httpClient;

        public VoznikService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ObservableCollection<Voznik>> GetVoznikiAsync()
        {
            return await _httpClient.GetFromJsonAsync<ObservableCollection<Voznik>>("api/Voznik");
        }

        public async Task<Voznik> GetVoznikByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Voznik>($"api/Voznik/{id}");
        }

        public async Task<Voznik> AddVoznikAsync(Voznik voznik)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Voznik", voznik);
            if (response.IsSuccessStatusCode)
            {
                // Preberi ustvarjenega voznika s serverja, da dobimo Id
                var createdVoznik = await response.Content.ReadFromJsonAsync<Voznik>();
                return createdVoznik;
            }
            return null;
        }



        public async Task<bool> UpdateVoznikAsync(int id, Voznik voznik)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Voznik/{id}", voznik);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteVoznikAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Voznik/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}