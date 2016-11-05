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
                this.MainListViewModel.ItemSelectedCommand.RaiseCanExecuteChanged();
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
            if (parameters.ContainsKey("Title")) { this.Title = (string)parameters["Title"]; }

            if (parameters.ContainsKey("NewsType"))
            {
                this.NewsType = (string)parameters["NewsType"];
                await this.DownloadNewsListAsync();
            }

            if (parameters.ContainsKey("DiseaseType"))
            {
                this.DiseaseType = (string)parameters["DiseaseType"];
                this.GetDiseaseList();
            }

            this.IsRunning = false;
        }

        #endregion

        public MainListViewModel MainListViewModel { get; set; }
            = new MainListViewModel();

        private readonly NewsService _newsService;

        #region News List

        private string _newsType;

        public string NewsType
        {
            get { return _newsType; }
            set
            {
                SetProperty(ref _newsType, value);
                //this._newsService.NewsType = _newsType;
            }
        }
        
        private List<RssFeed> _newsList = new List<RssFeed>();

        public async Task DownloadNewsListAsync()
        {
            try
            {
                if (CrossConnectivity.Current.IsConnected)
                {
                    this._newsList = await this._newsService.GetRssReedsAsync(this.NewsType);

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
            }
            catch (Exception ex)
            {
                await _dialogService.DisplayAlertAsync("發生錯誤", ex.Message, "OK");
            }
        }

        #endregion

        #region Disease List

        private string _diseaseType;

        public string DiseaseType
        {
            get { return _diseaseType; }
            set
            {
                SetProperty(ref _diseaseType, value);
                //this._newsService.DiseaseType = _diseaseType;
            }
        }
        
        public void GetDiseaseList()
        {
            var items = this._newsService.GetDiseaseList(this.DiseaseType);

            foreach (var item in items)
            {
                this.MainListViewModel.ItemsSource.Add(new MainListItem()
                {
                    Id = item.Key,
                    Title = item.Value
                });
            }
        }

        #endregion

        #region Command

        private async void NaviDetailPage()
        {
            RssFeed item = null;
            var title = string.Empty;

            if (string.IsNullOrEmpty(this.NewsType) == false)
            {
                item = this._newsList.First(x => x.Guid == this.MainListViewModel.SelectedItem.Id);
                title = "新聞稿內容";
            }

            if (string.IsNullOrEmpty(this.DiseaseType) == false)
            {
                item = await this._newsService.GetDiseaseFeedAsync(this.MainListViewModel.SelectedItem.Id);
                title = "傳染病說明";
            }

            var ps = new NavigationParameters {{"SelectedItem", item }};

            await _navigationService.NavigateAsync(new Uri($"NewsDetailPage?Title={title}", UriKind.Relative), ps);
        }

        #endregion
    }
}
