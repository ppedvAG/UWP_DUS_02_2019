using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GutesWetter
{

    public class WetterApiResult
    {
        public Weather[] weather { get; set; }
        public Main main { get; set; }
    }
    
    public class Main
    {
        public float temp { get; set; }
    }

    public class Weather
    {
        public string icon { get; set; }
    }
    
}
