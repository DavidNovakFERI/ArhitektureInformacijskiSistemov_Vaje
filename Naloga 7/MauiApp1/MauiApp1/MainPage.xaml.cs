using System.Collections.ObjectModel;
using MauiApp1.Models;
using MauiApp1.Services;
using Microsoft.Maui.Controls;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;

namespace MauiApp1
{
    public partial class MainPage : ContentPage, INotifyPropertyChanged
    {
        private readonly VoznikService _voznikService;
        private readonly EkipaService _ekipaService;
        private readonly VoznikVEkipiService _voznikVEkipiService;

        public ObservableCollection<Voznik> Vozniki { get; set; }
        private Voznik _selectedVoznik;

        public Voznik SelectedVoznik
        {
            get => _selectedVoznik;
            set
            {
                if (_selectedVoznik != value)
                {
                    _selectedVoznik = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(IsVoznikSelected));
                    OnSelectedVoznikChanged();
                }
            }
        }

        public bool IsVoznikSelected => SelectedVoznik != null;

        public MainPage(VoznikService voznikService, EkipaService ekipaService, VoznikVEkipiService voznikVEkipiService)
        {
            InitializeComponent();
            _voznikService = voznikService;
            _ekipaService = ekipaService;
            _voznikVEkipiService = voznikVEkipiService;
            Vozniki = new ObservableCollection<Voznik>();
            BindingContext = this;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadVoznikiAsync();
        }

        private async Task LoadVoznikiAsync()
        {
            var vozniki = await _voznikService.GetVoznikiAsync();
            Vozniki.Clear();
            foreach (var voznik in vozniki)
            {
                Vozniki.Add(voznik);
            }
        }

        private void OnSelectedVoznikChanged()
        {
            if (SelectedVoznik != null)
            {
                ImeEntry.Text = SelectedVoznik.Ime;
                PriimekEntry.Text = SelectedVoznik.Priimek;
                LetoRojstvaEntry.Text = SelectedVoznik.LetoRojstva.ToString();
            }
            else
            {
                ClearInputs(false);
            }
        }

        private async void OnAddVoznikClicked(object sender, System.EventArgs e)
        {
            var voznik = new Voznik
            {
                Ime = ImeEntry.Text,
                Priimek = PriimekEntry.Text,
                LetoRojstva = int.Parse(LetoRojstvaEntry.Text)
            };

            var addedVoznik = await AddVoznik(voznik);
            if (addedVoznik != null)
            {
                Vozniki.Add(addedVoznik);
                ClearInputs(true);
            }
        }

        private async void OnUpdateVoznikClicked(object sender, System.EventArgs e)
        {
            if (SelectedVoznik == null || SelectedVoznik.Id == 0)
            {
                await DisplayAlert("Napaka", "Izberite veljavnega voznika za posodobitev.", "OK");
                return;
            }

            SelectedVoznik.Ime = ImeEntry.Text;
            SelectedVoznik.Priimek = PriimekEntry.Text;
            SelectedVoznik.LetoRojstva = int.Parse(LetoRojstvaEntry.Text);

            bool uspeh = await UpdateVoznik(SelectedVoznik);
            if (uspeh)
            {
                await DisplayAlert("Uspeh", "Voznik je bil posodobljen.", "OK");
                await LoadVoznikiAsync();  // Refresh the list after update
            }
            else
            {
                await DisplayAlert("Napaka", "Posodobitev voznika ni uspela.", "OK");
            }
        }

        private async void OnDeleteVoznikClicked(object sender, System.EventArgs e)
        {
            if (SelectedVoznik == null || SelectedVoznik.Id == 0)
            {
                await DisplayAlert("Napaka", "Izberite veljavnega voznika za izbris.", "OK");
                return;
            }

            bool uspeh = await DeleteVoznik(SelectedVoznik.Id);
            if (uspeh)
            {
                await DisplayAlert("Uspeh", "Voznik je bil izbrisan.", "OK");
                await LoadVoznikiAsync();  // Refresh the list after delete
            }
            else
            {
                await DisplayAlert("Napaka", "Izbris voznika ni uspel.", "OK");
            }
        }

        public async Task<Voznik> AddVoznik(Voznik voznik)
        {
            var createdVoznik = await _voznikService.AddVoznikAsync(voznik);
            if (createdVoznik != null)
            {
                return createdVoznik;
            }
            else
            {
                await DisplayAlert("Napaka", "Dodajanje voznika ni uspelo.", "OK");
                return null;
            }
        }

        public async Task<bool> UpdateVoznik(Voznik voznik)
        {
            var response = await _voznikService.UpdateVoznikAsync(voznik.Id, voznik);

            if (response)
            {
                var existingVoznik = Vozniki.FirstOrDefault(v => v.Id == voznik.Id);
                if (existingVoznik != null)
                {
                    var index = Vozniki.IndexOf(existingVoznik);
                    Vozniki[index] = voznik;
                    ClearInputs(true);
                }
                return true;
            }
            else
            {
                await DisplayAlert("Napaka", "Posodobitev voznika ni uspela. Preverite povezavo ali API.", "OK");
            }

            return false;
        }

        public async Task<bool> DeleteVoznik(int id)
        {
            if (await _voznikService.DeleteVoznikAsync(id))
            {
                var voznikToRemove = Vozniki.FirstOrDefault(v => v.Id == id);
                if (voznikToRemove != null)
                {
                    Vozniki.Remove(voznikToRemove);
                    ClearInputs(true);
                }
                return true;
            }
            return false;
        }

        private void ClearInputs(bool resetSelection)
        {
            ImeEntry.Text = string.Empty;
            PriimekEntry.Text = string.Empty;
            LetoRojstvaEntry.Text = string.Empty;

            if (resetSelection)
            {
                SelectedVoznik = null;
            }
        }

        private async void OnManageTeamsClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TeamManagementPage(_ekipaService));
        }

        private async void OnManageVoznikiVEkipiClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new VoznikVEkipiManagementPage(_voznikVEkipiService, _voznikService, _ekipaService));
        }

        private void OnSortByNameClicked(object sender, EventArgs e)
        {
            var sortedList = Vozniki.OrderBy(v => v.Ime).ToList();
            UpdateVoznikiCollection(sortedList);
        }

        private void OnSortBySurnameClicked(object sender, EventArgs e)
        {
            var sortedList = Vozniki.OrderBy(v => v.Priimek).ToList();
            UpdateVoznikiCollection(sortedList);
        }

        private void OnSortByYearClicked(object sender, EventArgs e)
        {
            var sortedList = Vozniki.OrderBy(v => v.LetoRojstva).ToList();
            UpdateVoznikiCollection(sortedList);
        }

        private void UpdateVoznikiCollection(IEnumerable<Voznik> sortedList)
        {
            Vozniki.Clear();
            foreach (var voznik in sortedList)
            {
                Vozniki.Add(voznik);
            }
        }
    }
}
