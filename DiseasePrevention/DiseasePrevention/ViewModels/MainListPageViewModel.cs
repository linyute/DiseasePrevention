using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using DiseasePrevention.ViewModels.UserControls;
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
            IPageDialogService dialogService
            )
        {
            this.NavigationService = navigationService;
            this.DialogService = dialogService;

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

        protected readonly INavigationService NavigationService;

        protected readonly IPageDialogService DialogService;

        public virtual void OnNavigatedFrom(NavigationParameters parameters)
        {
            
        }

        public virtual void OnNavigatedTo(NavigationParameters parameters)
        {
            
        }

        public virtual async void OnNavigatingTo(NavigationParameters parameters)
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

        protected virtual async Task BuildList(string menuType, string listType)
        {

        }

        #endregion


        #region Command

        protected virtual async void NaviDetailPageAsync()
        {

        }

        #endregion
    }
}
