using System.Collections.ObjectModel;
using TodoModels.Models;
using TodoModels.MVVMHelper;

namespace TodoModels.ViewModels
{
    public class TodosViewModel : ModelBase, IViewModel
    {
        private ObservableCollection<TodoItem> _todoItems;
        public ObservableCollection<TodoItem> TodoItems
        {
            get { return _todoItems; }
            set { SetValue(ref _todoItems, value); }
        }

        private TodoItem _selectedTodoItem;
        public TodoItem SelectedTodoItem
        {
            get { return _selectedTodoItem; }
            set { SetValue(ref _selectedTodoItem, value); }
        }

        public DelegateCommand CreateNewTodoCommand { get; set; }

        public TodosViewModel(ObservableCollection<TodoItem> todoItems)
        {
            TodoItems = todoItems;
            CreateNewTodoCommand = new DelegateCommand(_ => CreateNewTodoItem());
        }

        public void CreateNewTodoItem()
        {
            TodoItem newTodo = new TodoItem("Todo ohne Titel", "...");
            TodoItems.Add(newTodo);
            SelectedTodoItem = newTodo;
        }
       
    }
}