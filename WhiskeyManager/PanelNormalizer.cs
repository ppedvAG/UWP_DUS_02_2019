using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace WhiskeyManager
{
    public class PanelNormalizer
    {
        //propa + Tab + Tab

        public static bool GetNormalizeWidth(DependencyObject obj)
        {
            return (bool)obj.GetValue(NormalizeWidthProperty);
            //hier keine weitere Logik einfügen!!
        }

        public static void SetNormalizeWidth(DependencyObject obj, bool value)
        {
            obj.SetValue(NormalizeWidthProperty, value);
            //hier keine weitere Logik einfügen!!
        }

        // Using a DependencyProperty as the backing store for NormalizeWidth.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NormalizeWidthProperty =
            DependencyProperty.RegisterAttached("NormalizeWidth", typeof(bool), typeof(PanelNormalizer), new PropertyMetadata(false,NormalizeWidthChanged));

        private static void NormalizeWidthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if(d is Panel panel && ((bool)e.NewValue))
            {
                panel.Loaded += Panel_LayoutUpdated;
                panel.Unloaded += Panel_Unloaded;
            }
        }

        private static void Panel_Unloaded(object sender, RoutedEventArgs e)
        {
            if(sender is Panel panel)
            {
                panel.Unloaded -= Panel_Unloaded;
                panel.Unloaded -= Panel_LayoutUpdated;
            }
        }

        private static void Panel_LayoutUpdated(object sender, RoutedEventArgs e)
        {
           
           if(sender is Panel panel)
           {
                double maxWidth = 0;
                maxWidth = panel.Children.OfType<FrameworkElement>().Max(element => element.ActualWidth);

                foreach (var item in panel.Children.OfType<FrameworkElement>())
                {
                    item.Width = maxWidth;
                }
            }
        }
    }
}
