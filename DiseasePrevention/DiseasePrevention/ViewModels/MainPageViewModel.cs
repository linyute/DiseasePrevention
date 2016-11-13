using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using DiseasePrevention.Models;
using DiseasePrevention.Services;
using DiseasePrevention.ViewModels.UserControls;
using Xamarin.Forms;

namespace DiseasePrevention.ViewModels
{
    public class MainPageViewModel : BindableBase, INavigationAware
    {
        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public MainPageViewModel(
            INavigationService navigationService,
            MenuItemService menuItemService)
        {
            this._navigationService = navigationService;
            this._menuItemService = menuItemService;
        }

        private bool _isRunning = true;

        public bool IsRunning
        {
            get { return _isRunning; }
            set
            {
                SetProperty(ref _isRunning, value);
                this.MainMenuViewModel.IsRunning = value;
            }
        }

        #region Navigation

        private readonly INavigationService _navigationService;

        public void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("Title")) { this.Title = (string)parameters["Title"]; }

            if (parameters.ContainsKey("MenuType")) { this.MenuType = (string)parameters["MenuType"]; }

            this.BuildMenu();

            this.IsRunning = false;
        }

        #endregion

        #region Menu

        private string _menuType;

        public string MenuType
        {
            get { return _menuType; }
            set { SetProperty(ref _menuType, value); }
        }

        private readonly MenuItemService _menuItemService;

        public MainMenuViewModel MainMenuViewModel { get; set; }
            = new MainMenuViewModel();

        private void BuildMenu()
        {
            this.MainMenuViewModel.MenuItems.Clear();

            List<MenuItem> items = null;

            switch (this.MenuType)
            {
                case "首頁":
                    items = this._menuItemService.MainMenuItems;
                    break;
                case "最新消息":
                    items = this._menuItemService.NewsMenuItems;
                    break;
                case "國際疫情":
                    items = this._menuItemService.TravelMenuItems;
                    break;
                case "傳染病介紹":
                    items = this._menuItemService.DiseaseMenuItems;
                    break;
                case "疫苗接種":
                    items = this._menuItemService.VaccineMenuItems;
                    break;
                case "抗蛇毒血清":
                    items = this._menuItemService.SerumMenuItems;
                    break;
                case "疾管署防疫專區":
                    items = this._menuItemService.CDCAreaMenuItems;
                    break;
                default:
                    items = new List<MenuItem>();
                    break;
            }

            foreach (var item in items)
            {
                if (item.Text == "首頁")
                {
                    continue;
                }
                this.MainMenuViewModel.MenuItems.Add(item);
            }
        }

        #endregion
    }
}
