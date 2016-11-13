using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using DiseasePrevention.Models.News;
using Plugin.Share;
using Prism.Navigation;

namespace DiseasePrevention.ViewModels.News
{
    public class NewsDetailPageViewModel : BindableBase, INavigationAware
    {
        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public NewsDetailPageViewModel(INavigationService navigationService)
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
            
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("Title")) { this.Title = (string)parameters["Title"]; }

            if (parameters.ContainsKey("SelectedItem"))
            {
                SelectedItem = (RssFeed)parameters["SelectedItem"];
            }
        }

        private RssFeed _selectedItem;
        public RssFeed SelectedItem
        {
            get { return _selectedItem; }
            set { SetProperty(ref _selectedItem, value); }
        }

        #endregion

        #region Command

        public DelegateCommand OpenBrowserCommand { get; private set; }

        private async void OpenBrowser()
        {
            if (string.IsNullOrWhiteSpace(SelectedItem.Link))
            {
                return;
            }

            await CrossShare.Current.OpenBrowser(SelectedItem.Link);
        }


        public DelegateCommand CopyLinkCommand { get; private set; }

        private async void CopyLink()
        {
            if (string.IsNullOrWhiteSpace(SelectedItem.Link))
            {
                return;
            }

            if (CrossShare.Current.SupportsClipboard == false)
            {
                return;
            }

            await CrossShare.Current.SetClipboardText(SelectedItem.Link);
        }


        public DelegateCommand ShareLinkCommand { get; private set; }

        private async void ShareLink()
        {
            if (string.IsNullOrWhiteSpace(SelectedItem.Link))
            {
                return;
            }

            await CrossShare.Current.ShareLink(SelectedItem.Link, null, SelectedItem.Title);
        }

        #endregion
    }
}
