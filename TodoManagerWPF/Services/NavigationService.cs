using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TodoManagerWPF.Views;
using TodoModels.MVVMHelper;

namespace TodoManagerWPF.Services
{
    public class NavigationService : INavigationService
    {
       
        public TabItem TodoFrame { get; set; }
        public TabItem ExportFrame { get; set; }

        public NavigationService(TabItem todoFrame, TabItem exportFrame)
        {
            TodoFrame = todoFrame;
            ExportFrame = exportFrame;
        }

        public void GoToView(TodoModels.MVVMHelper.Views view, IViewModel viewModel)
        {
            switch (view)
            {
                case TodoModels.MVVMHelper.Views.Todos:
                    TodosPage todoPage = new TodosPage();
                    todoPage.DataContext = viewModel;
                    ((Frame)TodoFrame.Content).Navigate(todoPage);
                    ((TabControl)TodoFrame.Parent).SelectedItem = TodoFrame;
                    break;
                case TodoModels.MVVMHelper.Views.Export:
                    ExportPage exportPage = new ExportPage();
                    exportPage.DataContext = viewModel;
                    ((Frame)ExportFrame.Content).Navigate(exportPage);
                    ((TabControl)ExportFrame.Parent).SelectedItem = ExportFrame;
                    break;
            }
        }
    }
}
