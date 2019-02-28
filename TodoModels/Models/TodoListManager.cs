using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using TodoModels.MVVMHelper;

namespace TodoModels.Models
{
    public class TodoListManager : ModelBase
    {
        public static ObservableCollection<TodoItem> TodoItems { get; set; }

        static TodoListManager()
        {
            TodoItems = new ObservableCollection<TodoItem>();
        }

    }
}
