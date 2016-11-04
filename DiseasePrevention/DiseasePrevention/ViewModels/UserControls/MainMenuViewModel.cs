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
            //this.MenuItemSelectedCommand = new DelegateCommand(MenuItemSelected);
        }

        #region Menu

        //public DelegateCommand MenuItemSelectedCommand { get; private set; }

        //private async void MenuItemSelected()
        //{
        //    await this.SelectedMenuItem.ActionAsync.Invoke();
        //}

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

        #endregion
    }
}
