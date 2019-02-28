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

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace GutesWetter
{
    public sealed partial class WetterPresenter : UserControl
    {
        //propdp + Tab + Tab
        public WetterInfo WetterInfo
        {
            //Diesen Code auf keinen Fall ändern oder ergänzen
            get { return (WetterInfo)GetValue(WetterInfoProperty); }
            set { SetValue(WetterInfoProperty, value); }
        }

        // Using a DependencyProperty as the backing store for WetterInfo.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WetterInfoProperty =
            DependencyProperty.Register("WetterInfo", typeof(WetterInfo), typeof(WetterPresenter), new PropertyMetadata(null, WetterInfoChanged));

        private async static void WetterInfoChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is WetterPresenter presenter && e.NewValue is WetterInfo info)
            {
                await WetterAPIService.ErmittleWetter(info);
                info.PropertyChanged += Info_PropertyChanged;
            }
        }

        private async static void Info_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(GutesWetter.WetterInfo.Name) && sender is WetterInfo info)
            {
                await WetterAPIService.ErmittleWetter(info);
            }
        }

        public WetterPresenter()
        {
            this.InitializeComponent();
        }
    }
}
