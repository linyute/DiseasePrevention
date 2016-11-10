using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using DiseasePrevention.Models.Vaccines;
using DiseasePrevention.Services.Vaccines;
using Prism.Navigation;

namespace DiseasePrevention.ViewModels.Vaccines
{
    public class AdultVaccinePageViewModel : BindableBase, INavigationAware
    {
        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public AdultVaccinePageViewModel(
            INavigationService navigationService,
            VaccineService vaccineService)
        {
            this._navigationService = navigationService;
            this._vaccineService = vaccineService;
        }

        private bool _isRunning = true;

        public bool IsRunning
        {
            get { return _isRunning; }
            set
            {
                SetProperty(ref _isRunning, value);
            }
        }

        #region Navigation

        private readonly INavigationService _navigationService;

        public void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public async void OnNavigatedTo(NavigationParameters parameters)
        {
            //if (parameters.ContainsKey("Title")) { this.Title = (string)parameters["Title"]; }

            await this.BuildList();

            this.IsRunning = false;
        }

        #endregion

        #region List

        private ObservableCollection<AdultVaccine> _itemsSource = new ObservableCollection<AdultVaccine>();
        public ObservableCollection<AdultVaccine> ItemsSource
        {
            get { return _itemsSource; }
            set { SetProperty(ref _itemsSource, value); }
        }

        private readonly VaccineService _vaccineService;

        public async Task BuildList()
        {
            List<AdultVaccine> items = null;

            await Task.Run(() =>
            {
                items = this._vaccineService.GetAdultVaccines().OrderBy(x => x.Age).ToList();
            });

            foreach (var item in items)
            {
                this.ItemsSource.Add(item);
            }
        }

        #endregion
    }
}
