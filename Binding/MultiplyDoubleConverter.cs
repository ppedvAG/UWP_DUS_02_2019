using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace Binding
{
    public class MultiplyDoubleConverter : IValueConverter
    {
        //Source -> Target
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if(double.TryParse(value.ToString(), out double dvalue))
            {
                if(parameter != null && double.TryParse(parameter.ToString(), out double multiplicator))
                {
                    return dvalue * multiplicator;
                }
                return dvalue * 2;
            }
            return value;
        }

        //Target -> Source
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (double.TryParse(value.ToString(), out double dvalue))
            {
                if (parameter != null && double.TryParse(parameter.ToString(), out double multiplicator))
                {
                    return dvalue / multiplicator;
                }
                return dvalue / 2;
            }
            return value;
        }
    }
}
