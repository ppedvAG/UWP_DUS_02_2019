using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Foundation.Metadata;
using Windows.Storage;
using Windows.Storage.AccessCache;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace SideLoadingTestApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            ShowSharedFiles();

            return;
            ApplicationData.Current.LocalSettings.Values.Add("Title", "Title 1");
            if(ApplicationData.Current.LocalSettings.Values.TryGetValue("Title", out object value)) 
            {
                string title = value.ToString();
            }

            CreateFiles();

        }

        private async void ShowSharedFiles()
        {
            var folder = ApplicationData.Current.SharedLocalFolder;

            try
            {
                var files = await folder.GetFilesAsync();
                if (files != null)
                {
                    foreach (var file in files)
                    {
                        lbMessages.Items.Add($"Datei {file.Path}\\{file.Name} wurde im SharedFolder gefunden!");
                    }
                }
            }
            catch (Exception)
            {
                lbMessages.Items.Add($"Keine Dateien im SharedFolder gefunden!");
            }
        }

        public async void CreateFiles()
        {
            
            StorageFile file = await ApplicationData.Current.LocalFolder.CreateFileAsync("MyFile.txt");
            StorageFolder folder = await ApplicationData.Current.LocalFolder.CreateFolderAsync("Ordner");

            await ApplicationData.Current.LocalFolder.GetFileAsync("MyFile.txt");
            //Pack-URI:Syntax: "ms-appdata:///localcache/MyFile.txt"


            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.FileTypeFilter.Add("*");
            openPicker.SuggestedStartLocation = PickerLocationId.Downloads;

            try
            {
                await KnownFolders.MusicLibrary.CreateFileAsync("My.mp3");
            }
            catch (Exception)
            {

                throw;
            }
           

            StorageFile pickedFile = await openPicker.PickSingleFileAsync();
            

            StorageApplicationPermissions.FutureAccessList.Add(pickedFile, "FileForImage");


            ApplicationData.Current.SharedLocalFolder.CreateFileAsync("asdasd");

            FileSavePicker savePicker = new FileSavePicker();

            FolderPicker folderPicker = new FolderPicker();
            StorageFolder pickedFolder = await  folderPicker.PickSingleFolderAsync();
           
            if(ApiInformation.IsApiContractPresent("Windows.Foundation.UniversalApiContract", 5))
            {

            }
            
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            StorageFolder folder = ApplicationData.Current.SharedLocalFolder;
            var item = await folder.CreateFileAsync($"{tbFileName.Text}.txt",CreationCollisionOption.ReplaceExisting);
            
            lbMessages.Items.Add($"Datei {item.Path}\\{item.Name} wurde im SharedFolder angelegt!");
        }
    }
}
