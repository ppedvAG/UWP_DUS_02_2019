using Microsoft.Toolkit.Uwp.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoModels.Models;
using TodoModels.MVVMHelper;
using Windows.UI.Notifications;

namespace TodoManagerUWP.Helper
{
    public class NotificationService : INotificationService
    {
        public void AddNotification(TodoItem todoItem)
        {
            //Bisherige Notificationen entfernen
            RemoveNotification(todoItem);

            //Nuget Packacke UWP.ToastNotification
            var content = new ToastContent()
            {
                // More about the Launch property at https://docs.microsoft.com/dotnet/api/microsoft.toolkit.uwp.notifications.toastcontent
                Launch = "ToastContentActivationParams",

                Visual = new ToastVisual()
                {
                    BindingGeneric = new ToastBindingGeneric()
                    {
                        Children =
                        {
                            new AdaptiveText()
                            {
                                Text = "TODO ist fällig!"
                            },

                            new AdaptiveText()
                            {
                                 Text = $"{todoItem.Title}: {todoItem.Description}"
                            }
                        }
                    }
                },

                Actions = new ToastActionsCustom()
                {
                    Buttons =
                    {
                        // More about Toast Buttons at https://docs.microsoft.com/dotnet/api/microsoft.toolkit.uwp.notifications.toastbutton
                        new ToastButton("Anzeigen", todoItem.CreationDate.Ticks.ToString())
                        {
                            ActivationType = ToastActivationType.Foreground
                        },
                        new ToastButtonDismiss("Abbrechen")
                    }
                }
            };

            ScheduledToastNotification newToast = new ScheduledToastNotification(content.GetXml(), todoItem.DueDate.Value);
            newToast.Tag = todoItem.CreationDate.Ticks.ToString();
            ToastNotificationManager.CreateToastNotifier().AddToSchedule(newToast);
        }

        public void RemoveNotification(TodoItem todoItem)
        {
            foreach (var item in ToastNotificationManager.CreateToastNotifier().GetScheduledToastNotifications())
            {
                
                if (item.Tag == todoItem.CreationDate.Ticks.ToString())
                {
                    ToastNotificationManager.CreateToastNotifier().RemoveFromSchedule(item);
                }
            }
        }

        public void RemoveAllNotifications()
        {
            ToastNotificationManager.CreateToastNotifier().GetScheduledToastNotifications().ToList()
                .ForEach(item => ToastNotificationManager.CreateToastNotifier().RemoveFromSchedule(item));
        }
    }
}