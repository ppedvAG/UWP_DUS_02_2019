using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace Lokalisierung
{
    public class LocalizationHelper
    {

        public enum ResourceType { Language, Style, None }



        public static ResourceType GetDictionaryType(DependencyObject obj)
        {
            return (ResourceType)obj.GetValue(DictionaryTypeProperty);
        }

        public static void SetDictionaryType(DependencyObject obj, ResourceType value)
        {
            obj.SetValue(DictionaryTypeProperty, value);
        }

        // Using a DependencyProperty as the backing store for DictionaryType.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DictionaryTypeProperty =
            DependencyProperty.RegisterAttached("DictionaryType", typeof(ResourceType), typeof(LocalizationHelper), new PropertyMetadata(ResourceType.None));

    }
}
