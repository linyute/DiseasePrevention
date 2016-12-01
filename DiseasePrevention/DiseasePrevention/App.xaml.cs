using System;
using Prism.Unity;
using DiseasePrevention.Views;
using DiseasePrevention.Views.News;
using DiseasePrevention.Views.Serums;
using DiseasePrevention.Views.Travels;
using DiseasePrevention.Views.Vaccines;
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
                "xf:///MainMasterDetailPage/MainNavigationPage/MainPage?Title=您好&MenuType=首頁");
        }

        protected override void RegisterTypes()
        {
            var httpClient = new System.Net.Http.HttpClient();
            Container.RegisterInstance(typeof(System.Net.Http.HttpClient), null, httpClient,
                new ContainerControlledLifetimeManager());

            
            Container.RegisterTypeForNavigation<MainMasterDetailPage>();
            Container.RegisterTypeForNavigation<MainNavigationPage>();

            Container.RegisterTypeForNavigation<MainPage>();

            Container.RegisterTypeForNavigation<AboutPage>();

            #region 最新消息

            Container.RegisterTypeForNavigation<NewsListPage>();
            Container.RegisterTypeForNavigation<DiseaseListPage>();
            Container.RegisterTypeForNavigation<NewsDetailPage>();

            #endregion

            #region 國際疫情

            Container.RegisterTypeForNavigation<TravelListPage>();
            Container.RegisterTypeForNavigation<TravelDetailPage>();

            #endregion

            #region 疫苗接種

            Container.RegisterTypeForNavigation<ChildVaccinePage>();
            Container.RegisterTypeForNavigation<AdultVaccinePage>();

            Container.RegisterTypeForNavigation<VaccineHospitalListPage>();
            Container.RegisterTypeForNavigation<VaccineHospitalDetailPage>();

            #endregion

            #region 抗蛇毒血清

            Container.RegisterTypeForNavigation<SerumHospitalListPage>();
            Container.RegisterTypeForNavigation<SerumHospitalDetailPage>();

            #endregion


        }
    }
}
