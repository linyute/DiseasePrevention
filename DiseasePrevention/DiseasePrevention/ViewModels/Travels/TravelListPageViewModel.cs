using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using DiseasePrevention.Models;
using DiseasePrevention.Models.Travels;
using DiseasePrevention.Services.Travels;
using Plugin.Connectivity;
using Prism.Navigation;
using Prism.Services;

namespace DiseasePrevention.ViewModels.Travels
{
    public class TravelListPageViewModel : MainListPageViewModel
    {
        public TravelListPageViewModel(
            INavigationService navigationService,
            IPageDialogService dialogService,
            TravelService travelService
            ) : base(navigationService, dialogService)
        {
            this._travelService = travelService;
        }
        
        protected override async Task BuildList(string menuType, string listType)
        {
            this.MainListViewModel.ItemsSource.Clear();

            await this.DownloadListAsync(listType);
        }

        private readonly TravelService _travelService;

        private List<TravelAlert> _travelList = new List<TravelAlert>();

        private async Task DownloadListAsync(string listType)
        {
            try
            {
                if (CrossConnectivity.Current.IsConnected)
                {
                    this._travelList = await this._travelService.GetTravelAlertsAsync(listType);

                    foreach (var item in _travelList)
                    {
                        this.MainListViewModel.ItemsSource.Add(new MainListItem()
                        {
                            Id = item.Id,
                            Title = item.Headline,
                            PublicationDate = item.Sent
                        });
                    }
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
        
        protected override async void NaviDetailPageAsync()
        {
            var item = this._travelList.First(x => x.Id == this.MainListViewModel.SelectedItem.Id);

            var ps = new NavigationParameters { { "SelectedItem", item } };

            await NavigationService.NavigateAsync(new Uri("TravelDetailPage", UriKind.Relative), ps);

            this.MainListViewModel.SelectedItem = null;
        }
        
    }
}
