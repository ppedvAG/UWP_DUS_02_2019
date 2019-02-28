using System;
using System.Collections.Generic;
using System.Text;
using TodoModels.MVVMHelper;

namespace TodoModels.Models
{
    public class TodoItem : ModelBase
    {

        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetValue(ref _title, value); }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set { SetValue(ref _description, value); }
        }

        private DateTimeOffset? _dueDate;
        public DateTimeOffset? DueDate
        {
            get { return _dueDate; }
            set { SetValue(ref _dueDate, value); }
        }

        private DateTimeOffset _creationDate;
        public DateTimeOffset CreationDate
        {
            get { return _creationDate; }
            private set { SetValue(ref _creationDate, value); }
        }

        private bool _done;
        public bool Done
        {
            get { return _done; }
            set { SetValue(ref _done, value); }
        }

        public TodoItem(string title, string description, DateTimeOffset? dueDate = null, bool done = false)
        {
            Title = title;
            Description = description;
            DueDate = dueDate;
            Done = done;
            CreationDate = DateTimeOffset.Now;
        }
    }
}
