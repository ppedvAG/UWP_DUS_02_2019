using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace HelloWorld
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

        private EventHandler _myEvent;
        public event EventHandler MyEvent
        {
            add
            {
                _myEvent += value;
            }
            remove
            {
                _myEvent -= value;
            }
        }

        bool _secondButtonCreated = false;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (_secondButtonCreated)
                return;

            Button newButton = new Button();
            newButton.HorizontalAlignment = HorizontalAlignment.Left;
            newButton.VerticalAlignment = VerticalAlignment.Center;
            newButton.Content = "Klick mich auch!";
            newButton.Click += NewButton_ClickAsync;

            mainGrid.Children.Add(newButton);
            _secondButtonCreated = true;
            
        }

        private async void NewButton_ClickAsync(object sender, RoutedEventArgs e)
        {
            await new MessageDialog("Hello World!").ShowAsync();
            this.Background = new SolidColorBrush(Colors.Aqua);
            this.Frame.Navigate(typeof(MainPage));
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            foreach (var item in mainGrid.Children)
            {
                if(item is Button button)
                {
                    button.Click -= NewButton_ClickAsync;
                }
            }
        }
    }
}
