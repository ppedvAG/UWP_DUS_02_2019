using TodoModels.Models;

namespace TodoModels.MVVMHelper
{
    public interface INotificationService
    {
        void AddNotification(TodoItem todoItem);
        void RemoveNotification(TodoItem todoItem);
    }
}