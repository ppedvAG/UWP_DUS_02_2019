using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TodoManagerUWP.UserControls
{
    public sealed partial class DateTimeDialog : ContentDialog
    {

        public DateTimeOffset? CurrentDate { get; set; }

        public DateTimeDialog(DateTimeOffset? currentDate)
        {
            this.InitializeComponent();
            CurrentDate = (currentDate != null) ? currentDate.Value : DateTimeOffset.Now.AddDays(7);
            calenderPicker.Date = CurrentDate;
            timePicker.Time = new TimeSpan(CurrentDate.Value.Hour, CurrentDate.Value.Minute, CurrentDate.Value.Second);
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            DateTimeOffset date = calenderPicker.Date.Value;
            TimeSpan time = timePicker.Time;

            CurrentDate = new DateTimeOffset(new DateTime(date.Year, date.Month, date.Day, time.Hours, time.Minutes, time.Seconds));
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            CurrentDate = null;           
        }
    }
}