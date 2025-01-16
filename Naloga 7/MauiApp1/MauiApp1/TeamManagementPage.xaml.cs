using System.Collections.ObjectModel;
using MauiApp1.Models;
using MauiApp1.Services;
using Microsoft.Maui.Controls;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;

namespace MauiApp1
{
    public partial class TeamManagementPage : ContentPage, INotifyPropertyChanged
    {
        private readonly EkipaService _ekipaService;
        public ObservableCollection<Ekipa> Ekipe { get; set; }
        private Ekipa _selectedEkipa;

        public Ekipa SelectedEkipa
        {
            get => _selectedEkipa;
            set
            {
                if (_selectedEkipa != value)
                {
                    _selectedEkipa = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(IsEkipaSelected));
                    OnSelectedEkipaChanged();
                }
            }
        }

        public bool IsEkipaSelected => SelectedEkipa != null;

        public TeamManagementPage(EkipaService ekipaService)
        {
            InitializeComponent();
            _ekipaService = ekipaService;
            Ekipe = new ObservableCollection<Ekipa>();
            BindingContext = this;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadEkipeAsync();
        }

        private async Task LoadEkipeAsync()
        {
            var ekipe = await _ekipaService.GetEkipeAsync();
            Ekipe.Clear();
            foreach (var ekipa in ekipe)
            {
                Ekipe.Add(ekipa);
            }
        }

        private void OnSelectedEkipaChanged()
        {
            if (SelectedEkipa != null)
            {
                NazivEntry.Text = SelectedEkipa.Naziv;
                DrzavaEntry.Text = SelectedEkipa.Drzava;
                LetoUstanovitveEntry.Text = SelectedEkipa.LetoUstanovitve.ToString();
            }
            else
            {
                ClearInputs(false);
            }
        }

        private async void OnAddEkipaClicked(object sender, System.EventArgs e)
        {
            var ekipa = new Ekipa
            {
                Naziv = NazivEntry.Text,
                Drzava = DrzavaEntry.Text,
                LetoUstanovitve = int.Parse(LetoUstanovitveEntry.Text)
            };

            var addedEkipa = await AddEkipa(ekipa);
            if (addedEkipa != null)
            {
                Ekipe.Add(addedEkipa);
                ClearInputs(true);
            }
        }

        private async void OnUpdateEkipaClicked(object sender, System.EventArgs e)
        {
            if (SelectedEkipa == null || SelectedEkipa.Id == 0)
            {
                await DisplayAlert("Napaka", "Izberite veljavno ekipo za posodobitev.", "OK");
                return;
            }

            SelectedEkipa.Naziv = NazivEntry.Text;
            SelectedEkipa.Drzava = DrzavaEntry.Text;
            SelectedEkipa.LetoUstanovitve = int.Parse(LetoUstanovitveEntry.Text);

            bool uspeh = await UpdateEkipa(SelectedEkipa);
            if (uspeh)
            {
                await DisplayAlert("Uspeh", "Ekipa je bila posodobljena.", "OK");
                await LoadEkipeAsync();  
            }
            else
            {
                await DisplayAlert("Napaka", "Posodobitev ekipe ni uspela.", "OK");
            }
        }

        private async void OnDeleteEkipaClicked(object sender, System.EventArgs e)
        {
            if (SelectedEkipa == null || SelectedEkipa.Id == 0)
            {
                await DisplayAlert("Napaka", "Izberite veljavno ekipo za izbris.", "OK");
                return;
            }

            bool uspeh = await DeleteEkipa(SelectedEkipa.Id);
            if (uspeh)
            {
                await DisplayAlert("Uspeh", "Ekipa je bila izbrisana.", "OK");
                await LoadEkipeAsync();  
            }
            else
            {
                await DisplayAlert("Napaka", "Izbris ekipe ni uspel.", "OK");
            }
        }

        public async Task<Ekipa> AddEkipa(Ekipa ekipa)
        {
            var createdEkipa = await _ekipaService.AddEkipaAsync(ekipa);
            if (createdEkipa != null)
            {
                return createdEkipa;
            }
            else
            {
                await DisplayAlert("Napaka", "Dodajanje ekipe ni uspelo.", "OK");
                return null;
            }
        }

        public async Task<bool> UpdateEkipa(Ekipa ekipa)
        {
            var response = await _ekipaService.UpdateEkipaAsync(ekipa.Id, ekipa);

            if (response)
            {
                var existingEkipa = Ekipe.FirstOrDefault(v => v.Id == ekipa.Id);
                if (existingEkipa != null)
                {
                    var index = Ekipe.IndexOf(existingEkipa);
                    Ekipe[index] = ekipa;
                    ClearInputs(true);
                }
                return true;
            }
            else
            {
                await DisplayAlert("Napaka", "Posodobitev ekipe ni uspela. Preverite povezavo ali API.", "OK");
            }

            return false;
        }

        public async Task<bool> DeleteEkipa(int id)
        {
            if (await _ekipaService.DeleteEkipaAsync(id))
            {
                var ekipaToRemove = Ekipe.FirstOrDefault(v => v.Id == id);
                if (ekipaToRemove != null)
                {
                    Ekipe.Remove(ekipaToRemove);
                    ClearInputs(true);
                }
                return true;
            }
            return false;
        }

        private void ClearInputs(bool resetSelection)
        {
            NazivEntry.Text = string.Empty;
            DrzavaEntry.Text = string.Empty;
            LetoUstanovitveEntry.Text = string.Empty;

            if (resetSelection)
            {
                SelectedEkipa = null;
            }
        }

        private async void OnBackButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }


        private void OnSortByNameClicked(object sender, EventArgs e)
        {
            var sortedList = Ekipe.OrderBy(e => e.Naziv).ToList();
            UpdateEkipeCollection(sortedList);
        }

        private void OnSortByCountryClicked(object sender, EventArgs e)
        {
            var sortedList = Ekipe.OrderBy(e => e.Drzava).ToList();
            UpdateEkipeCollection(sortedList);
        }

        private void OnSortByYearClicked(object sender, EventArgs e)
        {
            var sortedList = Ekipe.OrderBy(e => e.LetoUstanovitve).ToList();
            UpdateEkipeCollection(sortedList);
        }

        private void UpdateEkipeCollection(IEnumerable<Ekipa> sortedList)
        {
            Ekipe.Clear();
            foreach (var ekipa in sortedList)
            {
                Ekipe.Add(ekipa);
            }
        }
    }
}
