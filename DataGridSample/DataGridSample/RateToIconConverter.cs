using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DataGridSample
{
    class RateToIconConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string path = string.Empty;
            if (value is double || value is int)
                path = ((double)value > 0) ? "up.png" : "down.png";

            else if (value is string)
                path = value.ToString().StartsWith("-")? "down.png" : "up.png";

            return new FileImageSource() { File = path};
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
