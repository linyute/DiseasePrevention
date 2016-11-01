using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using DiseasePrevention.Models;
using DiseasePrevention.Services;
using Prism.Navigation;

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

            BuildMenu();

            MasterMenuItemSelectedCommand = new DelegateCommand(MasterMenuItemSelected);
        }

        private readonly INavigationService _navigationService;

        #region Menu

        private readonly MenuItemService _menuItemService;

        public DelegateCommand MasterMenuItemSelectedCommand { get; private set; }

        private async void MasterMenuItemSelected()
        {
            await _navigationService.NavigateAsync(SelectedMasterMenuItem.Uri);
        }

        private MasterMenuItem _selectedMasterMenuItem;
        public MasterMenuItem SelectedMasterMenuItem
        {
            get { return _selectedMasterMenuItem; }
            set { SetProperty(ref _selectedMasterMenuItem, value); }
        }

        private ObservableCollection<MasterMenuItem> _masterMenuItems = new ObservableCollection<MasterMenuItem>();
        public ObservableCollection<MasterMenuItem> MasterMenuItems
        {
            get { return _masterMenuItems; }
            set { SetProperty(ref _masterMenuItems, value); }
        }

        private void BuildMenu()
        {
            var items = _menuItemService.MainMenuItems;

            foreach (var item in items)
            {
                MasterMenuItems.Add(item);
            }
        }

        #endregion
    }
}
