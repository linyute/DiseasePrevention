using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using DiseasePrevention.Models;
using DiseasePrevention.Models.Serums;
using DiseasePrevention.Services;
using DiseasePrevention.Services.Serums;
using Microsoft.Practices.ObjectBuilder2;
using Plugin.Connectivity;
using Prism.Navigation;
using Prism.Services;

namespace DiseasePrevention.ViewModels.Serums
{
    public class SerumHospitalListPageViewModel : MainListPageViewModel
    {
        public SerumHospitalListPageViewModel(
            INavigationService navigationService, 
            IPageDialogService dialogService, 
            SerumService serumService)
            : base(navigationService, dialogService)
        {
            this._serumService = serumService;
        }

        private readonly SerumService _serumService;

        protected override async Task BuildList(string menuType, string listType)
        {
            this.MainListViewModel.ItemsSource.Clear();

            switch (menuType)
            {
                case "血清儲備點縣市":
                    await this.DownloadToGlobalDataAsync();
                    await this.GlobalDataToListAsync();
                    break;
                case "血清儲備點查詢":
                    await this.GetSerumHospitalsAsync(listType);
                    break;
                default:
                    break;
            }
        }

        protected override async void NaviDetailPageAsync()
        {
            switch (this.MenuType)
            {
                case "血清儲備點縣市":
                    await this.NaviSerumHospitalListPageAsync();
                    break;
                case "血清儲備點查詢":
                    await this.NaviSerumHospitalDetailPageAsync();
                    break;
                default:
                    break;
            }

            this.MainListViewModel.SelectedItem = null;
        }

        #region 血清儲備點縣市

        public async Task DownloadToGlobalDataAsync()
        {
            try
            {
                if (CrossConnectivity.Current.IsConnected)
                {
                    var items = await this._serumService.GetSerumHospitalsAsync();

                    GlobalData.SerumHospitals.Clear();
                    GlobalData.SerumHospitals.AddRange(items);
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

        private async Task GlobalDataToListAsync()
        {
            List<string> items = null;

            await Task.Run(() =>
            {
                items = GlobalData.SerumHospitals
                            .Select(h => h.縣市別).Distinct()
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

        private async Task NaviSerumHospitalListPageAsync()
        {
            await NavigationService.NavigateAsync(
                new Uri($"SerumHospitalListPage?Title=血清儲備點&MenuType=血清儲備點查詢&ListType={this.MainListViewModel.SelectedItem.Title}", UriKind.Relative));

            this.MainListViewModel.SelectedItem = null;
        }

        #endregion

        #region 血清儲備點查詢

        private List<HospitalSerums> _hospitalList = new List<HospitalSerums>();

        private async Task GetSerumHospitalsAsync(string city)
        {
            await Task.Run(() =>
            {
                _hospitalList = GlobalData.SerumHospitals.Where(s => s.縣市別 == city)
                            .GroupBy(i => new { i.區域別, i.縣市別, i.醫療院所名稱, i.聯絡電話 })
                            .Select(g => new HospitalSerums()
                            {
                                區域別 = g.Key.區域別,
                                縣市別 = g.Key.縣市別,
                                醫療院所名稱 = g.Key.醫療院所名稱,
                                聯絡電話 = g.Key.聯絡電話,
                                抗蛇毒血清種類 = g.Select(i => i.抗蛇毒血清種類).JoinStrings(Environment.NewLine)
                            })
                            .OrderBy(h => h.醫療院所名稱)
                            .ToList();

                _hospitalList.ForEach(x =>
                {
                    x.Id = x.GetHashCode().ToString();
                });
            });

            foreach (var item in _hospitalList)
            {
                this.MainListViewModel.ItemsSource.Add(new MainListItem()
                {
                    Id = item.Id,
                    Title = item.醫療院所名稱
                });
            }
        }

        private async Task NaviSerumHospitalDetailPageAsync()
        {
            var item = this._hospitalList.First(x => x.Id == this.MainListViewModel.SelectedItem.Id);

            var ps = new NavigationParameters { { "SelectedItem", item } };

            await NavigationService.NavigateAsync(new Uri($"SerumHospitalDetailPage", UriKind.Relative), ps);

            this.MainListViewModel.SelectedItem = null;
        }

        #endregion
    }
}
