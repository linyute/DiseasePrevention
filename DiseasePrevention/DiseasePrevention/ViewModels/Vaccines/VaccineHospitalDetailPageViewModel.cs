using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using DiseasePrevention.Models.Vaccines;
using Plugin.ExternalMaps;
using Plugin.Messaging;
using Plugin.Share;
using Prism.Navigation;
using Prism.Services;

namespace DiseasePrevention.ViewModels.Vaccines
{
    public class VaccineHospitalDetailPageViewModel : BindableBase, INavigationAware
    {
        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public VaccineHospitalDetailPageViewModel(
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
            //if (parameters.ContainsKey("Title")) { this.Title = (string)parameters["Title"]; }

            if (parameters.ContainsKey("SelectedItem"))
            {
                this.SelectedItem = (VaccineHospital)parameters["SelectedItem"];
            }
        }

        private VaccineHospital _selectedItem;
        public VaccineHospital SelectedItem
        {
            get { return _selectedItem; }
            set { SetProperty(ref _selectedItem, value); }
        }

        #endregion

        #region Command

        public DelegateCommand OpenExternalMapsCommand { get; private set; }
        private async void OpenExternalMaps()
        {
            if (string.IsNullOrWhiteSpace(SelectedItem.地址))
            {
                return;
            }

            await CrossExternalMaps.Current.NavigateTo(
                SelectedItem.合約醫療院所名稱,
                SelectedItem.地址 + " " + SelectedItem.合約醫療院所名稱,
                string.Empty,
                string.Empty,
                string.Empty,
                string.Empty,
                string.Empty
                );
        }


        public DelegateCommand MakePhoneCallCommand { get; private set; }

        private async void MakePhoneCall()
        {
            CrossMessaging.Current.PhoneDialer.MakePhoneCall(SelectedItem.連絡電話, SelectedItem.合約醫療院所名稱);
        }

        #endregion
    }
}
