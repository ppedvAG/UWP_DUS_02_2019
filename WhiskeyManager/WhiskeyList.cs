using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhiskeyManager
{
    public class WhiskeyList
    {
        public static ObservableCollection<Whiskey> Whiskeys { get; set; }

        static WhiskeyList()
        {
            Whiskeys = new ObservableCollection<Whiskey>()
            {
                new Whiskey("Jack Daniels", 1900, false, Whiskey.WhiskeyType.Scotch),
                new Whiskey("Famous Grouse", 2010, true, Whiskey.WhiskeyType.Bourbon)
            };
        }
    }
}
