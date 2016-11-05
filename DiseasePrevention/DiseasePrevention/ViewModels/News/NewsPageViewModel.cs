using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using DiseasePrevention.Services;
using DiseasePrevention.ViewModels.UserControls;
using Prism.Navigation;
using Xamarin.Forms;

namespace DiseasePrevention.ViewModels.News
{
    public class NewsPageViewModel : BindableBase, INavigationAware
    {
        private string _title;

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public NewsPageViewModel(
            INavigationService navigationService, 
            MenuItemService menuItemService)
        {
            this._navigationService = navigationService;
            this._menuItemService = menuItemService;
        }

        #region Navigation

        private readonly INavigationService _navigationService;

        public void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("Title")) { this.Title = (string)parameters["Title"]; }

            if (parameters.ContainsKey("MenuType")) { this.MenuType = (string)parameters["MenuType"]; }

            this.BuildMenu();
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
            var items = this.MenuType == "最新消息" ? 
                this._menuItemService.NewsMenuItems : 
                this._menuItemService.DiseaseMenuItems;

            foreach (var item in items)
            {
                this.MainMenuViewModel.MenuItems.Add(item);
            }
        }

        #endregion

    }
}
