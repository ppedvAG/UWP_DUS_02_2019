using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace GutesWetter
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public ObservableCollection<WetterInfo> Cities { get; set; } = new ObservableCollection<WetterInfo>();

        public MainPage()
        {
            this.InitializeComponent();
        }

        private void Add_City_Button_Click(object sender, RoutedEventArgs e)
        {
            WetterInfo info = new WetterInfo(textbox.Text);
            Cities.Add(info);

            WetterAPIService.ErmittleWetter(info);
        }

        private void Delete_City_Click(object sender, RoutedEventArgs e)
        {
            if(sender is Button button && button.DataContext is WetterInfo info)
            {
                Cities.Remove(info);
            }
        }
    }
}