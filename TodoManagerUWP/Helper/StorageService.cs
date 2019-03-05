using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoModels.Models;
using TodoModels.MVVMHelper;
using Windows.Storage;
using Windows.Storage.Pickers;

namespace TodoManagerUWP.Helper
{
    public class StorageService : IStorageService 
    {
        public const string Last_File_Name_Key = "LastFileName";
        
        #region LastFileName
        public Task<string> GetLastFileName()
        {
            if(ApplicationData.Current.LocalSettings.Values.TryGetValue(Last_File_Name_Key, out object lastFile))
            {
                return Task.FromResult(lastFile.ToString());
            }
            return Task.FromResult(string.Empty);
        }

        public Task SafeLastFileName(string filename)
        {
            ApplicationData.Current.LocalSettings.Values[Last_File_Name_Key] = filename;
            return Task.CompletedTask;
        }
        #endregion

        #region SharedFolder

        public async Task<List<TodoItem>> LoadSharedTodoItems(string filename)
        {
            try
            {
                var item = await ApplicationData.Current.SharedLocalFolder.TryGetItemAsync(filename);
                if (item is StorageFile file)
                {
                    string json = await FileIO.ReadTextAsync(file);
                    var todos = JsonConvert.DeserializeObject<List<TodoItem>>(json);
                    return todos;
                }
                else
                {
                    GUIServices.MessageService.ShowMessage("Datei wurde nicht gefunden!");
                    return new List<TodoItem>();
                }
            }
            catch (Exception exp)
            {
                GUIServices.MessageService.ShowMessage(exp.Message);
                return new List<TodoItem>();
            }
        }

        public async Task SaveSharedTodoItems(string filename, List<TodoItem> items)
        {
            try
            {
                var file = await ApplicationData.Current.SharedLocalFolder.CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting);
                string jsonString = JsonConvert.SerializeObject(items);
                await FileIO.WriteTextAsync(file, jsonString);
            }
            catch (Exception exp)
            {
                GUIServices.MessageService.ShowMessage(exp.Message);
            }
        }

        public async Task<List<string>> GetSharedFileNames()
        {
            List<string> fileNames = new List<string>();
            foreach (var item in await ApplicationData.Current.SharedLocalFolder.GetFilesAsync())
            {
                fileNames.Add(item.Name);
            }
            return fileNames;
        }

        public async Task<List<TodoItem>> GetTodosFromLastFile()
        {
            try
            {
                string lastFileName = await GetLastFileName();
                if (lastFileName != string.Empty)
                {
                    return await LoadSharedTodoItems(lastFileName);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception exp)
            {
                GUIServices.MessageService.ShowMessage(exp.Message);
                return null;
            }
        }

        public async Task SaveTodosToLastFile(List<TodoItem> todos)
        {
            if (todos == null)
                return;
            try
            {
                string lastFileName = await GetLastFileName();
                await SaveSharedTodoItems(lastFileName, todos);
            }
            catch (Exception exp)
            {
                GUIServices.MessageService.ShowMessage(exp.Message);
            }
        }
        #endregion

        #region Import/Export

        public async Task<string> ExportTodoItems(List<TodoItem> items)
        {
            try
            {
                FileSavePicker picker = new FileSavePicker();
                picker.FileTypeChoices.Add("JSON Datei", new List<string>() { ".json" });
                picker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
                picker.SuggestedFileName = "MyTodos";
                var file = await picker.PickSaveFileAsync();
                string jsonString = JsonConvert.SerializeObject(items);
                await FileIO.WriteTextAsync(file, jsonString);
                return file.Name;
            }
            catch (Exception exp)
            {
                GUIServices.MessageService.ShowMessage(exp.Message);
                return string.Empty;
            }
        }

        public async  Task<Tuple<List<TodoItem>, string>> ImportTodoItems()
        {
            try
            {
                FileOpenPicker picker = new FileOpenPicker();
                picker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
                picker.FileTypeFilter.Add(".json");
                var file = await picker.PickSingleFileAsync();
                string json = await FileIO.ReadTextAsync(file);
                var todos = JsonConvert.DeserializeObject<List<TodoItem>>(json);
                return new Tuple<List<TodoItem>, string>(todos, file.Name);
            }
            catch (Exception exp)
            {
                GUIServices.MessageService.ShowMessage(exp.Message);
                return new Tuple<List<TodoItem>, string>(new List<TodoItem>(), string.Empty);
            }
        }
        #endregion

       
    }
}