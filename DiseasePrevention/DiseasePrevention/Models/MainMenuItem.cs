using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiseasePrevention.Models
{
    public class MainMenuItem
    {
        public string Title { get; set; }

        public string Icon { get; set; }

        public Func<Task> ActionAsync { get; set; }
    }

    /*
     ActionAsync = async () =>
                {
                    await this._navigationService.NavigateAsync(
                        new Uri("xf:///MainMasterDetailPage/MainNavigationPage/MainPage?Title=歡迎", UriKind.Absolute));
                }

     await this.SelectedMenuItem.ActionAsync.Invoke();
     */
}
