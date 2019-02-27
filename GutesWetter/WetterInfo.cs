using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GutesWetter
{
    public class WetterInfo : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name)));
            }
        }

        private double _temperature;
        public double Temperature
        {
            get { return _temperature; }
            set
            {
                _temperature = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Temperature)));
            }
        }

        private bool _isLoaded;
        public bool IsLoaded
        {
            get { return _isLoaded; }
            set
            {
                _isLoaded = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsLoaded)));
            }
        }

        private string _iconURL;
        public string IconUrl
        {
            get { return _iconURL; }
            set
            {
                _iconURL = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IconUrl)));
            }
        }

        public WetterInfo(string name, double temperature = 0, string iconUrl = "")
        {
            Name = name;
            Temperature = temperature;
            IconUrl = iconUrl;
        }
    }
}
