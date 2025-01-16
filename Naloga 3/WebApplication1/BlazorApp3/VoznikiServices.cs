using System;
using BlazorApp3.Razredi;
using static BlazorApp3.Razredi.Razredi;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using System.Text.Json;

namespace BlazorApp3
{
    public class VoznikServices
    {
        private readonly HttpClient httpClient;

        public VoznikServices(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IEnumerable<Voznik>> GetVozniki()
        {
            return await httpClient.GetFromJsonAsync<Voznik[]>("https://localhost:7200/vozniki");
        }

        public async Task<Voznik> UpdateVoznik(Voznik voznik)
        {
            var response = await httpClient.PutAsJsonAsync($"https://localhost:7200/UrediVoznika/{voznik.Id}", voznik);

            if (response.IsSuccessStatusCode)
            {
                
                return await response.Content.ReadFromJsonAsync<Voznik>();
            }
            else
            {
                
                throw new HttpRequestException($"Failed to update voznik. Status code: {response.StatusCode}");
            }
        }

        public async Task DeleteVoznik(int id)
        {
            var response = await httpClient.DeleteAsync($"https://localhost:7200/IzbrisiVoznika/{id}");
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Error deleting voznik. Status code: {response.StatusCode}");
            }
        }

        


    }
}
