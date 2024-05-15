using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace HRM.UI.ViewModels.Converter
{
    public class GenderToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool booleanValue && parameter is string gender)
            {
                if (gender == "Male")
                {
                    return booleanValue == true;
                }
                else if (gender == "Female")
                {
                    return booleanValue == false;
                }
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool booleanValue && parameter is string gender)
            {
                if (gender == "Male")
                {
                    return booleanValue;
                }
                else if (gender == "Female")
                {
                    return !booleanValue;
                }
            }
            return false;
        }
    }
}
