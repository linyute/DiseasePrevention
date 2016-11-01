using Prism.Unity;
using DiseasePrevention.Views;

namespace DiseasePrevention
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer = null) : base(initializer) { }

        protected override void OnInitialized()
        {
            InitializeComponent();

            NavigationService.NavigateAsync(
                "Xf:///MainMasterDetailPage/MainNavigationPage/MainPage?title=Hello%20from%20Xamarin.Forms");
        }

        protected override void RegisterTypes()
        {
            Container.RegisterTypeForNavigation<MainPage>();
            Container.RegisterTypeForNavigation<MainMasterDetailPage>();
            Container.RegisterTypeForNavigation<MainNavigationPage>();

            Container.RegisterTypeForNavigation<AboutPage>();
        }
    }
}
