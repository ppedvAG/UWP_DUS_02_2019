﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace WhiskeyManager
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            SystemNavigationManager.GetForCurrentView().BackRequested += MainPage_BackRequested;

            subFrame.Navigate<ListPage>();
            subFrame.Navigated += SubFrame_Navigated;
        }

        private void SubFrame_Navigated(object sender, NavigationEventArgs e)
        {
            if(subFrame.CanGoBack)
            {
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            }
            else
            {
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
            }

        }

        private void MainPage_BackRequested(object sender, BackRequestedEventArgs e)
        {
            if(subFrame.CanGoBack)
            {
                subFrame.GoBack();
                e.Handled = true;
            }

        }

        private void New_Whiskey_Click(object sender, RoutedEventArgs e)
        {
            subFrame.Navigate<DetailsPage>();
        }

        private void Cleanin_Button_Click(object sender, RoutedEventArgs e)
        {
            cleaningGrid.Visibility = Visibility.Visible;
            Task.Factory.StartNew(async () =>
            {
                for (int i = 3 - 1; i >= 0; i--)
                {
                    await this.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                    {
                        tbVerbleibend.Text = i.ToString();
                    });
                    await Task.Delay(1000);
                }
                await this.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                {
                    cleaningGrid.Visibility = Visibility.Collapsed;
                });
            });
        }

        private void Liste_Button_Click(object sender, RoutedEventArgs e)
        {
            subFrame.Navigate<ListPage>();
        }
    }
}
