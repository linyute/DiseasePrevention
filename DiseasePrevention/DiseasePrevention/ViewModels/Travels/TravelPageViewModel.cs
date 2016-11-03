using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using DiseasePrevention.Services;
using DiseasePrevention.ViewModels.UserControls;
using Prism.Navigation;

namespace DiseasePrevention.ViewModels.Travels
{
    public class TravelPageViewModel : BindableBase, INavigationAware
    {
        public TravelPageViewModel(
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
            var items = this._menuItemService.TravelMenuItems;

            foreach (var item in items)
            {
                this.MainMenuViewModel.MenuItems.Add(item);
            }
        }

        #endregion

    }
}
