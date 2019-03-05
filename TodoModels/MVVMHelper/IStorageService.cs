using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using TodoModels.Models;

namespace TodoModels.MVVMHelper
{
    public interface IStorageService
    {
        Task SaveSharedTodoItems(string filename, List<TodoItem> items);
        Task<List<TodoItem>> LoadSharedTodoItems(string filename);
        Task SafeLastFileName(string filename);
        Task<string> GetLastFileName();
        Task<List<string>> GetSharedFileNames();
        Task<List<TodoItem>> GetTodosFromLastFile();
        Task SaveTodosToLastFile(List<TodoItem> todos);
        Task<string> ExportTodoItems(List<TodoItem> items);
        Task<Tuple<List<TodoItem>,string>> ImportTodoItems();
    }
}