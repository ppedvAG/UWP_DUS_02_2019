using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace GutesWetter
{
    public class BooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is bool boolValue && bool.TryParse(parameter.ToString(), out bool parameterValue))
            {
                if (targetType == typeof(bool))
                {
                    return boolValue == parameterValue;
                }
                else
                {
                    return boolValue == parameterValue ? Visibility.Visible : Visibility.Collapsed;

                }
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
        }
    }
}
