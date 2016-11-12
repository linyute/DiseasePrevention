using System.Reflection;
using Prism.Mvvm;
using Xamarin.Forms;

namespace DiseasePrevention.Views
{
    public partial class MainNavigationPage : NavigationPage
    {
        public MainNavigationPage()
        {
            InitializeComponent();

            // UWP 無法 Binding Title
            if (Device.OS == TargetPlatform.Windows)
            {
                this.Pushed += MainNavigationPage_Pushed;
            }
        }

        private void MainNavigationPage_Pushed(object sender, NavigationEventArgs e)
        {
            var vm = (BindableBase)this.CurrentPage.BindingContext;

            vm.PropertyChanged += (vmSender, args) =>
            {
                if (args.PropertyName == "Title")
                {
                    var prop = vmSender.GetType().GetRuntimeProperty("Title");
                    var title = (string)prop.GetValue(vmSender);
                    
                    this.Title = title; // UriKind.Relative
                    this.CurrentPage.Title = title; // UriKind.Absolute
                }
            };
        }
    }
}
