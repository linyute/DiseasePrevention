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
    public class NewsListPageViewModel : MainListPageViewModel
    {
        public NewsListPageViewModel(
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
            try
            {
                if (CrossConnectivity.Current.IsConnected)
                {
                    this._newsList = await this._newsService.GetRssReedsAsync(listType);

                    foreach (var feed in _newsList)
                    {
                        this.MainListViewModel.ItemsSource.Add(new MainListItem()
                        {
                            Id = feed.Guid,
                            Title = feed.Title,
                            PublicationDate = feed.PublicationDate.ToString("yyyy-MM-dd HH:mm:ss")
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
            var item = this._newsList.First(x => x.Guid == this.MainListViewModel.SelectedItem.Id);

            var title = "新聞稿內容";

            var ps = new NavigationParameters { { "SelectedItem", item } };

            await NavigationService.NavigateAsync(new Uri($"NewsDetailPage?Title={title}", UriKind.Relative), ps);

            this.MainListViewModel.SelectedItem = null;
        }
        
    }
}
