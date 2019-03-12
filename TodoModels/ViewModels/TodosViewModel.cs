using System;
using System.Collections.ObjectModel;
using System.Linq;
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
            set
            {
                SetValue(ref _selectedTodoItem, value);
                //Speichere die Todos bei jeder Änderung
                GUIServices.StorageService.SaveTodosToLastFile(TodoListManager.TodoItems?.ToList());
            }
        }

        public DelegateCommand CreateNewTodoCommand { get; set; }
        public DelegateCommand DeleteAllCommand { get; set; }

        public TodosViewModel(ObservableCollection<TodoItem> todoItems)
        {
            TodoItems = todoItems;
            CreateNewTodoCommand = new DelegateCommand(_ => CreateNewTodoItem());
            DeleteAllCommand = new DelegateCommand(_ =>
            {
                TodoListManager.TodoItems.Clear();
                GUIServices.StorageService.SaveTodosToLastFile(TodoListManager.TodoItems.ToList());
                GUIServices.NotificationService.RemoveAllNotifications();
            });
            

            foreach (var item in todoItems)
            {
                item.PropertyChanged += Item_PropertyChanged;
            }
        }

        public TodosViewModel()
        {

        }

        public TodosViewModel(ObservableCollection<TodoItem> todoItems, TodoItem selectedDate) : this(todoItems)
        {
            SelectedTodoItem = selectedDate;
        }

        private void Item_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (sender is TodoItem todo)
            {
                if (e.PropertyName == nameof(TodoItem.DueDate))
                {
                    if (todo.DueDate == null)
                    {
                        //Notification entfernen
                        GUIServices.NotificationService?.RemoveNotification(todo);
                    }
                    else
                    {
                        if (todo.DueDate > DateTimeOffset.Now && !todo.Done)
                        {
                            GUIServices.NotificationService?.AddNotification(todo);
                        }
                    }
                }
                else if (e.PropertyName == nameof(TodoItem.Done))
                {
                    if (todo.Done == true)
                    {
                        todo.DueDate = DateTime.Now.AddDays(7);
                    }
                }
            }
        }

        public void CreateNewTodoItem()
        {
            TodoItem newTodo = new TodoItem("Todo ohne Titel", "...");
            TodoItems.Add(newTodo);

            newTodo.PropertyChanged += Item_PropertyChanged;

            SelectedTodoItem = newTodo;
        }
    }
}