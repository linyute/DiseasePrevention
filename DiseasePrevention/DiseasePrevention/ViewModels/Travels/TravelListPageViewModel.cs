using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
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

namespace DiseasePrevention.ViewModels.Travels
{
    public class TravelListPageViewModel : BindableBase, INavigationAware
    {
        private string _title;

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public TravelListPageViewModel(
            INavigationService navigationService,
            IPageDialogService dialogService,
            TravelService travelService
        )
        {
            this._navigationService = navigationService;
            this._dialogService = dialogService;
            this._travelService = travelService;

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
            }

            await this.DownloadListAsync();

            this.IsRunning = false;
        }

        #endregion

        #region ListView

        private readonly TravelService _travelService;

        private string _newsType;

        public string NewsType
        {
            get { return _newsType; }
            set
            {
                SetProperty(ref _newsType, value);
                this._travelService.NewsType = _newsType;
            }
        }

        public NewsListViewModel NewsListViewModel { get; set; }
        = new NewsListViewModel();

        private List<TravelAlert> _itemList = new List<TravelAlert>();

        public async Task DownloadListAsync()
        {
            try
            {
                if (CrossConnectivity.Current.IsConnected)
                {
                    this._itemList = await this._travelService.GetTravelAlertsAsync();

                    foreach (var item in _itemList)
                    {
                        this.NewsListViewModel.ItemsSource.Add(new NewsListItem()
                        {
                            Id = item.Id,
                            Title = item.Headline,
                            PublicationDate = item.Sent
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
            var item = this._itemList.First(x => x.Id == this.NewsListViewModel.SelectedItem.Id);

            var ps = new NavigationParameters {{"SelectedItem", item}};

            await _navigationService.NavigateAsync(new Uri("TravelDetailPage", UriKind.Relative), ps);
        }

        #endregion
    }
}
