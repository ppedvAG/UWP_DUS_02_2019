using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace TodoModels.MVVMHelper
{
    public class DelegateCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        Action<object> _executeLogic;
        Func<object, bool> _canExecuteLogic;

        public DelegateCommand(Action<object> executeLogic, Func<object, bool> canExecuteLogic = null)
        {
            _executeLogic = executeLogic;
            _canExecuteLogic = canExecuteLogic;
        }

        public bool CanExecute(object parameter)
        {
            if(_canExecuteLogic != null)
            {
                return _canExecuteLogic.Invoke(parameter);
            }
            return true;
        }

        public void Execute(object parameter)
        {
            _executeLogic?.Invoke(parameter);
        }

        //Zum Feuern des Events von außen
        //In reinen WPF-Projekten kann dies über den CommandManager automatisch
        //abgewickelt werden, in UWP muss es händisch geschehen
        public void OnCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}