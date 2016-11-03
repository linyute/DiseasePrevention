using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiseasePrevention.Models;
using Prism.Navigation;
using Xamarin.Forms;
using MenuItem = DiseasePrevention.Models.MenuItem;

namespace DiseasePrevention.Services
{
    public class MenuItemService
    {
        public MenuItemService(INavigationService navigationService)
        {
            this._navigationService = navigationService;

            BuildMainMenu();

            BuildNewsMenu();
        }

        private readonly INavigationService _navigationService;

        #region 主要選單

        public List<MenuItem> MainMenuItems { get; set; } = new List<MenuItem>();

        private void BuildMainMenu()
        {
            MainMenuItems.Add(new MenuItem()
            {
                Title = "首頁",
                Icon = Device.OnPlatform("menu_home.png", "menu_home.png", "Assets/menu_home.png"),
                ActionAsync = async () =>
                {
                    await this._navigationService.NavigateAsync(
                        new Uri("xf:///MainMasterDetailPage/MainNavigationPage/MainPage?", UriKind.Absolute));
                }
            });

            MainMenuItems.Add(new MenuItem()
            {
                Title = "最新消息",
                Icon = Device.OnPlatform("menu_rss.png", "menu_rss.png", "Assets/menu_rss.png"),
                ActionAsync = async () =>
                {
                    await this._navigationService.NavigateAsync(
                        new Uri("xf:///MainMasterDetailPage/MainNavigationPage/NewsPage?", UriKind.Absolute));
                }
            });

            MainMenuItems.Add(new MenuItem()
            {
                Title = "關於本程式",
                Icon = Device.OnPlatform("menu_info.png", "menu_info.png", "Assets/menu_info.png"),
                ActionAsync = async () =>
                {
                    await this._navigationService.NavigateAsync(
                        new Uri("xf:///MainMasterDetailPage/MainNavigationPage/AboutPage?", UriKind.Absolute));
                }
            });
        }

        #endregion

        #region 最新消息

        public List<MenuItem> NewsMenuItems { get; set; } = new List<MenuItem>();

        private void BuildNewsMenu()
        {
            NewsMenuItems.Add(new MenuItem()
            {
                Title = "一般民眾版",
                Icon = Device.OnPlatform("menu_rss.png", "menu_rss.png", "Assets/menu_rss.png"),
                ActionAsync = async () =>
                {
                    await this._navigationService.NavigateAsync(
                        new Uri("NewsListPage?Title=一般民眾版&NewsType=一般民眾版", UriKind.Relative));
                }
            });

            NewsMenuItems.Add(new MenuItem()
            {
                Title = "專業人士版",
                Icon = Device.OnPlatform("menu_rss.png", "menu_rss.png", "Assets/menu_rss.png"),
                ActionAsync = async () =>
                {
                    await this._navigationService.NavigateAsync(
                        new Uri("NewsListPage?Title=專業人士版&NewsType=專業人士版", UriKind.Relative));
                }
            });

            NewsMenuItems.Add(new MenuItem()
            {
                Title = "致醫界通函",
                Icon = Device.OnPlatform("menu_rss.png", "menu_rss.png", "Assets/menu_rss.png"),
                ActionAsync = async () =>
                {
                    await this._navigationService.NavigateAsync(
                        new Uri("NewsListPage?Title=致醫界通函&NewsType=致醫界通函", UriKind.Relative));
                }
            });
        }

        #endregion
    }
}
