using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TodoModels.Models;
using TodoModels.MVVMHelper;

namespace TodoModels.ViewModels
{
    public class MainViewModel : ModelBase, IViewModel
    {
        public DelegateCommand GoToTodosCommand { get; set; }
        public DelegateCommand GotoExportCommand { get; set; }
        public DelegateCommand SaveTodosCommand { get; set; }

        public MainViewModel()
        {
            GoToTodosCommand = new DelegateCommand(p => GoToTodosView());
            GotoExportCommand = new DelegateCommand(p => GoToExportView());
            SaveTodosCommand = new DelegateCommand(_ => GUIServices.StorageService.SaveTodosToLastFile(TodoListManager.TodoItems?.ToList()));
        }

        public void GoToTodosView()
        {
            GUIServices.NavigationService?.GoToView(Views.Todos, new TodosViewModel(TodoListManager.TodoItems));
        }

        public void GoToExportView()
        {
            GUIServices.NavigationService?.GoToView(Views.Export, new ExportViewModel());
        }
    }
}