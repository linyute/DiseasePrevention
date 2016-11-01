using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiseasePrevention.Models;
using Xamarin.Forms;

namespace DiseasePrevention.Services
{
    public class MenuItemService
    {
        public MenuItemService()
        {
            BuildMainMenu();
        }

        public List<MasterMenuItem> MainMenuItems { get; set; }
            = new List<MasterMenuItem>();

        private void BuildMainMenu()
        {
            MainMenuItems.Add(new MasterMenuItem()
            {
                Title = "首頁",
                Icon = Device.OnPlatform("menu_home.png", "menu_home.png", "Assets/menu_home.png"),
                Uri = new Uri("xf:///MainMasterDetailPage/MainNavigationPage/MainPage", UriKind.Absolute)
            });

            MainMenuItems.Add(new MasterMenuItem()
            {
                Title = "關於本程式",
                Icon = Device.OnPlatform("menu_info.png", "menu_info.png", "Assets/menu_info.png"),
                Uri = new Uri("xf:///MainMasterDetailPage/MainNavigationPage/AboutPage", UriKind.Absolute)
            });
        }
    }
}
