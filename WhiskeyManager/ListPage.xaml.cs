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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace WhiskeyManager
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ListPage : Page
    {
        public ListPage()
        {
            this.InitializeComponent();
            listboxWhiskeys.ItemsSource = WhiskeyList.Whiskeys;
        }

        private void Whiskey_Edit_Click(object sender, RoutedEventArgs e)
        {
            if(sender is Button button && button.DataContext is Whiskey whiskey)

            this.Frame.Navigate(typeof(DetailsPage), whiskey);
        }

        private void ListboxWhiskeys_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(DetailsPage), listboxWhiskeys.SelectedItem);
        }
    }
}
