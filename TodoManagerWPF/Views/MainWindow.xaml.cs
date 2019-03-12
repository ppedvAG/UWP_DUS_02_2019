using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TodoManagerWPF.Services;
using TodoModels.Models;
using TodoModels.MVVMHelper;
using TodoModels.ViewModels;

namespace TodoManagerWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            GUIServices.NavigationService = new TodoManagerWPF.Services.NavigationService(todosTab, exportTab);
            GUIServices.StorageService = new FileManager();
            GUIServices.MessageService = new MessageService();
            GUIServices.NotificationService = new NotificationService();

            TodoListManager.LoadTodoItems();
        }

        string _previous = string.Empty;
        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(sender is TabControl control && control.SelectedItem is TabItem item && DataContext is MainViewModel model && item.Name != _previous)
            {
                _previous = item.Name;
                switch (item.Name)
                {
                    case nameof(todosTab):
                        model.GoToTodosCommand.Execute(null);
                        break;
                    case nameof(exportTab):
                        model.GotoExportCommand.Execute(null);
                        break;
                }
            }
        }
    }
}