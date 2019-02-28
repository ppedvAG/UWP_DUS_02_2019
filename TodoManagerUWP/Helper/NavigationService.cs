using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoModels.MVVMHelper;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace TodoManagerUWP.Helper
{
    public class NavigationService : INavigationService
    {
        public Frame MainFrame { get; set; }

        public void GoToView(Views view, IViewModel viewModel)
        {
            switch (view)
            {
                case Views.Main:
                    (Window.Current.Content as Frame)?.Navigate(typeof(MainPage), viewModel);
                    break;
                case Views.Todos:
                    MainFrame?.Navigate(typeof(TodosPage), viewModel);
                    break;
                case Views.Export:
                    MainFrame?.Navigate(typeof(ExportPage), viewModel);
                    break;
                default:
                    break;
            }
        }
    }
}
