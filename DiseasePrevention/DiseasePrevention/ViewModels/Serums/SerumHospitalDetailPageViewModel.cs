using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using DiseasePrevention.Models.Serums;
using Plugin.ExternalMaps;
using Plugin.Messaging;
using Plugin.Share;
using Prism.Navigation;
using Prism.Services;

namespace DiseasePrevention.ViewModels.Serums
{
    public class SerumHospitalDetailPageViewModel : BindableBase, INavigationAware
    {
        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public SerumHospitalDetailPageViewModel(
            INavigationService navigationService,
            IPageDialogService dialogService)
        {
            this._navigationService = navigationService;
            this._dialogService = dialogService;

            OpenExternalMapsCommand = new DelegateCommand(OpenExternalMaps);

            MakePhoneCallCommand = new DelegateCommand(MakePhoneCall, () => CrossMessaging.Current.PhoneDialer.CanMakePhoneCall);
        }

        #region Navigation

        private readonly INavigationService _navigationService;

        private readonly IPageDialogService _dialogService;

        public void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("Title")) { this.Title = (string)parameters["Title"]; }

            if (parameters.ContainsKey("SelectedItem"))
            {
                this.SelectedItem = (HospitalSerums)parameters["SelectedItem"];
            }
        }

        private HospitalSerums _selectedItem;
        public HospitalSerums SelectedItem
        {
            get { return _selectedItem; }
            set { SetProperty(ref _selectedItem, value); }
        }

        #endregion

        #region Command

        public DelegateCommand OpenExternalMapsCommand { get; private set; }
        private async void OpenExternalMaps()
        {
            if (string.IsNullOrWhiteSpace(SelectedItem.醫療院所名稱))
            {
                return;
            }

            await CrossExternalMaps.Current.NavigateTo(
                SelectedItem.醫療院所名稱,
                SelectedItem.醫療院所名稱,
                string.Empty,
                string.Empty,
                string.Empty,
                string.Empty,
                string.Empty
                );
        }


        public DelegateCommand MakePhoneCallCommand { get; private set; }

        private void MakePhoneCall()
        {
            CrossMessaging.Current.PhoneDialer.MakePhoneCall(SelectedItem.聯絡電話, SelectedItem.醫療院所名稱);
        }

        #endregion
    }
}
