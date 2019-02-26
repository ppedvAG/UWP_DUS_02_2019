using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace WhiskeyManager
{
    public static class Erweiterungsmethoden
    {
        public static void Navigate<T>(this Frame frame) where T : Page
        {
            frame.Navigate(typeof(T));
        }
    }
}
