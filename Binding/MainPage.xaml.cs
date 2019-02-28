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

namespace Binding
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            this.DataContext = woerter;
        }

        List<string> woerter { get; set; } = new List<string>() { "Wort1", "Wort2", "Wort3" };

        //innerhalb einer Page darf mit x:Bind nicht auf ein Array gebunden werden!
        List<string> andereWoerter = new List<string>() { "Wort1", "Wort2", "Wort3" };

        private void Textbox_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            if(sender is TextBox textbox && e.Key == Windows.System.VirtualKey.Enter)
            {
                textbox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            }
        }
    }
}
