using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace TodoManagerWPF.Helper
{
    public class DateTimeOffsetToDateTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is DateTimeOffset offset)
            {
                return offset.DateTime;
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is DateTime time)
            {
                return (DateTimeOffset?)time;
            }
            if(value is null)
            {
                return null;
            }
            return value;
        }
    }
}
