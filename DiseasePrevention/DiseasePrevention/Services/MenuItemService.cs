﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiseasePrevention.Models;
using Microsoft.Practices.Unity;
using Plugin.Messaging;
using Plugin.Share;
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

            this.BuildSerumMenu();

            this.BuildCDCAreaMenu();
        }

        private readonly INavigationService _navigationService;

        private readonly IPageDialogService _dialogService;

        #region 主要選單

        /// <summary>
        /// 主要選單
        /// </summary>
        public List<MainMenuItem> MainMenuItems { get; set; } = new List<MainMenuItem>();

        /// <summary>
        /// 主要選單
        /// </summary>
        private void BuildMainMenu()
        {
            this.MainMenuItems.Add(new MainMenuItem()
            {
                Text = "首頁",
                Icon = Device.OnPlatform("menu_home.png", "menu_home.png", "Assets/menu_home.png"),
                BackgroundColor = Color.Black,
                Command = new DelegateCommand(async () =>
                {
                    await this._navigationService.NavigateAsync(
                        new Uri("xf:///MainMasterDetailPage/MainNavigationPage/MainPage?Title=歡迎&MenuType=首頁", UriKind.Absolute));
                })
            });

            this.MainMenuItems.Add(new MainMenuItem()
            {
                Text = "最新消息",
                Icon = Device.OnPlatform("menu_rss.png", "menu_rss.png", "Assets/menu_rss.png"),
                BackgroundColor = Color.FromRgb(66, 147, 195),
                Command = new DelegateCommand(async () =>
                {
                    await this._navigationService.NavigateAsync(
                        new Uri("xf:///MainMasterDetailPage/MainNavigationPage/MainPage?Title=最新消息&MenuType=最新消息", UriKind.Absolute));
                })
            });

            this.MainMenuItems.Add(new MainMenuItem()
            {
                Text = "國際疫情",
                Icon = Device.OnPlatform("menu_globe.png", "menu_globe.png", "Assets/menu_globe.png"),
                BackgroundColor = Color.FromRgb(177, 104, 168),
                Command = new DelegateCommand(async () =>
                {
                    await this._navigationService.NavigateAsync(
                        new Uri("xf:///MainMasterDetailPage/MainNavigationPage/MainPage?Title=國際疫情&MenuType=國際疫情", UriKind.Absolute));
                })
            });

            this.MainMenuItems.Add(new MainMenuItem()
            {
                Text = "傳染病介紹",
                Icon = Device.OnPlatform("menu_sick.png", "menu_sick.png", "Assets/menu_sick.png"),
                BackgroundColor = Color.FromRgb(249, 142, 173),
                Command = new DelegateCommand(async () =>
                {
                    await this._navigationService.NavigateAsync(
                        new Uri("xf:///MainMasterDetailPage/MainNavigationPage/MainPage?Title=傳染病介紹&MenuType=傳染病介紹", UriKind.Absolute));
                })
            });

            this.MainMenuItems.Add(new MainMenuItem()
            {
                Text = "疫苗接種",
                Icon = Device.OnPlatform("menu_syringe.png", "menu_syringe.png", "Assets/menu_syringe.png"),
                BackgroundColor = Color.FromRgb(251, 154, 133),
                Command = new DelegateCommand(async () =>
                {
                    await this._navigationService.NavigateAsync(
                        new Uri("xf:///MainMasterDetailPage/MainNavigationPage/MainPage?Title=疫苗接種&MenuType=疫苗接種", UriKind.Absolute));
                })
            });

            this.MainMenuItems.Add(new MainMenuItem()
            {
                Text = "抗蛇毒血清",
                Icon = Device.OnPlatform("menu_snake.png", "menu_snake.png", "Assets/menu_snake.png"),
                BackgroundColor = Color.FromRgb(240, 182, 148),
                Command = new DelegateCommand(async () =>
                {
                    await this._navigationService.NavigateAsync(
                        new Uri("xf:///MainMasterDetailPage/MainNavigationPage/MainPage?Title=抗蛇毒血清&MenuType=抗蛇毒血清", UriKind.Absolute));
                })
            });

            this.MainMenuItems.Add(new MainMenuItem()
            {
                Text = "疾管署防疫專區",
                Icon = Device.OnPlatform("menu_cdc.png", "menu_cdc.png", "Assets/menu_cdc.png"),
                BackgroundColor = Color.FromRgb(221, 184, 126),
                Command = new DelegateCommand(async () =>
                {
                    await this._navigationService.NavigateAsync(
                        new Uri("xf:///MainMasterDetailPage/MainNavigationPage/MainPage?Title=疾管署防疫專區&MenuType=疾管署防疫專區", UriKind.Absolute));
                })
            });

            this.MainMenuItems.Add(new MainMenuItem()
            {
                Text = "關於防疫小幫手",
                Icon = Device.OnPlatform("menu_info.png", "menu_info.png", "Assets/menu_info.png"),
                BackgroundColor = Color.FromRgb(151, 121, 146),
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
        public List<MainMenuItem> NewsMenuItems { get; set; } = new List<MainMenuItem>();

        /// <summary>
        /// 最新消息
        /// </summary>
        private void BuildNewsMenu()
        {
            this.NewsMenuItems.Add(new MainMenuItem()
            {
                Text = "一般民眾版",
                Icon = Device.OnPlatform("menu_children.png", "menu_children.png", "Assets/menu_children.png"),
                BackgroundColor = Color.FromRgb(117, 193, 221),
                Command = new DelegateCommand(async () =>
                {
                    await this._navigationService.NavigateAsync(
                        new Uri("NewsListPage?Title=一般民眾版&MenuType=最新消息&ListType=一般民眾版", UriKind.Relative));
                })
            });

            this.NewsMenuItems.Add(new MainMenuItem()
            {
                Text = "專業人士版",
                Icon = Device.OnPlatform("menu_employee.png", "menu_employee.png", "Assets/menu_employee.png"),
                BackgroundColor = Color.FromRgb(143, 143, 190),
                Command = new DelegateCommand(async () =>
                {
                    await this._navigationService.NavigateAsync(
                        new Uri("NewsListPage?Title=專業人士版&MenuType=最新消息&ListType=專業人士版", UriKind.Relative));
                })
            });

            this.NewsMenuItems.Add(new MainMenuItem()
            {
                Text = "致醫界通函",
                Icon = Device.OnPlatform("menu_doctor.png", "menu_doctor.png", "Assets/menu_doctor.png"),
                BackgroundColor = Color.FromRgb(247, 197, 200),
                Command = new DelegateCommand(async () =>
                {
                    await this._navigationService.NavigateAsync(
                        new Uri("NewsListPage?Title=致醫界通函&MenuType=最新消息&ListType=致醫界通函", UriKind.Relative));
                })
            });
        }

        #endregion

        #region 國際疫情

        /// <summary>
        /// 國際疫情
        /// </summary>
        public List<MainMenuItem> TravelMenuItems { get; set; } = new List<MainMenuItem>();

        /// <summary>
        /// 國際疫情
        /// </summary>
        private void BuildTravelMenu()
        {
            this.TravelMenuItems.Add(new MainMenuItem()
            {
                Text = "國際重要疫情",
                Icon = Device.OnPlatform("menu_globe.png", "menu_globe.png", "Assets/menu_globe.png"),
                BackgroundColor = Color.FromRgb(189, 209, 234),
                Command = new DelegateCommand(async () =>
                {
                    await this._navigationService.NavigateAsync(
                        new Uri("TravelListPage?Title=國際重要疫情&MenuType=國際疫情&ListType=國際重要疫情", UriKind.Relative));
                })
            });

            this.TravelMenuItems.Add(new MainMenuItem()
            {
                Text = "國際旅遊疫情",
                Icon = Device.OnPlatform("menu_aeroplane.png", "menu_aeroplane.png", "Assets/menu_aeroplane.png"),
                BackgroundColor = Color.FromRgb(188, 192, 221),
                Command = new DelegateCommand(async () =>
                {
                    await this._navigationService.NavigateAsync(
                        new Uri("TravelListPage?Title=國際旅遊疫情&MenuType=國際疫情&ListType=國際旅遊疫情", UriKind.Relative));
                })
            });
        }

        #endregion

        #region 傳染病介紹

        /// <summary>
        /// 傳染病介紹
        /// </summary>
        public List<MainMenuItem> DiseaseMenuItems { get; set; } = new List<MainMenuItem>();

        /// <summary>
        /// 傳染病介紹
        /// </summary>
        private void BuildDiseaseMenu()
        {
            this.DiseaseMenuItems.Add(new MainMenuItem()
            {
                Text = "食物或飲水傳染",
                //Icon = Device.OnPlatform("menu_rss.png", "menu_rss.png", "Assets/menu_rss.png"),
                Command = new DelegateCommand(async () =>
                {
                    await this._navigationService.NavigateAsync(
                        new Uri("DiseaseListPage?Title=食物或飲水傳染&MenuType=傳染病介紹&ListType=食物或飲水傳染", UriKind.Relative));
                })
            });

            this.DiseaseMenuItems.Add(new MainMenuItem()
            {
                Text = "空氣或飛沫傳染",
                //Icon = Device.OnPlatform("menu_rss.png", "menu_rss.png", "Assets/menu_rss.png"),
                Command = new DelegateCommand(async () =>
                {
                    await this._navigationService.NavigateAsync(
                        new Uri("DiseaseListPage?Title=空氣或飛沫傳染&MenuType=傳染病介紹&ListType=空氣或飛沫傳染", UriKind.Relative));
                })
            });

            this.DiseaseMenuItems.Add(new MainMenuItem()
            {
                Text = "蟲媒傳染",
                //Icon = Device.OnPlatform("menu_rss.png", "menu_rss.png", "Assets/menu_rss.png"),
                Command = new DelegateCommand(async () =>
                {
                    await this._navigationService.NavigateAsync(
                        new Uri("DiseaseListPage?Title=蟲媒傳染&MenuType=傳染病介紹&ListType=蟲媒傳染", UriKind.Relative));
                })
            });

            this.DiseaseMenuItems.Add(new MainMenuItem()
            {
                Text = "性接觸或血液傳染",
                //Icon = Device.OnPlatform("menu_rss.png", "menu_rss.png", "Assets/menu_rss.png"),
                Command = new DelegateCommand(async () =>
                {
                    await this._navigationService.NavigateAsync(
                        new Uri("DiseaseListPage?Title=性接觸或血液傳染&MenuType=傳染病介紹&ListType=性接觸或血液傳染", UriKind.Relative));
                })
            });

            this.DiseaseMenuItems.Add(new MainMenuItem()
            {
                Text = "接觸傳染",
                //Icon = Device.OnPlatform("menu_rss.png", "menu_rss.png", "Assets/menu_rss.png"),
                Command = new DelegateCommand(async () =>
                {
                    await this._navigationService.NavigateAsync(
                        new Uri("DiseaseListPage?Title=接觸傳染&MenuType=傳染病介紹&ListType=接觸傳染", UriKind.Relative));
                })
            });

            this.DiseaseMenuItems.Add(new MainMenuItem()
            {
                Text = "其他類",
                //Icon = Device.OnPlatform("menu_rss.png", "menu_rss.png", "Assets/menu_rss.png"),
                Command = new DelegateCommand(async () =>
                {
                    await this._navigationService.NavigateAsync(
                        new Uri("DiseaseListPage?Title=其他類&MenuType=傳染病介紹&ListType=其他類", UriKind.Relative));
                })
            });
        }

        #endregion

        #region 疫苗接種

        /// <summary>
        /// 疫苗接種
        /// </summary>
        public List<MainMenuItem> VaccineMenuItems { get; set; } = new List<MainMenuItem>();

        /// <summary>
        /// 疫苗接種
        /// </summary>
        private void BuildVaccineMenu()
        {
            this.VaccineMenuItems.Add(new MainMenuItem()
            {
                Text = "幼兒疫苗接種",
                Icon = Device.OnPlatform("menu_baby.png", "menu_baby.png", "Assets/menu_baby.png"),
                BackgroundColor = Color.FromRgb(209, 201, 223),
                Command = new DelegateCommand(async () =>
                {
                    await this._navigationService.NavigateAsync(
                        new Uri("ChildVaccinePage?Title=幼兒疫苗接種", UriKind.Relative));
                })
            });

            this.VaccineMenuItems.Add(new MainMenuItem()
            {
                Text = "成人預防接種",
                Icon = Device.OnPlatform("menu_user.png", "menu_user.png", "Assets/menu_user.png"),
                BackgroundColor = Color.FromRgb(226, 191, 212),
                Command = new DelegateCommand(async () =>
                {
                    await this._navigationService.NavigateAsync(
                        new Uri("AdultVaccinePage?Title=成人預防接種", UriKind.Relative));
                })
            });

            this.VaccineMenuItems.Add(new MainMenuItem()
            {
                Text = "公費流感疫苗合約院所查詢",
                Icon = Device.OnPlatform("menu_safari.png", "menu_chrome.png", "Assets/menu_edge.png"),
                BackgroundColor = Color.FromRgb(66, 147, 195),
                Command = new DelegateCommand(async () =>
                {
                    await CrossShare.Current.OpenBrowser(@"https://antiflu.cdc.gov.tw/");
                })
            });

            this.VaccineMenuItems.Add(new MainMenuItem()
            {
                Text = "預防接種單位查詢",
                Icon = Device.OnPlatform("menu_hospital.png", "menu_hospital.png", "Assets/menu_hospital.png"),
                BackgroundColor = Color.FromRgb(154, 168, 187),
                Command = new DelegateCommand(async () =>
                {
                    await this._navigationService.NavigateAsync(
                        new Uri("VaccineHospitalListPage?Title=預防接種單位縣市&MenuType=預防接種單位縣市", UriKind.Relative));
                })
            });
        }

        #endregion

        #region 抗蛇毒血清

        /// <summary>
        /// 抗蛇毒血清
        /// </summary>
        public List<MainMenuItem> SerumMenuItems { get; set; } = new List<MainMenuItem>();

        /// <summary>
        /// 抗蛇毒血清
        /// </summary>
        private void BuildSerumMenu()
        {
            this.SerumMenuItems.Add(new MainMenuItem()
            {
                Text = "抗蛇毒血清儲備點查詢",
                Icon = Device.OnPlatform("menu_injection2.png", "menu_injection2.png", "Assets/menu_injection2.png"),
                BackgroundColor = Color.FromRgb(153, 153, 176),
                Command = new DelegateCommand(async () =>
                {
                    await this._navigationService.NavigateAsync(
                        new Uri("SerumHospitalListPage?Title=抗蛇毒血清儲備點查詢&MenuType=血清儲備點縣市", UriKind.Relative));
                })
            });

            this.SerumMenuItems.Add(new MainMenuItem()
            {
                Text = "台灣六大毒蛇",
                Icon = Device.OnPlatform("menu_safari.png", "menu_chrome.png", "Assets/menu_edge.png"),
                BackgroundColor = Color.FromRgb(66, 147, 195),
                Command = new DelegateCommand(async () =>
                {
                    await CrossShare.Current.OpenBrowser(@"https://zh.m.wikipedia.org/wiki/%E5%8F%B0%E7%81%A3%E5%85%AD%E5%A4%A7%E6%AF%92%E8%9B%87");
                })
            });

            this.SerumMenuItems.Add(new MainMenuItem()
            {
                Text = "毒蛇咬傷緊急處置要點",
                Icon = Device.OnPlatform("menu_safari.png", "menu_chrome.png", "Assets/menu_edge.png"),
                BackgroundColor = Color.FromRgb(66, 147, 195),
                Command = new DelegateCommand(async () =>
                {
                    await CrossShare.Current.OpenBrowser(@"http://www.pcc-vghtpe.tw/antidote/snake02.htm");
                })
            });
        }

        #endregion

        #region 防疫專區

        /// <summary>
        /// 防疫專區
        /// </summary>
        public List<MainMenuItem> CDCAreaMenuItems { get; set; } = new List<MainMenuItem>();

        /// <summary>
        /// 防疫專區
        /// </summary>
        private void BuildCDCAreaMenu()
        {
            this.CDCAreaMenuItems.Add(new MainMenuItem()
            {
                Text = "流感專區",
                Icon = Device.OnPlatform("menu_safari.png", "menu_chrome.png", "Assets/menu_edge.png"),
                BackgroundColor = Color.FromRgb(66, 147, 195),
                Command = new DelegateCommand(async () =>
                {
                    await CrossShare.Current.OpenBrowser(@"http://www.cdc.gov.tw/Professional/DiseasePrologue.aspx?did=680&lid=960C589FBCC5C017&treeid=8208EB95DDA7842A&nowtreeid=8208EB95DDA7842A");
                })
            });

            this.CDCAreaMenuItems.Add(new MainMenuItem()
            {
                Text = "登革熱防治網",
                Icon = Device.OnPlatform("menu_safari.png", "menu_chrome.png", "Assets/menu_edge.png"),
                BackgroundColor = Color.FromRgb(66, 147, 195),
                Command = new DelegateCommand(async () =>
                {
                    await CrossShare.Current.OpenBrowser(@"http://www.dengue.gov.tw/");
                })
            });

            this.CDCAreaMenuItems.Add(new MainMenuItem()
            {
                Text = "茲卡病毒感染症專區",
                Icon = Device.OnPlatform("menu_safari.png", "menu_chrome.png", "Assets/menu_edge.png"),
                BackgroundColor = Color.FromRgb(66, 147, 195),
                Command = new DelegateCommand(async () =>
                {
                    await CrossShare.Current.OpenBrowser(@"http://www.cdc.gov.tw/Professional/DiseasePrologue.aspx?did=744&lid=64B7C4FC7D164D4D&treeid=53FDE358DA8186DD&nowtreeid=53FDE358DA8186DD");
                })
            });

            this.CDCAreaMenuItems.Add(new MainMenuItem()
            {
                Text = "疾管署諮詢專線",
                Icon = Device.OnPlatform("menu_telephone.png", "menu_telephone.png", "Assets/menu_telephone.png"),
                BackgroundColor = Color.FromRgb(51, 154, 106),
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
        }

        #endregion
    }
}
