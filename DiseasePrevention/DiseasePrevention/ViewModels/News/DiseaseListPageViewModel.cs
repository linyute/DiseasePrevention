using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using DiseasePrevention.Models;
using DiseasePrevention.Models.News;
using DiseasePrevention.Services.News;
using Plugin.Connectivity;
using Prism.Navigation;
using Prism.Services;

namespace DiseasePrevention.ViewModels.News
{
    public class DiseaseListPageViewModel : MainListPageViewModel
    {
        public DiseaseListPageViewModel(
            INavigationService navigationService,
            IPageDialogService dialogService,
            NewsService newsService
            ) : base(navigationService, dialogService)
        {
            this._newsService = newsService;
        }
        
        protected override async Task BuildList(string menuType, string listType)
        {
            this.MainListViewModel.ItemsSource.Clear();

            await this.DownloadListAsync(listType);
        }

        private readonly NewsService _newsService;

        private List<RssFeed> _newsList = new List<RssFeed>();

        private async Task DownloadListAsync(string listType)
        {
            Dictionary<string, string> items = null;

            await Task.Run(() =>
            {
                items = this._newsService.GetDiseases(listType);
            });

            foreach (var item in items)
            {
                this.MainListViewModel.ItemsSource.Add(new MainListItem()
                {
                    Id = item.Key,
                    Title = item.Value
                });
            }
        }
        
        protected override async void NaviDetailPageAsync()
        {
            try
            {
                if (CrossConnectivity.Current.IsConnected)
                {
                    var item = await this._newsService.GetDiseaseFeedAsync(this.MainListViewModel.SelectedItem.Id);

                    var title = "傳染病說明";

                    var ps = new NavigationParameters { { "SelectedItem", item } };

                    await NavigationService.NavigateAsync(new Uri($"NewsDetailPage?Title={title}", UriKind.Relative), ps);
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

            this.MainListViewModel.SelectedItem = null;
        }
        
    }
}
