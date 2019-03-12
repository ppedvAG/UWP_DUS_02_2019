using System.Windows;
using TodoModels.MVVMHelper;

namespace TodoManagerWPF.Services
{
    internal class MessageService : IMessageService
    {
        public void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }
    }
}