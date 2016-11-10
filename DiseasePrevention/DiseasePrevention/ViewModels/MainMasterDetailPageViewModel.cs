using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using DiseasePrevention.Models;
using DiseasePrevention.Services;
using Prism.Navigation;
using Xamarin.Forms;

namespace DiseasePrevention.ViewModels
{
    public class MainMasterDetailPageViewModel : BindableBase
    {
        public MainMasterDetailPageViewModel(
            INavigationService navigationService,
            MenuItemService menuItemService)
        {
            _navigationService = navigationService;
            _menuItemService = menuItemService;

            MenuItemSelectedCommand = new DelegateCommand(MenuItemSelected);

            this.BuildMenu();

            this.IsRunning = false;
        }

        private readonly INavigationService _navigationService;

        private bool _isRunning = true;

        public bool IsRunning
        {
            get { return _isRunning; }
            set { SetProperty(ref _isRunning, value); }
        }

        #region Menu

        private readonly MenuItemService _menuItemService;

        public DelegateCommand MenuItemSelectedCommand { get; private set; }

        private void MenuItemSelected()
        {
            SelectedMenuItem.Command.Execute(SelectedMenuItem.CommandParameter);
            SelectedMenuItem = null;
        }

        private MenuItem _selectedMenuItem;
        public MenuItem SelectedMenuItem
        {
            get { return _selectedMenuItem; }
            set { SetProperty(ref _selectedMenuItem, value); }
        }

        private ObservableCollection<MenuItem> _menuItems = new ObservableCollection<MenuItem>();
        public ObservableCollection<MenuItem> MenuItems
        {
            get { return _menuItems; }
            set { SetProperty(ref _menuItems, value); }
        }

        private void BuildMenu()
        {
            var items = _menuItemService.MainMenuItems;

            foreach (var item in items)
            {
                this.MenuItems.Add(item);
            }
        }

        #endregion
    }
}
