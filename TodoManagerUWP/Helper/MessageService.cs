using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoModels.MVVMHelper;
using Windows.UI.Popups;

namespace TodoManagerUWP.Helper
{
    public class MessageService : IMessageService
    {
        public async void ShowMessage(string message)
        {
            await new MessageDialog(message).ShowAsync();
        }
    }
}
