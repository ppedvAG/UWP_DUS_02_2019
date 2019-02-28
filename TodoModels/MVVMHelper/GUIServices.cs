using System;
using System.Collections.Generic;
using System.Text;

namespace TodoModels.MVVMHelper
{
    //vereinfachte Factory, kann mit Singleton/Factory-Pattern architektonisch veredelt werden
    public class GUIServices
    {
        public static INavigationService NavigationService { get; set; }

    }
}
