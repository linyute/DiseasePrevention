using DiseasePrevention.ViewModels.News;
using Xamarin.Forms;

namespace DiseasePrevention.Views.News
{
    public partial class NewsTabbedPage : TabbedPage
    {
        public NewsTabbedPage()
        {
            InitializeComponent();

            var normalNews = this.NormalNewsPage.BindingContext as NewsListPageViewModel;
            normalNews.NewsType = "一般民眾版";

            var professionalNewsPage = this.ProfessionalNewsPage.BindingContext as NewsListPageViewModel;
            professionalNewsPage.NewsType = "專業人士版";

            var medicalNewsPage = this.MedicalNewsPage.BindingContext as NewsListPageViewModel;
            medicalNewsPage.NewsType = "致醫界通函";
        }
    }
}
