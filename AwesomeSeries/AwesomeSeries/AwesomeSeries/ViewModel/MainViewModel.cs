using AwesomeSeries.Models;
using AwesomeSeries.Services;
using AwesomeSeries.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AwesomeSeries.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        readonly ISerieService _serieService;

        private ICommand _searchCommand;
        public ICommand ItemClickCommand { get; }
        public ObservableCollection<Serie> Items { get; }

        public MainViewModel(ISerieService serieService) : base("Awesome Series")
        {
            _serieService = serieService;
            Items = new ObservableCollection<Serie>();
            ItemClickCommand = new Command<Serie>(async (item)
                => await ItemClickCommandExecute(item));
        }

        public ICommand SearchCommand
        {
            get
            {
                return _searchCommand ?? (_searchCommand = new Command<string>(async (text) =>
                {
                    await LoadDataAsync(text);
                }));
            }
        }

        async Task ItemClickCommandExecute(Serie serie)
        {
            if (serie != null)
                await NavigationService.NavigateToAsync<DetailViewModel>(serie);
        }

        public override async Task InitializeAsync(object navgationData)
        {
            await base.InitializeAsync(navgationData);

            await LoadDataAsync("");
        }

        async Task LoadDataAsync(string search)
        {
            var result = await _serieService.GetSeriesAsync();
            AddItens(result, search);
        }

        private void AddItens(SerieResponse result, string search)
        {
            Items.Clear();
            if (search == "")
            {
                result?.Series.ToList()?.ForEach(i => Items.Add(i));
            }
            else
            {
                var filtered = result.Series.ToList();
                foreach (Serie s in filtered.Where(s => 
                    s.Name.ToLower().Contains(search.ToLower()))) { Items.Add(s); }
            }
            
        }
    }
}
