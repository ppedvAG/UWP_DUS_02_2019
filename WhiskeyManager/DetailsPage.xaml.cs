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
    public sealed partial class DetailsPage : Page
    {
        Whiskey _whiskeyToEdit = null;

        public DetailsPage()
        {
            this.InitializeComponent();
            cbTyp.ItemsSource =  Enum.GetValues(typeof(Whiskey.WhiskeyType));
            cbTyp.SelectedIndex = 0;
        }

        private void Create_Button_Click(object sender, RoutedEventArgs e)
        {
            if(_whiskeyToEdit == null)
            {
                string name = tbName.Text;
                bool leer = cbLeer.IsChecked.Value;
                Whiskey.WhiskeyType type = (Whiskey.WhiskeyType)cbTyp.SelectedItem;
                int year = dpJahrgang.Date.Year;

                Whiskey newWhiskey = new Whiskey(name, year, leer, type);
                WhiskeyList.Whiskeys.Add(newWhiskey);
            }
            else
            {
                _whiskeyToEdit.Name = tbName.Text;
                _whiskeyToEdit.Leer = cbLeer.IsChecked.Value;
                _whiskeyToEdit.Type = (Whiskey.WhiskeyType)cbTyp.SelectedItem;
                _whiskeyToEdit.Jahrgang = dpJahrgang.Date.Year;
            }


            this.Frame.GoBack();
        }

        private void Abort_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.GoBack();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if(e.Parameter is Whiskey whiskey)
            {
                _whiskeyToEdit = whiskey;

                tbName.Text = whiskey.Name;
                cbLeer.IsChecked = whiskey.Leer;
                cbTyp.SelectedItem = whiskey.Type;
                dpJahrgang.Date = new DateTime(whiskey.Jahrgang,1,1);

                btnErstellen.Content = "Speichernajsidjasijdias";
            }
            base.OnNavigatedTo(e);
        }
    }
}
