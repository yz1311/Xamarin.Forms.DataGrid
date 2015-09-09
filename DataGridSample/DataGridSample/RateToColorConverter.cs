using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DataGridSample
{
    class RateToColorConverter:IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is double ||value is int)
                return (double)value > 0 ? Color.FromHex("#81F781") : Color.FromHex("#F78181");
            if(value is string)
                return value.ToString().StartsWith("-") ? Color.FromHex("#F78181") : Color.FromHex("#81F781");

            return Color.White;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
