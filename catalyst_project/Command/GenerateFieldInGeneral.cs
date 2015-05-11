using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using catalyst_project.ViewModel;

namespace catalyst_project.Command
{
    class GenerateFieldInGeneral : ICommand
    {
        private MainAppViewModel _ViewModel;

        public GenerateFieldInGeneral(MainAppViewModel viewModel) 
        {
            ViewModel = viewModel;
        }

        public MainAppViewModel ViewModel
        { 
            get
            {
                return _ViewModel;
            }

            set
            {
                _ViewModel = value;
            }
        }

        #region ICommand Members

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            //will determine if button can be enabled or disabled
            return true;
        }

        public void Execute(object parameter)
        {
            
        }
        #endregion
    }
}
