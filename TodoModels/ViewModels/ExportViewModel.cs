using System.Collections.ObjectModel;
using TodoModels.Models;
using TodoModels.MVVMHelper;
using System.Linq;

namespace TodoModels.ViewModels
{
    public class ExportViewModel : ModelBase, IViewModel
    {
        public DelegateCommand LoadSharedFileCommand { get; set; }
        public DelegateCommand SaveAsSharedFileCommand { get; set; }
        public DelegateCommand ExportCommand { get; set; }
        public DelegateCommand ImportCommand { get; set; }

        private ObservableCollection<string> _sharedFileNames;
        public ObservableCollection<string> SharedFileNames
        {
            get { return _sharedFileNames; }
            set { SetValue(ref _sharedFileNames, value); }
        }

        private string _currentFile;
        public string CurrentFile
        {
            get { return _currentFile; }
            set
            {
                SetValue(ref _currentFile, value);
                GUIServices.StorageService.SafeLastFileName(value);
            }
        }

        private string _currentImportedFile;
        public string CurrentImportedFile
        {
            get { return _currentImportedFile; }
            set { SetValue(ref _currentImportedFile, value); }
        }


        private string _chosenFileName;
        public string ChosenFileName
        {
            get { return _chosenFileName; }
            set
            {
                SetValue(ref _chosenFileName, value);
                SaveAsSharedFileCommand.OnCanExecuteChanged();
            }
        }


        public int CurrentFileTodoItemsCount => TodoListManager.TodoItems?.Count ?? 0;

        public ExportViewModel()
        {
            LoadSharedFileCommand = new DelegateCommand(p => LoadSharedFile(p.ToString()));
            SaveAsSharedFileCommand = new DelegateCommand(p =>
            {
                if (p == null)
                    SaveSharedFile();
                else
                    SaveSharedFile(p.ToString());
            },
            p =>
            CurrentFileTodoItemsCount > 0 && (!string.IsNullOrEmpty(ChosenFileName) || p != null));

            ExportCommand = new DelegateCommand(p => ExportTodoItems(), p => CurrentFileTodoItemsCount > 0);
            ImportCommand = new DelegateCommand(p => ImportTodos());

            if(GUIServices.StorageService != null)
                PrepareFileNameList();
        }

        public async void PrepareFileNameList()
        {
            SharedFileNames = new ObservableCollection<string>(await GUIServices.StorageService.GetSharedFileNames());
            CurrentFile = await GUIServices.StorageService.GetLastFileName();
            CurrentImportedFile = CurrentFile;
        }

        public async void LoadSharedFile(string filename)
        {
            var todoItems = await GUIServices.StorageService.LoadSharedTodoItems(filename);
            if (todoItems.Count <= 0)
                return;
            TodoListManager.RefreshTodoItems(todoItems);
            GUIServices.MessageService.ShowMessage("Todo Items wurden geladen!");
            CurrentFile = filename;
            OnPropertyChanged(nameof(CurrentFileTodoItemsCount));
            SaveAsSharedFileCommand.OnCanExecuteChanged();
            ExportCommand.OnCanExecuteChanged();
        }

        public void SaveSharedFile(string filename = null)
        {
            filename = filename == null ? ChosenFileName : filename;
            GUIServices.StorageService.SaveSharedTodoItems(filename, TodoListManager.TodoItems.ToList());
            if (!SharedFileNames.Contains(filename))
            {
                SharedFileNames.Add(filename);
            }
            CurrentFile = filename;
            GUIServices.MessageService.ShowMessage("Todo-Liste wurde gespeichert!");
        }

        public async void ExportTodoItems()
        {
            CurrentImportedFile = await GUIServices.StorageService.ExportTodoItems(TodoListManager.TodoItems.ToList());
        }

        public async void ImportTodos()
        {
            var todos = await GUIServices.StorageService.ImportTodoItems();
            if (todos.Item1 != null && todos.Item1.Count > 0)
            {
                CurrentImportedFile = todos.Item2;
                TodoListManager.RefreshTodoItems(todos.Item1);
                GUIServices.MessageService.ShowMessage("Todo Items wurden geladen!");
                await GUIServices.StorageService.SaveTodosToLastFile(TodoListManager.TodoItems.ToList());
                OnPropertyChanged(nameof(CurrentFileTodoItemsCount));
                ExportCommand.OnCanExecuteChanged();
                SaveAsSharedFileCommand.OnCanExecuteChanged();
            }
        }
    }
}