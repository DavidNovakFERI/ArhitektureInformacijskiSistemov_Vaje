using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using MauiApp1.Models;

namespace MauiApp1.Services
{
    public class VoznikVEkipiService
    {
        private readonly HttpClient _httpClient;

        public VoznikVEkipiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ObservableCollection<VoznikVEkipi>> GetVoznikiVEkipiAsync()
        {
            return await _httpClient.GetFromJsonAsync<ObservableCollection<VoznikVEkipi>>("api/VoznikVEkipi");
        }

        public async Task<VoznikVEkipi> GetVoznikVEkipiByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<VoznikVEkipi>($"api/VoznikVEkipi/{id}");
        }

        public async Task<VoznikVEkipi> AddVoznikVEkipiAsync(VoznikVEkipi voznikVEkipi)
        {
            var response = await _httpClient.PostAsJsonAsync("api/VoznikVEkipi", voznikVEkipi);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<VoznikVEkipi>();
            }
            return null;
        }

        public async Task<bool> UpdateVoznikVEkipiAsync(int id, VoznikVEkipi voznikVEkipi)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/VoznikVEkipi/{id}", voznikVEkipi);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteVoznikVEkipiAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/VoznikVEkipi/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> AddVoznikToEkipaAsync(int voznikId, int ekipaId, int letoOd, int letoDo, int steviloZmag)
        {
            var voznikVEkipi = new VoznikVEkipi
            {
                VoznikId = voznikId,
                EkipaId = ekipaId,
                LetoOd = letoOd,
                LetoDo = letoDo,
                SteviloZmag = steviloZmag
            };

            var response = await _httpClient.PostAsJsonAsync("api/VoznikVEkipi/AddVoznikToEkipa", voznikVEkipi);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> RemoveVoznikFromEkipaAsync(int voznikId, int ekipaId)
        {
            var response = await _httpClient.DeleteAsync($"api/VoznikVEkipi/RemoveVoznikFromEkipa?voznikId={voznikId}&ekipaId={ekipaId}");
            return response.IsSuccessStatusCode;
        }
    }
}
