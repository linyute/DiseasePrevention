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

            this.BuildMenu();
        }

        #region Navigation

        private readonly INavigationService _navigationService;

        public void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            //if (parameters.ContainsKey("Title")) { Title = (string)parameters["Title"]; }
        }

        #endregion

        #region Menu

        private readonly MenuItemService _menuItemService;

        public MainMenuViewModel MainMenuViewModel { get; set; }
            = new MainMenuViewModel();

        private void BuildMenu()
        {
            var items = _menuItemService.MainMenuItems;

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
