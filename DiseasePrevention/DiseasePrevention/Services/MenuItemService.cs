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

            this.BuildDiseaseMenu();

            this.BuildVaccineMenu();
        }

        private readonly INavigationService _navigationService;

        private readonly IPageDialogService _dialogService;

        #region 主要選單

        /// <summary>
        /// 主要選單
        /// </summary>
        public List<MenuItem> MainMenuItems { get; set; } = new List<MenuItem>();

        /// <summary>
        /// 主要選單
        /// </summary>
        private void BuildMainMenu()
        {
            this.MainMenuItems.Add(new MenuItem()
            {
                Text = "首頁",
                Icon = Device.OnPlatform("menu_home.png", "menu_home.png", "Assets/menu_home.png"),
                Command = new DelegateCommand(async () =>
                {
                    await this._navigationService.NavigateAsync(
                        new Uri("xf:///MainMasterDetailPage/MainNavigationPage/MainPage?Title=歡迎&MenuType=首頁", UriKind.Absolute));
                })
            });

            this.MainMenuItems.Add(new MenuItem()
            {
                Text = "最新消息",
                Icon = Device.OnPlatform("menu_rss.png", "menu_rss.png", "Assets/menu_rss.png"),
                Command = new DelegateCommand(async () =>
                {
                    await this._navigationService.NavigateAsync(
                        new Uri("xf:///MainMasterDetailPage/MainNavigationPage/MainPage?Title=最新消息&MenuType=最新消息", UriKind.Absolute));
                })
            });

            this.MainMenuItems.Add(new MenuItem()
            {
                Text = "國際疫情",
                Icon = Device.OnPlatform("menu_rss.png", "menu_rss.png", "Assets/menu_rss.png"),
                Command = new DelegateCommand(async () =>
                {
                    await this._navigationService.NavigateAsync(
                        new Uri("xf:///MainMasterDetailPage/MainNavigationPage/MainPage?Title=國際疫情&MenuType=國際疫情", UriKind.Absolute));
                })
            });

            this.MainMenuItems.Add(new MenuItem()
            {
                Text = "傳染病介紹",
                Icon = Device.OnPlatform("menu_rss.png", "menu_rss.png", "Assets/menu_rss.png"),
                Command = new DelegateCommand(async () =>
                {
                    await this._navigationService.NavigateAsync(
                        new Uri("xf:///MainMasterDetailPage/MainNavigationPage/MainPage?Title=傳染病介紹&MenuType=傳染病介紹", UriKind.Absolute));
                })
            });

            this.MainMenuItems.Add(new MenuItem()
            {
                Text = "疫苗接種",
                Icon = Device.OnPlatform("menu_rss.png", "menu_rss.png", "Assets/menu_rss.png"),
                Command = new DelegateCommand(async () =>
                {
                    await this._navigationService.NavigateAsync(
                        new Uri("xf:///MainMasterDetailPage/MainNavigationPage/MainPage?Title=疫苗接種&MenuType=疫苗接種", UriKind.Absolute));
                })
            });

            this.MainMenuItems.Add(new MenuItem()
            {
                Text = "疾管署諮詢專線",
                Icon = Device.OnPlatform("menu_info.png", "menu_info.png", "Assets/menu_info.png"),
                Command = new DelegateCommand(() =>
                {
                    if (CrossMessaging.Current.PhoneDialer.CanMakePhoneCall)
                    {
                        CrossMessaging.Current.PhoneDialer.MakePhoneCall("1922", "疾管署諮詢專線");
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

        /// <summary>
        /// 最新消息
        /// </summary>
        public List<MenuItem> NewsMenuItems { get; set; } = new List<MenuItem>();

        /// <summary>
        /// 最新消息
        /// </summary>
        private void BuildNewsMenu()
        {
            this.NewsMenuItems.Add(new MenuItem()
            {
                Text = "一般民眾版",
                Icon = Device.OnPlatform("menu_rss.png", "menu_rss.png", "Assets/menu_rss.png"),
                Command = new DelegateCommand(async () =>
                {
                    await this._navigationService.NavigateAsync(
                        new Uri("MainListPage?Title=一般民眾版&MenuType=最新消息&ListType=一般民眾版", UriKind.Relative));
                })
            });

            this.NewsMenuItems.Add(new MenuItem()
            {
                Text = "專業人士版",
                Icon = Device.OnPlatform("menu_rss.png", "menu_rss.png", "Assets/menu_rss.png"),
                Command = new DelegateCommand(async () =>
                {
                    await this._navigationService.NavigateAsync(
                        new Uri("MainListPage?Title=專業人士版&MenuType=最新消息&ListType=專業人士版", UriKind.Relative));
                })
            });

            this.NewsMenuItems.Add(new MenuItem()
            {
                Text = "致醫界通函",
                Icon = Device.OnPlatform("menu_rss.png", "menu_rss.png", "Assets/menu_rss.png"),
                Command = new DelegateCommand(async () =>
                {
                    await this._navigationService.NavigateAsync(
                        new Uri("MainListPage?Title=致醫界通函&MenuType=最新消息&ListType=致醫界通函", UriKind.Relative));
                })
            });
        }

        #endregion

        #region 國際疫情

        /// <summary>
        /// 國際疫情
        /// </summary>
        public List<MenuItem> TravelMenuItems { get; set; } = new List<MenuItem>();

        /// <summary>
        /// 國際疫情
        /// </summary>
        private void BuildTravelMenu()
        {
            this.TravelMenuItems.Add(new MenuItem()
            {
                Text = "國際重要疫情",
                Icon = Device.OnPlatform("menu_rss.png", "menu_rss.png", "Assets/menu_rss.png"),
                Command = new DelegateCommand(async () =>
                {
                    await this._navigationService.NavigateAsync(
                        new Uri("MainListPage?Title=國際重要疫情&MenuType=國際疫情&ListType=國際重要疫情", UriKind.Relative));
                })
            });

            this.TravelMenuItems.Add(new MenuItem()
            {
                Text = "國際旅遊疫情",
                Icon = Device.OnPlatform("menu_rss.png", "menu_rss.png", "Assets/menu_rss.png"),
                Command = new DelegateCommand(async () =>
                {
                    await this._navigationService.NavigateAsync(
                        new Uri("MainListPage?Title=國際旅遊疫情&MenuType=國際疫情&ListType=國際旅遊疫情", UriKind.Relative));
                })
            });
        }

        #endregion

        #region 傳染病介紹

        /// <summary>
        /// 傳染病介紹
        /// </summary>
        public List<MenuItem> DiseaseMenuItems { get; set; } = new List<MenuItem>();

        /// <summary>
        /// 傳染病介紹
        /// </summary>
        private void BuildDiseaseMenu()
        {
            this.DiseaseMenuItems.Add(new MenuItem()
            {
                Text = "食物或飲水傳染",
                //Icon = Device.OnPlatform("menu_rss.png", "menu_rss.png", "Assets/menu_rss.png"),
                Command = new DelegateCommand(async () =>
                {
                    await this._navigationService.NavigateAsync(
                        new Uri("MainListPage?Title=食物或飲水傳染&MenuType=傳染病介紹&ListType=食物或飲水傳染", UriKind.Relative));
                })
            });

            this.DiseaseMenuItems.Add(new MenuItem()
            {
                Text = "空氣或飛沫傳染",
                //Icon = Device.OnPlatform("menu_rss.png", "menu_rss.png", "Assets/menu_rss.png"),
                Command = new DelegateCommand(async () =>
                {
                    await this._navigationService.NavigateAsync(
                        new Uri("MainListPage?Title=空氣或飛沫傳染&MenuType=傳染病介紹&ListType=空氣或飛沫傳染", UriKind.Relative));
                })
            });

            this.DiseaseMenuItems.Add(new MenuItem()
            {
                Text = "蟲媒傳染",
                //Icon = Device.OnPlatform("menu_rss.png", "menu_rss.png", "Assets/menu_rss.png"),
                Command = new DelegateCommand(async () =>
                {
                    await this._navigationService.NavigateAsync(
                        new Uri("MainListPage?Title=蟲媒傳染&MenuType=傳染病介紹&ListType=蟲媒傳染", UriKind.Relative));
                })
            });

            this.DiseaseMenuItems.Add(new MenuItem()
            {
                Text = "性接觸或血液傳染",
                //Icon = Device.OnPlatform("menu_rss.png", "menu_rss.png", "Assets/menu_rss.png"),
                Command = new DelegateCommand(async () =>
                {
                    await this._navigationService.NavigateAsync(
                        new Uri("MainListPage?Title=性接觸或血液傳染&MenuType=傳染病介紹&ListType=性接觸或血液傳染", UriKind.Relative));
                })
            });

            this.DiseaseMenuItems.Add(new MenuItem()
            {
                Text = "接觸傳染",
                //Icon = Device.OnPlatform("menu_rss.png", "menu_rss.png", "Assets/menu_rss.png"),
                Command = new DelegateCommand(async () =>
                {
                    await this._navigationService.NavigateAsync(
                        new Uri("MainListPage?Title=接觸傳染&MenuType=傳染病介紹&ListType=接觸傳染", UriKind.Relative));
                })
            });

            this.DiseaseMenuItems.Add(new MenuItem()
            {
                Text = "其他類",
                //Icon = Device.OnPlatform("menu_rss.png", "menu_rss.png", "Assets/menu_rss.png"),
                Command = new DelegateCommand(async () =>
                {
                    await this._navigationService.NavigateAsync(
                        new Uri("MainListPage?Title=其他類&MenuType=傳染病介紹&ListType=其他類", UriKind.Relative));
                })
            });
        }

        #endregion

        #region 疫苗接種

        /// <summary>
        /// 疫苗接種
        /// </summary>
        public List<MenuItem> VaccineMenuItems { get; set; } = new List<MenuItem>();

        /// <summary>
        /// 疫苗接種
        /// </summary>
        private void BuildVaccineMenu()
        {
            this.VaccineMenuItems.Add(new MenuItem()
            {
                Text = "幼兒疫苗接種",
                Icon = Device.OnPlatform("menu_rss.png", "menu_rss.png", "Assets/menu_rss.png"),
                Command = new DelegateCommand(async () =>
                {
                    await this._navigationService.NavigateAsync(
                        new Uri("ChildVaccinePage?Title=幼兒疫苗接種", UriKind.Relative));
                })
            });
        }

        #endregion
    }
}
