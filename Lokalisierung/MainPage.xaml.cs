using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Lokalisierung
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void Change_Language(object sender, RoutedEventArgs e)
        {
            //TODO: In dritte Methode auslagern

            if (sender is MenuFlyoutItem item)
            {
                string filename = item.Tag.ToString();

                //https://docs.microsoft.com/de-de/windows/uwp/app-resources/uri-schemes

                var dictionary = Application.Current.Resources.MergedDictionaries.SingleOrDefault(d => LocalizationHelper.GetDictionaryType(d) == LocalizationHelper.ResourceType.Language);

                if (dictionary == null)
                    return;

                dictionary.Source = new Uri($"ms-appx:///Sprachen/{filename}");
                
                this.Frame.Navigate(typeof(MainPage));
            }
        }

        private void Change_Style(object sender, RoutedEventArgs e)
        {
            if (sender is MenuFlyoutItem item)
            {
                string filename = item.Tag.ToString();

                //https://docs.microsoft.com/de-de/windows/uwp/app-resources/uri-schemes
                var dictionary = Application.Current.Resources.MergedDictionaries.SingleOrDefault(d => LocalizationHelper.GetDictionaryType(d) == LocalizationHelper.ResourceType.Style);

                if (dictionary == null)
                    return;

                dictionary.Source = new Uri($"ms-appx:///Styles/{filename}");
                this.Frame.Navigate(typeof(MainPage));
            }
        }
    }
}
