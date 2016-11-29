using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using DiseasePrevention.Models;
using Xamarin.Forms;

namespace DiseasePrevention.ViewModels.UserControls
{
    public class MainMenuViewModel : BindableBase
    {
        public MainMenuViewModel()
        {
            this.MenuItemSelectedCommand = new DelegateCommand(MenuItemSelected);
        }

        private bool _isRunning = true;

        public bool IsRunning
        {
            get { return _isRunning; }
            set { SetProperty(ref _isRunning, value); }
        }

        #region Menu

        public DelegateCommand MenuItemSelectedCommand { get; private set; }

        private void MenuItemSelected()
        {
            SelectedMenuItem.Command.Execute(SelectedMenuItem.CommandParameter);
            SelectedMenuItem = null;
        }

        private MainMenuItem _selectedMenuItem;
        public MainMenuItem SelectedMenuItem
        {
            get { return _selectedMenuItem; }
            set { SetProperty(ref _selectedMenuItem, value); }
        }

        private ObservableCollection<MainMenuItem> _menuItems = new ObservableCollection<MainMenuItem>();
        public ObservableCollection<MainMenuItem> MenuItems
        {
            get { return _menuItems; }
            set { SetProperty(ref _menuItems, value); }
        }

        #endregion
    }
}
