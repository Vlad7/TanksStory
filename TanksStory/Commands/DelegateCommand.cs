using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TanksStory.Commands
{
    class DelegateCommand : ICommand
    {
        public Action<object> ExecuteDelegate { get; set; }
        public Predicate<object> CanExecuteDelegate { get; set; }

        public DelegateCommand(Action<object> action)
        {
            ExecuteDelegate = action;
        }

        public DelegateCommand(Action<object> action, Predicate<object> predicate)
        {
            ExecuteDelegate = action;
            CanExecuteDelegate = predicate;
        }
 
        public bool CanExecute(object parameter)
        {
            if (CanExecuteDelegate != null)
            {
                return CanExecuteDelegate(parameter);
            }
 
            return true;
        }
 
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
 
        public void Execute(object parameter)
        {
            if (ExecuteDelegate != null)
            {
                ExecuteDelegate(parameter);
            }
        }
    }
}
