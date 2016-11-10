using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using DiseasePrevention.Models;
using DiseasePrevention.Models.News;
using DiseasePrevention.Models.Travels;
using DiseasePrevention.Services.News;
using DiseasePrevention.Services.Travels;
using DiseasePrevention.ViewModels.UserControls;
using Plugin.Connectivity;
using Prism.Navigation;
using Prism.Services;

namespace DiseasePrevention.ViewModels
{
    public class MainListPageViewModel : BindableBase, INavigationAware
    {
        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public MainListPageViewModel(
            INavigationService navigationService,
            IPageDialogService dialogService,
            NewsService newsService,
            TravelService travelService
            )
        {
            this._navigationService = navigationService;
            this._dialogService = dialogService;

            this._newsService = newsService;
            this._travelService = travelService;

            this.MainListViewModel.ItemSelectedCommand =
                new DelegateCommand(NaviDetailPage, () => this.IsRunning == false);
        }

        private bool _isRunning = true;

        public bool IsRunning
        {
            get { return _isRunning; }
            set
            {
                SetProperty(ref _isRunning, value);
                this.MainListViewModel.IsRunning = value;
            }
        }

        #region Navigation

        private readonly INavigationService _navigationService;

        private readonly IPageDialogService _dialogService;

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            
        }

        public async void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("Title")) { this.Title = (string)parameters["Title"]; }

            if (parameters.ContainsKey("MenuType")) { this.MenuType = (string)parameters["MenuType"]; }

            if (parameters.ContainsKey("ListType")) { this.ListType = (string)parameters["ListType"]; }

            await this.BuildList();

            this.IsRunning = false;
        }

        #endregion


        #region Main List

        private string _menuType;

        public string MenuType
        {
            get { return _menuType; }
            set { SetProperty(ref _menuType, value); }
        }

        private string _listType;

        public string ListType
        {
            get { return _listType; }
            set { SetProperty(ref _listType, value); }
        }

        public MainListViewModel MainListViewModel { get; set; }
            = new MainListViewModel();

        private async Task BuildList()
        {
            this.MainListViewModel.ItemsSource.Clear();

            switch (this.MenuType)
            {
                case "最新消息":
                    await this.DownloadNewsListAsync();
                    break;
                case "傳染病介紹":
                    await this.GetDiseaseListAsync();
                    break;
                case "國際疫情":
                    await this.DownloadTravelListAsync();
                    break;
                default:
                    break;
            }
        }

        #endregion
        

        #region Command

        private async void NaviDetailPage()
        {
            switch (this.MenuType)
            {
                case "最新消息":
                    await this.NaviNewsDetailPage();
                    break;
                case "傳染病介紹":
                    await this.NaviDiseaseDetailPage();
                    break;
                case "國際疫情":
                    await this.NaviTravelDetailPage();
                    break;
                default:
                    break;
            }

            this.MainListViewModel.SelectedItem = null;
        }

        #endregion


        #region 最新消息

        private readonly NewsService _newsService;

        private List<RssFeed> _newsList = new List<RssFeed>();

        private async Task DownloadNewsListAsync()
        {
            try
            {
                if (CrossConnectivity.Current.IsConnected)
                {
                    this._newsList = await this._newsService.GetRssReedsAsync(this.ListType);

                    foreach (var feed in _newsList)
                    {
                        this.MainListViewModel.ItemsSource.Add(new MainListItem()
                        {
                            Id = feed.Guid,
                            Title = feed.Title,
                            PublicationDate = feed.PublicationDate
                        });
                    }
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

        private async Task NaviNewsDetailPage()
        {
            var item = this._newsList.First(x => x.Guid == this.MainListViewModel.SelectedItem.Id);

            var title = "新聞稿內容";

            var ps = new NavigationParameters { { "SelectedItem", item } };

            await _navigationService.NavigateAsync(new Uri($"NewsDetailPage?Title={title}", UriKind.Relative), ps);
        }

        #endregion

        #region 傳染病介紹

        private async Task GetDiseaseListAsync()
        {
            Dictionary<string, string> items = null;

            await Task.Run(() =>
            {
                items = this._newsService.GetDiseases(this.ListType);
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

        private async Task NaviDiseaseDetailPage()
        {
            try
            {
                if (CrossConnectivity.Current.IsConnected)
                {
                    var item = await this._newsService.GetDiseaseFeedAsync(this.MainListViewModel.SelectedItem.Id);

                    var title = "傳染病說明";

                    var ps = new NavigationParameters { { "SelectedItem", item } };

                    await _navigationService.NavigateAsync(new Uri($"NewsDetailPage?Title={title}", UriKind.Relative), ps);
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

        #region 國際疫情

        private readonly TravelService _travelService;

        private List<TravelAlert> _travelList = new List<TravelAlert>();

        private async Task DownloadTravelListAsync()
        {
            try
            {
                if (CrossConnectivity.Current.IsConnected)
                {
                    this._travelList = await this._travelService.GetTravelAlertsAsync(this.ListType);

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
                    await _dialogService.DisplayAlertAsync("無法連線", "請開啟網路", "OK");
                }
            }
            catch (Exception ex)
            {
                await _dialogService.DisplayAlertAsync("發生錯誤", ex.Message, "OK");
            }
        }

        private async Task NaviTravelDetailPage()
        {
            var item = this._travelList.First(x => x.Id == this.MainListViewModel.SelectedItem.Id);

            var ps = new NavigationParameters { { "SelectedItem", item } };

            await _navigationService.NavigateAsync(new Uri("TravelDetailPage", UriKind.Relative), ps);
        }

        #endregion
    }
}
