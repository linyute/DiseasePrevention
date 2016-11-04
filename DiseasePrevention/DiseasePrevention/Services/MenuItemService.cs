using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiseasePrevention.Models;
using Microsoft.Practices.Unity;
using Plugin.Messaging;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using Xamarin.Forms;

namespace DiseasePrevention.Services
{
    public class MenuItemService
    {
        public MenuItemService(
            INavigationService navigationService,
            IPageDialogService dialogService)
        {
            this._navigationService = navigationService;
            this._dialogService = dialogService;

            this.BuildMainMenu();

            this.BuildNewsMenu();

            this.BuildTravelMenu();
        }

        private readonly INavigationService _navigationService;

        private readonly IPageDialogService _dialogService;

        #region 主要選單

        public List<MenuItem> MainMenuItems { get; set; } = new List<MenuItem>();

        private void BuildMainMenu()
        {
            this.MainMenuItems.Add(new MenuItem()
            {
                Text = "首頁",
                Icon = Device.OnPlatform("menu_home.png", "menu_home.png", "Assets/menu_home.png"),
                Command = new DelegateCommand(async () =>
                {
                    await this._navigationService.NavigateAsync(
                        new Uri("xf:///MainMasterDetailPage/MainNavigationPage/MainPage?Title=歡迎", UriKind.Absolute));
                })
            });

            this.MainMenuItems.Add(new MenuItem()
            {
                Text = "最新消息",
                Icon = Device.OnPlatform("menu_rss.png", "menu_rss.png", "Assets/menu_rss.png"),
                Command = new DelegateCommand(async () =>
                {
                    await this._navigationService.NavigateAsync(
                        new Uri("xf:///MainMasterDetailPage/MainNavigationPage/NewsPage?Title=最新消息", UriKind.Absolute));
                })
            });

            this.MainMenuItems.Add(new MenuItem()
            {
                Text = "國際疫情",
                Icon = Device.OnPlatform("menu_rss.png", "menu_rss.png", "Assets/menu_rss.png"),
                Command = new DelegateCommand(async () =>
                {
                    await this._navigationService.NavigateAsync(
                        new Uri("xf:///MainMasterDetailPage/MainNavigationPage/TravelPage?Title=國際疫情", UriKind.Absolute));
                })
            });

            this.MainMenuItems.Add(new MenuItem()
            {
                Text = "疾管局諮詢專線",
                Icon = Device.OnPlatform("menu_info.png", "menu_info.png", "Assets/menu_info.png"),
                Command = new DelegateCommand(() =>
                {
                    if (CrossMessaging.Current.PhoneDialer.CanMakePhoneCall)
                    {
                        CrossMessaging.Current.PhoneDialer.MakePhoneCall("1922", "疾管局諮詢專線");
                    }
                    else
                    {
                        this._dialogService.DisplayAlertAsync("疫情通報及傳染病諮詢", "請撥打 1922 專線", "OK");
                    }
                })
            });

            this.MainMenuItems.Add(new MenuItem()
            {
                Text = "關於防疫小幫手",
                Icon = Device.OnPlatform("menu_info.png", "menu_info.png", "Assets/menu_info.png"),
                Command = new DelegateCommand(async () =>
                {
                    await this._navigationService.NavigateAsync(
                        new Uri("xf:///MainMasterDetailPage/MainNavigationPage/AboutPage?Title=關於防疫小幫手", UriKind.Absolute));
                })
            });
        }

        #endregion

        #region 最新消息

        public List<MenuItem> NewsMenuItems { get; set; } = new List<MenuItem>();

        private void BuildNewsMenu()
        {
            this.NewsMenuItems.Add(new MenuItem()
            {
                Text = "一般民眾版",
                Icon = Device.OnPlatform("menu_rss.png", "menu_rss.png", "Assets/menu_rss.png"),
                Command = new DelegateCommand(async () =>
                {
                    await this._navigationService.NavigateAsync(
                        new Uri("NewsListPage?Title=一般民眾版&NewsType=一般民眾版", UriKind.Relative));
                })
            });

            this.NewsMenuItems.Add(new MenuItem()
            {
                Text = "專業人士版",
                Icon = Device.OnPlatform("menu_rss.png", "menu_rss.png", "Assets/menu_rss.png"),
                Command = new DelegateCommand(async () =>
                {
                    await this._navigationService.NavigateAsync(
                        new Uri("NewsListPage?Title=專業人士版&NewsType=專業人士版", UriKind.Relative));
                })
            });

            this.NewsMenuItems.Add(new MenuItem()
            {
                Text = "致醫界通函",
                Icon = Device.OnPlatform("menu_rss.png", "menu_rss.png", "Assets/menu_rss.png"),
                Command = new DelegateCommand(async () =>
                {
                    await this._navigationService.NavigateAsync(
                        new Uri("NewsListPage?Title=致醫界通函&NewsType=致醫界通函", UriKind.Relative));
                })
            });
        }

        #endregion

        #region 國際疫情

        public List<MenuItem> TravelMenuItems { get; set; } = new List<MenuItem>();

        private void BuildTravelMenu()
        {
            this.TravelMenuItems.Add(new MenuItem()
            {
                Text = "國際重要疫情",
                Icon = Device.OnPlatform("menu_rss.png", "menu_rss.png", "Assets/menu_rss.png"),
                Command = new DelegateCommand(async () =>
                {
                    await this._navigationService.NavigateAsync(
                        new Uri("TravelListPage?Title=國際重要疫情&NewsType=國際重要疫情", UriKind.Relative));
                })
            });

            this.TravelMenuItems.Add(new MenuItem()
            {
                Text = "國際旅遊疫情",
                Icon = Device.OnPlatform("menu_rss.png", "menu_rss.png", "Assets/menu_rss.png"),
                Command = new DelegateCommand(async () =>
                {
                    await this._navigationService.NavigateAsync(
                        new Uri("TravelListPage?Title=國際旅遊疫情&NewsType=國際旅遊疫情", UriKind.Relative));
                })
            });
        }

        #endregion
    }
}
