using System;
using Prism.Unity;
using DiseasePrevention.Views;
using DiseasePrevention.Views.News;
using DiseasePrevention.Views.Travels;
using Microsoft.Practices.Unity;

namespace DiseasePrevention
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer = null) : base(initializer) { }

        protected override void OnInitialized()
        {
            InitializeComponent();

            NavigationService.NavigateAsync(
                "Xf:///MainMasterDetailPage/MainNavigationPage/MainPage?Title=Hello");
        }

        protected override void RegisterTypes()
        {
            var httpClient = new System.Net.Http.HttpClient();
            Container.RegisterInstance(typeof(System.Net.Http.HttpClient), null, httpClient,
                new ContainerControlledLifetimeManager());

            Container.RegisterTypeForNavigation<MainPage>();
            Container.RegisterTypeForNavigation<MainMasterDetailPage>();
            Container.RegisterTypeForNavigation<MainNavigationPage>();

            Container.RegisterTypeForNavigation<AboutPage>();

            #region News

            // Prism for XF 6.3 才會修正子頁面的 Navigation
            // https://github.com/PrismLibrary/Prism/issues/650
            // Container.RegisterTypeForNavigation<NewsTabbedPage>();

            //Container.RegisterTypeForNavigation<NewsListPage>("NormalNewsPage");
            //Container.RegisterTypeForNavigation<NewsListPage>("ProfessionalNewsPage");
            //Container.RegisterTypeForNavigation<NewsListPage>("MedicalNewsPage");
            Container.RegisterTypeForNavigation<NewsPage>();
            Container.RegisterTypeForNavigation<NewsListPage>();
            Container.RegisterTypeForNavigation<NewsDetailPage>();

            #endregion

            Container.RegisterTypeForNavigation<TravelPage>();
            Container.RegisterTypeForNavigation<TravelListPage>();
            Container.RegisterTypeForNavigation<TravelDetailPage>();
        }
    }
}
