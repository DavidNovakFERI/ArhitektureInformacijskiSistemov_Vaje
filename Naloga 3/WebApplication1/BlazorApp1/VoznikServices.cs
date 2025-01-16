using System;
using BlazorApp1.Razredi;
using static BlazorApp1.Razredi.Razredi;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;


namespace BlazorApp1
{
    public class VoznikServices
    {
        private readonly HttpClient httpClient;
        IEnumerable<Voznik>? vozniki; 

        public VoznikServices(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        //Metoda za pridobivanje vseh voznikov
        public async Task<IEnumerable<Voznik>> GetVozniki(int id, string ImePriimek, int StevilkaAvta, int letoRojstva)
        {
        Voznik v1 = new() { Id = id, ImePriimek = ImePriimek, StevilkaAvta = StevilkaAvta, letoRojstva = letoRojstva };  
        await httpClient.PostAsJsonAsync("https://localhost:7200/vozniki", v1);
            return vozniki!;
        
        }
        
         

        //metoda za pridobivanje podrobnosti voznika
        public async Task<List<Voznik>> GetPodrobnostiVoznikov(string ImePriimek)
        {
            return await httpClient.GetFromJsonAsync<List<Voznik>>($"api/PodrobnostiVoznikov/{ImePriimek}");
            
        }

        //metoda za dodajanje voznika
        public async Task<Voznik> AddVoznik(Voznik voznik)
        {
            var response = await httpClient.PostAsJsonAsync("api/vozniki", voznik);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Voznik>();
            }
            return null;
        }

        //metoda za posodobitev voznika
        public async Task<Voznik> UpdateVoznik(Voznik voznik)
        {
            var response = await httpClient.PutAsJsonAsync($"api/vozniki/{voznik.Id}", voznik);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Voznik>();
            }

            return null;
        }

        //metoda za brisanje voznika
        public async Task DeleteVoznik(int id)
        {
            HttpResponseMessage response = await httpClient.DeleteAsync($"api/vozniki/{id}");

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Error deleting voznik. Status code: {response.StatusCode}");
            }
        }
    }
}
