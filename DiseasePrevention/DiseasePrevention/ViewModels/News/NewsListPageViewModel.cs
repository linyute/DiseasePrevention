using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using DiseasePrevention.Models;
using DiseasePrevention.Models.News;
using DiseasePrevention.Services.News;
using DiseasePrevention.ViewModels.UserControls;
using Plugin.Connectivity;
using Prism.Navigation;
using Prism.Services;

namespace DiseasePrevention.ViewModels.News
{
    public class NewsListPageViewModel : BindableBase, INavigationAware
    {
        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public NewsListPageViewModel(
            INavigationService navigationService,
            IPageDialogService dialogService,
            NewsService newsService
            )
        {
            this._navigationService = navigationService;
            this._dialogService = dialogService;
            this._newsService = newsService;

            this.NewsListViewModel.ItemSelectedCommand = 
                new DelegateCommand(NaviDetailPage, () => this.IsRunning == false);
        }

        private bool _isRunning = true;

        public bool IsRunning
        {
            get { return _isRunning; }
            set
            {
                SetProperty(ref _isRunning, value);
                this.NewsListViewModel.ItemSelectedCommand.RaiseCanExecuteChanged();
            }
        }

        #region Navigation

        private readonly INavigationService _navigationService;

        private readonly IPageDialogService _dialogService;

        public async void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public async void OnNavigatedTo(NavigationParameters parameters)
        {
            //if (parameters.ContainsKey("Title")) { this.Title = (string)parameters["Title"]; }

            if (parameters.ContainsKey("NewsType"))
            {
                var newsType = (string) parameters["NewsType"];
                this.NewsType = newsType;
                this._newsService.NewsType = newsType;
            }

            await this.DownloadListAsync();

            this.IsRunning = false;
        }

        #endregion

        #region ListView

        private readonly NewsService _newsService;
        
        private string _newsType;

        public string NewsType
        {
            get { return _newsType; }
            set
            {
                SetProperty(ref _newsType, value);
                this._newsService.NewsType = _newsType;
            }
        }

        public NewsListViewModel NewsListViewModel { get; set; }
            = new NewsListViewModel();

        private List<RssFeed> _itemList = new List<RssFeed>();

        public async Task DownloadListAsync()
        {
            try
            {
                if (CrossConnectivity.Current.IsConnected)
                {
                    this._itemList = await this._newsService.GetRssReedsAsync();

                    foreach (var feed in _itemList)
                    {
                        this.NewsListViewModel.ItemsSource.Add(new NewsListItem()
                        {
                            Id = feed.Guid,
                            Title = feed.Title,
                            PublicationDate = feed.PublicationDate
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                await _dialogService.DisplayAlertAsync("發生錯誤", ex.Message, "OK");
            }
        }

        #endregion

        #region Command

        private async void NaviDetailPage()
        {
            var item = this._itemList.First(x => x.Guid == this.NewsListViewModel.SelectedItem.Id);

            var ps = new NavigationParameters {{"SelectedItem", item }};

            await _navigationService.NavigateAsync(new Uri("NewsDetailPage", UriKind.Relative), ps);
        }

        #endregion
    }
}
