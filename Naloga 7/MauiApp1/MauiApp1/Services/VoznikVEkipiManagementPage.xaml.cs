using System.Collections.ObjectModel;
using MauiApp1.Models;
using MauiApp1.Services;
using Microsoft.Maui.Controls;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;

namespace MauiApp1
{
    public partial class VoznikVEkipiManagementPage : ContentPage, INotifyPropertyChanged
    {
        private readonly VoznikVEkipiService _voznikVEkipiService;
        private readonly VoznikService _voznikService;
        private readonly EkipaService _ekipaService;

        public ObservableCollection<VoznikVEkipi> VoznikiVEkipi { get; set; }
        private VoznikVEkipi _selectedVoznikVEkipi;

        public VoznikVEkipi SelectedVoznikVEkipi
        {
            get => _selectedVoznikVEkipi;
            set
            {
                if (_selectedVoznikVEkipi != value)
                {
                    _selectedVoznikVEkipi = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(IsVoznikVEkipiSelected));
                    OnSelectedVoznikVEkipiChanged();
                }
            }
        }

        public bool IsVoznikVEkipiSelected => SelectedVoznikVEkipi != null;

        public VoznikVEkipiManagementPage(VoznikVEkipiService voznikVEkipiService, VoznikService voznikService, EkipaService ekipaService)
        {
            InitializeComponent();
            _voznikVEkipiService = voznikVEkipiService;
            _voznikService = voznikService;
            _ekipaService = ekipaService;
            VoznikiVEkipi = new ObservableCollection<VoznikVEkipi>();
            BindingContext = this;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadVoznikiVEkipiAsync();
        }

        private async Task LoadVoznikiVEkipiAsync()
        {
            var voznikiVEkipi = await _voznikVEkipiService.GetVoznikiVEkipiAsync();
            VoznikiVEkipi.Clear();
            foreach (var voznikVEkipi in voznikiVEkipi)
            {
                VoznikiVEkipi.Add(voznikVEkipi);
            }
        }

        private void OnSelectedVoznikVEkipiChanged()
        {
            if (SelectedVoznikVEkipi != null)
            {
                VoznikImeEntry.Text = SelectedVoznikVEkipi.Voznik.Ime;
                EkipaNazivEntry.Text = SelectedVoznikVEkipi.Ekipa.Naziv;
                LetoOdEntry.Text = SelectedVoznikVEkipi.LetoOd.ToString();
                LetoDoEntry.Text = SelectedVoznikVEkipi.LetoDo.ToString();
                SteviloZmagEntry.Text = SelectedVoznikVEkipi.SteviloZmag.ToString();
            }
            else
            {
                ClearInputs(false);
            }
        }

        private async void OnAddVoznikVEkipiClicked(object sender, System.EventArgs e)
        {
            var voznikId = await GetVoznikIdByNameAsync(VoznikImeEntry.Text);
            var ekipaId = await GetEkipaIdByNameAsync(EkipaNazivEntry.Text);

            if (voznikId == null || ekipaId == null)
            {
                await DisplayAlert("Napaka", "Voznik ali ekipa z navedenim imenom/nazivom ne obstajata.", "OK");
                return;
            }

            var voznikVEkipi = new VoznikVEkipi
            {
                VoznikId = voznikId.Value,
                EkipaId = ekipaId.Value,
                LetoOd = int.Parse(LetoOdEntry.Text),
                LetoDo = int.Parse(LetoDoEntry.Text),
                SteviloZmag = int.Parse(SteviloZmagEntry.Text)
            };

            var addedVoznikVEkipi = await AddVoznikVEkipi(voznikVEkipi);
            if (addedVoznikVEkipi != null)
            {
                VoznikiVEkipi.Add(addedVoznikVEkipi);
                ClearInputs(true);
            }
        }

        private async void OnUpdateVoznikVEkipiClicked(object sender, System.EventArgs e)
        {
            if (SelectedVoznikVEkipi == null || SelectedVoznikVEkipi.Id == 0)
            {
                await DisplayAlert("Napaka", "Izberite veljavno povezavo za posodobitev.", "OK");
                return;
            }

            SelectedVoznikVEkipi.VoznikId = await GetVoznikIdByNameAsync(VoznikImeEntry.Text) ?? 0;
            SelectedVoznikVEkipi.EkipaId = await GetEkipaIdByNameAsync(EkipaNazivEntry.Text) ?? 0;
            SelectedVoznikVEkipi.LetoOd = int.Parse(LetoOdEntry.Text);
            SelectedVoznikVEkipi.LetoDo = int.Parse(LetoDoEntry.Text);
            SelectedVoznikVEkipi.SteviloZmag = int.Parse(SteviloZmagEntry.Text);

            bool uspeh = await UpdateVoznikVEkipi(SelectedVoznikVEkipi);
            if (uspeh)
            {
                await DisplayAlert("Uspeh", "Povezava je bila posodobljena.", "OK");
                await LoadVoznikiVEkipiAsync();  
            }
            else
            {
                await DisplayAlert("Napaka", "Posodobitev povezave ni uspela.", "OK");
            }
        }

