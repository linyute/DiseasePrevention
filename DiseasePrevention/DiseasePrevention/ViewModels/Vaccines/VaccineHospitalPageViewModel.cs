using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using DiseasePrevention.Models.Vaccines;
using DiseasePrevention.Services;
using DiseasePrevention.Services.Vaccines;
using Plugin.Connectivity;
using Prism.Navigation;
using Prism.Services;

namespace DiseasePrevention.ViewModels.Vaccines
{
    public class VaccineHospitalPageViewModel : BindableBase, INavigationAware
    {
        private string _title;

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public VaccineHospitalPageViewModel(
            INavigationService navigationService,
            IPageDialogService dialogService,
            VaccineService vaccineService)
        {
            this._navigationService = navigationService;
            this._dialogService = dialogService;
            this._vaccineService = vaccineService;
        }

        private bool _isRunning = true;

        public bool IsRunning
        {
            get { return _isRunning; }
            set { SetProperty(ref _isRunning, value); }
        }

        #region Navigation

        private readonly INavigationService _navigationService;

        private readonly IPageDialogService _dialogService;

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            
        }

        public async void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("GoBack"))
            {
                await this._navigationService.GoBackAsync();
            }

            if (parameters.ContainsKey("Title"))
            {
                this.Title = (string) parameters["Title"];
            }

            await this.DownloadDataAsync();

            this.IsRunning = false;
        }

        #endregion

        #region List

        private readonly VaccineService _vaccineService;

        public async Task DownloadDataAsync()
        {
            try
            {
                if (CrossConnectivity.Current.IsConnected)
                {
                    var items = await this._vaccineService.GetVaccineHospitalsAsync();

                    GlobalData.VaccineHospitals.Clear();
                    GlobalData.VaccineHospitals.AddRange(items);

                    var ps = new NavigationParameters { { "SelectedItem", "" } };

                    await _navigationService.NavigateAsync(
                        new Uri($"MainListPage?Title=選擇縣市&MenuType=預防接種單位縣市", UriKind.Relative), ps);
                }
                else
                {
                    await _dialogService.DisplayAlertAsync("無法連線", "請開啟網路", "OK");
                }
            }
            catch (Exception ex)
            {
                await _dialogService.DisplayAlertAsync("發生錯誤", ex.Message, "OK");
            }
        }

        #endregion
    }
}
