using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Plugin.DeviceInfo;
using Prism.Navigation;

namespace DiseasePrevention.ViewModels
{
    public class AboutPageViewModel : BindableBase, INavigationAware
    {
        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public AboutPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            
            this.Model = CrossDeviceInfo.Current.Model;
            this.Platform = CrossDeviceInfo.Current.Platform.ToString();
            this.VersionNumber = CrossDeviceInfo.Current.VersionNumber.ToString();

            this.CurrentCulture = CultureInfo.CurrentCulture.Name;
            this.CurrentUICulture = CultureInfo.CurrentUICulture.Name;
        }

        #region Naviation

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
        }

        #endregion

        #region DeviceInfo

        private string _platform;
        public string Platform
        {
            get { return _platform; }
            set { SetProperty(ref _platform, value); }
        }
        
        private string _model;
        public string Model
        {
            get { return _model; }
            set { SetProperty(ref _model, value); }
        }

        private string _versionNumber;
        public string VersionNumber
        {
            get { return _versionNumber; }
            set { SetProperty(ref _versionNumber, value); }
        }

        private string _currentCulture;
        public string CurrentCulture
        {
            get { return _currentCulture; }
            set { SetProperty(ref _currentCulture, value); }
        }

        private string _currentUICulture;
        public string CurrentUICulture
        {
            get { return _currentUICulture; }
            set { SetProperty(ref _currentUICulture, value); }
        }

        #endregion
    }
}
