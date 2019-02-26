using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhiskeyManager
{
    class Whiskey : INotifyPropertyChanged
    {
        public enum WhiskeyType { SingleMalt, DoubleMalt, TripleMalt }

        public event PropertyChangedEventHandler PropertyChanged;

        //Snippet: propfull + Tab + Tab
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

        private int _jahrgang;
        public int Jahrgang
        {
            get { return _jahrgang; }
            set
            {
                _jahrgang = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Jahrgang)));
            }
        }

        private bool _leer;
        public bool Leer
        {
            get { return _leer; }
            set
            {
                _leer = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Leer)));
            }
        }

        private WhiskeyType _type;
        public WhiskeyType Type
        {
            get { return _type; }
            set
            {
                _type = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Type)));
            }
        }

    }
}
