using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using DiseasePrevention.Models;
using DiseasePrevention.Models.Vaccines;
using DiseasePrevention.Services;
using DiseasePrevention.Services.Vaccines;
using Plugin.Connectivity;
using Prism.Navigation;
using Prism.Services;

namespace DiseasePrevention.ViewModels.Vaccines
{
    public class VaccineHospitalListPageViewModel : MainListPageViewModel
    {
        public VaccineHospitalListPageViewModel(
            INavigationService navigationService,
            IPageDialogService dialogService,
            VaccineService vaccineService
            ) : base(navigationService, dialogService)
        {
            this._vaccineService = vaccineService;
        }
        
        protected override async Task BuildList(string menuType, string listType)
        {
            this.MainListViewModel.ItemsSource.Clear();

            switch (menuType)
            {
                case "預防接種單位縣市":
                    await this.GetVaccineHospitalsAsync();
                    await this.GetVaccineHospitalCitiesAsync();
                    break;
                case "預防接種單位行政區":
                    await this.GetVaccineHospitalDistrictsAsync(listType);
                    break;
                case "預防接種單位查詢":
                    await this.GetVaccineHospitalsAsync(listType);
                    break;
                default:
                    break;
            }
        }

        protected override async void NaviDetailPageAsync()
        {
            switch (this.MenuType)
            {
                case "預防接種單位縣市":
                    await this.NaviVaccineHospitalDistrictsPageAsync();
                    break;
                case "預防接種單位行政區":
                    await this.NaviVaccineHospitalsPageAsync();
                    break;
                case "預防接種單位查詢":
                    await this.NaviVaccineHospitalDetailPageAsync();
                    break;
                default:
                    break;
            }

            this.MainListViewModel.SelectedItem = null;
        }

        #region 預防接種

        private readonly VaccineService _vaccineService;

        #region 預防接種單位縣市

        public async Task GetVaccineHospitalsAsync()
        {
            try
            {
                if (CrossConnectivity.Current.IsConnected)
                {
                    var items = await this._vaccineService.GetVaccineHospitalsAsync();

                    GlobalData.VaccineHospitals.Clear();
                    GlobalData.VaccineHospitals.AddRange(items);
                }
                else
                {
                    await DialogService.DisplayAlertAsync("無法連線", "請開啟網路", "OK");
                }
            }
            catch (Exception ex)
            {
                await DialogService.DisplayAlertAsync("發生錯誤", ex.Message, "OK");
            }
        }

        private async Task GetVaccineHospitalCitiesAsync()
        {
            List<string> items = null;

            await Task.Run(() =>
            {
                items = GlobalData.VaccineHospitals
                            .Select(h => h.縣市).Distinct()
                            .OrderBy(c => c).ToList();
            });

            foreach (var item in items)
            {
                this.MainListViewModel.ItemsSource.Add(new MainListItem()
                {
                    Id = item,
                    Title = item
                });
            }
        }

        private async Task NaviVaccineHospitalDistrictsPageAsync()
        {
            var url = $"VaccineHospitalListPage?Title=預防接種單位行政區&MenuType=預防接種單位行政區&ListType={this.MainListViewModel.SelectedItem.Id}";

            await NavigationService.NavigateAsync(new Uri(url, UriKind.Relative));
        }

        #endregion

        #region 預防接種單位行政區

        private async Task GetVaccineHospitalDistrictsAsync(string city)
        {
            List<string> items = null;

            await Task.Run(() =>
            {
                items = GlobalData.VaccineHospitals
                            .Where(h => h.縣市 == city)
                            .Select(h => h.鄉鎮市區).Distinct()
                            .OrderBy(d => d).ToList();
            });

            foreach (var item in items)
            {
                this.MainListViewModel.ItemsSource.Add(new MainListItem()
                {
                    Id = item,
                    Title = item
                });
            }
        }

        private async Task NaviVaccineHospitalsPageAsync()
        {
            var cityAndDistrict = $"{this.ListType},{this.MainListViewModel.SelectedItem.Id}";

            var url = $"VaccineHospitalListPage?Title=預防接種單位查詢&MenuType=預防接種單位查詢&ListType={cityAndDistrict}";

            await NavigationService.NavigateAsync(new Uri(url, UriKind.Relative));
        }

        #endregion

        #region 預防接種單位查詢

        private async Task GetVaccineHospitalsAsync(string cityAndDistrict)
        {
            var temp = cityAndDistrict.Split(',');

            var city = temp[0];
            var district = temp[1];

            List<VaccineHospital> items = null;

            await Task.Run(() =>
            {
                items = GlobalData.VaccineHospitals
                            .Where(h => h.縣市 == city && h.鄉鎮市區 == district)
                            .OrderBy(h => h.合約醫療院所名稱)
                            .ToList();
            });

            foreach (var item in items)
            {
                this.MainListViewModel.ItemsSource.Add(new MainListItem()
                {
                    Id = item.Id,
                    Title = item.合約醫療院所名稱
                });
            }
        }

        private async Task NaviVaccineHospitalDetailPageAsync()
        {
            var item = GlobalData.VaccineHospitals.First(x => x.Id == this.MainListViewModel.SelectedItem.Id);

            var ps = new NavigationParameters { { "SelectedItem", item } };

            await NavigationService.NavigateAsync(new Uri("VaccineHospitalDetailPage", UriKind.Relative), ps);
        }

        #endregion

        #endregion

    }
}
