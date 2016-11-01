﻿using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using DiseasePrevention.Models;
using DiseasePrevention.Services;

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
            _navigationService = navigationService;
            _menuItemService = menuItemService;

            MasterMenuItemSelectedCommand = new DelegateCommand(MasterMenuItemSelected);

            BuildMenu();
        }

        #region Navigation

        private readonly INavigationService _navigationService;

        public void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("title"))
                Title = (string)parameters["title"] + " and Prism";
        }

        #endregion

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
                //if (item.Title == "首頁")
                //{
                //    continue;
                //}
                MasterMenuItems.Add(item);
            }
        }

        #endregion
    }
}
