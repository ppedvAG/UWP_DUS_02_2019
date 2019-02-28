using System;
using System.Collections.Generic;
using System.Text;

namespace TodoModels.MVVMHelper
{
    public enum Views
    {
        Main,
        Todos,
        Export
    }

    public interface IViewModel { }

    public interface INavigationService
    {
        void GoToView(Views view, IViewModel viewModel);
    }
}