using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using TodoModels.MVVMHelper;
using System.Linq;
using System.Threading.Tasks;

namespace TodoModels.Models
{
    public class TodoListManager : ModelBase
    {

        public static ObservableCollection<TodoItem> TodoItems { get; private set; }

        public async static Task LoadTodoItems()
        {
            if (TodoItems != null)
                return;

            var todos = await GUIServices.StorageService.GetTodosFromLastFile();
            if (todos != null)
            {
                TodoItems = new ObservableCollection<TodoItem>(todos);
            }
            else
            {
                TodoItems = new ObservableCollection<TodoItem>();
            }
        }

        public static void RefreshTodoItems(IEnumerable<TodoItem> todoItems)
        {
            TodoItems.Clear();
            todoItems.ToList().ForEach(item => TodoItems.Add(item));
        }
    }
}
