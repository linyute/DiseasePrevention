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
using DiseasePrevention.Models.Travels;
using DiseasePrevention.Models.Vaccines;
using DiseasePrevention.Services;
using DiseasePrevention.Services.News;
using DiseasePrevention.Services.Travels;
using DiseasePrevention.Services.Vaccines;
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
            TravelService travelService,
            VaccineService vaccineService
            )
        {
            this._navigationService = navigationService;
            this._dialogService = dialogService;

            this._newsService = newsService;
            this._travelService = travelService;
            this._vaccineService = vaccineService;

            this.MainListViewModel.ItemSelectedCommand =
                new DelegateCommand(NaviDetailPageAsync, () => this.IsRunning == false);
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

            await this.BuildList(this.MenuType, this.ListType);

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

        private async Task BuildList(string menuType, string listType)
        {
            this.MainListViewModel.ItemsSource.Clear();

            switch (menuType)
            {
                case "最新消息":
                    await this.DownloadNewsListAsync(listType);
                    break;
                case "傳染病介紹":
                    await this.GetDiseaseListAsync(listType);
                    break;
                case "國際疫情":
                    await this.DownloadTravelListAsync(listType);
                    break;
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

        #endregion
        

        #region Command

        private async void NaviDetailPageAsync()
        {
            switch (this.MenuType)
            {
                case "最新消息":
                    await this.NaviNewsDetailPageAsync();
                    break;
                case "傳染病介紹":
                    await this.NaviDiseaseDetailPageAsync();
                    break;
                case "國際疫情":
                    await this.NaviTravelDetailPageAsync();
                    break;
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

        #endregion


        #region 最新消息

        private readonly NewsService _newsService;

        private List<RssFeed> _newsList = new List<RssFeed>();

        private async Task DownloadNewsListAsync(string listType)
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

        private async Task NaviNewsDetailPageAsync()
        {
            var item = this._newsList.First(x => x.Guid == this.MainListViewModel.SelectedItem.Id);

            var title = "新聞稿內容";

            var ps = new NavigationParameters { { "SelectedItem", item } };

            await _navigationService.NavigateAsync(new Uri($"NewsDetailPage?Title={title}", UriKind.Relative), ps);
        }

        #endregion

        #region 傳染病介紹

        private async Task GetDiseaseListAsync(string listType)
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

        private async Task NaviDiseaseDetailPageAsync()
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

        private async Task DownloadTravelListAsync(string listType)
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
                    await _dialogService.DisplayAlertAsync("無法連線", "請開啟網路", "OK");
                }
            }
            catch (Exception ex)
            {
                await _dialogService.DisplayAlertAsync("發生錯誤", ex.Message, "OK");
            }
        }

        private async Task NaviTravelDetailPageAsync()
        {
            var item = this._travelList.First(x => x.Id == this.MainListViewModel.SelectedItem.Id);

            var ps = new NavigationParameters { { "SelectedItem", item } };

            await _navigationService.NavigateAsync(new Uri("TravelDetailPage", UriKind.Relative), ps);
        }

        #endregion

        #region 預防接種

        private readonly VaccineService _vaccineService;

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
                    await _dialogService.DisplayAlertAsync("無法連線", "請開啟網路", "OK");
                }
            }
            catch (Exception ex)
            {
                await _dialogService.DisplayAlertAsync("發生錯誤", ex.Message, "OK");
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
            var url = $"MainListPage?Title=預防接種單位查詢&MenuType=預防接種單位行政區&ListType={this.MainListViewModel.SelectedItem.Id}";

            await _navigationService.NavigateAsync(new Uri(url, UriKind.Relative));
        }

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

            var url = $"MainListPage?Title=預防接種單位查詢&MenuType=預防接種單位查詢&ListType={cityAndDistrict}";

            await _navigationService.NavigateAsync(new Uri(url, UriKind.Relative));
        }

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

            await _navigationService.NavigateAsync(new Uri("VaccineHospitalDetailPage", UriKind.Relative), ps);
        }

        #endregion
    }
}
