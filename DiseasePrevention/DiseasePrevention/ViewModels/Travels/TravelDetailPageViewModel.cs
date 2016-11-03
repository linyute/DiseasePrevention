using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using DiseasePrevention.Models.Travels;
using Plugin.Share;
using Prism.Navigation;

namespace DiseasePrevention.ViewModels.Travels
{
    public class TravelDetailPageViewModel : BindableBase, INavigationAware
    {
        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public TravelDetailPageViewModel(INavigationService navigationService)
        {
            this._navigationService = navigationService;

            OpenBrowserCommand = new DelegateCommand(OpenBrowser);
            CopyLinkCommand = new DelegateCommand(CopyLink);
            ShareLinkCommand = new DelegateCommand(ShareLink);
        }

        #region Navigation

        private readonly INavigationService _navigationService;

        public void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("SelectedItem"))
            {
                SelectedItem = parameters["SelectedItem"] as TravelAlert;
            }
        }

        private TravelAlert _selectedItem;
        public TravelAlert SelectedItem
        {
            get { return _selectedItem; }
            set { SetProperty(ref _selectedItem, value); }
        }

        #endregion

        #region Command

        public DelegateCommand OpenBrowserCommand { get; private set; }

        private async void OpenBrowser()
        {
            if (string.IsNullOrWhiteSpace(SelectedItem.Web))
            {
                return;
            }

            await CrossShare.Current.OpenBrowser(SelectedItem.Web);
        }


        public DelegateCommand CopyLinkCommand { get; private set; }

        private async void CopyLink()
        {
            if (string.IsNullOrWhiteSpace(SelectedItem.Web))
            {
                return;
            }

            if (CrossShare.Current.SupportsClipboard == false)
            {
                return;
            }

            await CrossShare.Current.SetClipboardText(SelectedItem.Web);
        }


        public DelegateCommand ShareLinkCommand { get; private set; }

        private async void ShareLink()
        {
            if (string.IsNullOrWhiteSpace(SelectedItem.Web))
            {
                return;
            }

            await CrossShare.Current.ShareLink(SelectedItem.Web, null, SelectedItem.Headline);
        }

        #endregion
    }
}
