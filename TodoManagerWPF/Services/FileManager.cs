using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoModels.Models;
using TodoModels.MVVMHelper;

namespace TodoManagerWPF.Services
{
    public class FileManager : IStorageService
    {
        const string Last_File_Name = "Last_File_Name.txt";
        const string Shared_Files_Path = "Shared";

        public Task<string> ExportTodoItems(List<TodoItem> items)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Todo-Dateien|*.todojson";
            dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments);
            if (dialog.ShowDialog() == true)
            {
                var json = JsonConvert.SerializeObject(TodoListManager.TodoItems.ToList());
                File.WriteAllText(dialog.FileName, json);
                SafeLastFileName(dialog.FileName);
                return Task.FromResult(dialog.FileName);
            }
            return Task.FromResult(string.Empty);
        }

        public Task<string> GetLastFileName()
        {
            if (File.Exists(Last_File_Name))
            {
                return Task.FromResult(File.ReadAllText(Last_File_Name));
            }
            return Task.FromResult(string.Empty);
        }

        public Task<List<string>> GetSharedFileNames()
        {
            return Task.FromResult(new List<string>());
        }

        public async Task<List<TodoItem>> GetTodosFromLastFile()
        {
            string lastFile = await GetLastFileName();
            if (lastFile != string.Empty)
            {
                var json = File.ReadAllText(lastFile);
                return JsonConvert.DeserializeObject<List<TodoItem>>(json);
            }
            return new List<TodoItem>();
        }

        public Task<Tuple<List<TodoItem>, string>> ImportTodoItems()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Todo-Dateien|*.todojson";
            dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments);
            if (dialog.ShowDialog() == true)
            {
                string json = File.ReadAllText(dialog.FileName);
                var todoList = JsonConvert.DeserializeObject<List<TodoItem>>(json);
                SafeLastFileName(dialog.FileName);
                return Task.FromResult(new Tuple<List<TodoItem>, string>(todoList, dialog.FileName));
            }
            return Task.FromResult(new Tuple<List<TodoItem>, string>(new List<TodoItem>(), string.Empty));
        }

        public Task<List<TodoItem>> LoadSharedTodoItems(string filename)
        {
            string json = File.ReadAllText(filename);
            var todos = JsonConvert.DeserializeObject<List<TodoItem>>(json);
            return Task.FromResult(todos);
        }

        public Task SafeLastFileName(string filename)
        {
            File.WriteAllText(Last_File_Name, filename);
            return Task.CompletedTask;
        }

        public Task SaveSharedTodoItems(string filename, List<TodoItem> items)
        {
            string json = JsonConvert.SerializeObject(items);
            File.WriteAllText(filename, json);
            return Task.CompletedTask;
        }

        public async Task SaveTodosToLastFile(List<TodoItem> todos)
        {
            string lastFile = await GetLastFileName();
            if (lastFile != string.Empty)
            {
                var json = JsonConvert.SerializeObject(todos);
                File.WriteAllText(lastFile, json);
            }
        }
    }
}
