using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DiseasePrevention.Converters
{
    public class StringToHtmlConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var html = new HtmlWebViewSource();

            var content = value as string;
            if (content != null)
            {
                html.Html = content;
            }
            else
            {
                html.Html = string.Empty;
            }

            return html;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