        private async void OnDeleteVoznikVEkipiClicked(object sender, System.EventArgs e)
        {
            if (SelectedVoznikVEkipi == null || SelectedVoznikVEkipi.Id == 0)
            {
                await DisplayAlert("Napaka", "Izberite veljavno povezavo za izbris.", "OK");
                return;
            }

            bool uspeh = await DeleteVoznikVEkipi(SelectedVoznikVEkipi.Id);
            if (uspeh)
            {
                await DisplayAlert("Uspeh", "Povezava je bila izbrisana.", "OK");
                await LoadVoznikiVEkipiAsync();  
            }
            else
            {
                await DisplayAlert("Napaka", "Izbris povezave ni uspel.", "OK");
            }
        }

        public async Task<VoznikVEkipi> AddVoznikVEkipi(VoznikVEkipi voznikVEkipi)
        {
            var createdVoznikVEkipi = await _voznikVEkipiService.AddVoznikVEkipiAsync(voznikVEkipi);
            if (createdVoznikVEkipi != null)
            {
                return createdVoznikVEkipi;
            }
            else
            {
                await DisplayAlert("Napaka", "Dodajanje povezave ni uspelo.", "OK");
                return null;
            }
        }

        public async Task<bool> UpdateVoznikVEkipi(VoznikVEkipi voznikVEkipi)
        {
            var response = await _voznikVEkipiService.UpdateVoznikVEkipiAsync(voznikVEkipi.Id, voznikVEkipi);

            if (response)
            {
                var existingVoznikVEkipi = VoznikiVEkipi.FirstOrDefault(v => v.Id == voznikVEkipi.Id);
                if (existingVoznikVEkipi != null)
                {
                    var index = VoznikiVEkipi.IndexOf(existingVoznikVEkipi);
                    VoznikiVEkipi[index] = voznikVEkipi;
                    ClearInputs(true);
                }
                return true;
            }
            else
            {
                await DisplayAlert("Napaka", "Posodobitev povezave ni uspela. Preverite povezavo ali API.", "OK");
            }

            return false;
        }

        public async Task<bool> DeleteVoznikVEkipi(int id)
        {
            if (await _voznikVEkipiService.DeleteVoznikVEkipiAsync(id))
            {
                var voznikVEkipiToRemove = VoznikiVEkipi.FirstOrDefault(v => v.Id == id);
                if (voznikVEkipiToRemove != null)
                {
                    VoznikiVEkipi.Remove(voznikVEkipiToRemove);
                    ClearInputs(true);
                }
                return true;
            }
            return false;
        }

        private async Task<int?> GetVoznikIdByNameAsync(string ime)
        {
            var vozniki = await _voznikService.GetVoznikiAsync();
            var voznik = vozniki.FirstOrDefault(v => v.Ime == ime);
            return voznik?.Id;
        }

        private async Task<int?> GetEkipaIdByNameAsync(string naziv)
        {
            var ekipe = await _ekipaService.GetEkipeAsync();
            var ekipa = ekipe.FirstOrDefault(e => e.Naziv == naziv);
            return ekipa?.Id;
        }

        private void ClearInputs(bool resetSelection)
        {
            VoznikImeEntry.Text = string.Empty;
            EkipaNazivEntry.Text = string.Empty;
            LetoOdEntry.Text = string.Empty;
            LetoDoEntry.Text = string.Empty;
            SteviloZmagEntry.Text = string.Empty;

            if (resetSelection)
            {
                SelectedVoznikVEkipi = null;
            }
        }

        private async void OnBackToMainPageClicked(object sender, System.EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private void OnSortByNameClicked(object sender, EventArgs e)
        {
            var sortedList = VoznikiVEkipi.OrderBy(v => v.Voznik.Ime).ToList();
            UpdateVoznikiVEkipiCollection(sortedList);
        }

        private void OnSortByTeamNameClicked(object sender, EventArgs e)
        {
            var sortedList = VoznikiVEkipi.OrderBy(v => v.Ekipa.Naziv).ToList();
            UpdateVoznikiVEkipiCollection(sortedList);
        }

        private void OnSortByYearFromClicked(object sender, EventArgs e)
        {
            var sortedList = VoznikiVEkipi.OrderBy(v => v.LetoOd).ToList();
            UpdateVoznikiVEkipiCollection(sortedList);
        }

        private void OnSortByYearToClicked(object sender, EventArgs e)
        {
            var sortedList = VoznikiVEkipi.OrderBy(v => v.LetoDo).ToList();
            UpdateVoznikiVEkipiCollection(sortedList);
        }

        private void OnSortByWinsClicked(object sender, EventArgs e)
        {
            var sortedList = VoznikiVEkipi.OrderByDescending(v => v.SteviloZmag).ToList();
            UpdateVoznikiVEkipiCollection(sortedList);
        }

        private void UpdateVoznikiVEkipiCollection(IEnumerable<VoznikVEkipi> sortedList)
        {
            VoznikiVEkipi.Clear();
            foreach (var voznikVEkipi in sortedList)
            {
                VoznikiVEkipi.Add(voznikVEkipi);
            }
        }
    }
}
