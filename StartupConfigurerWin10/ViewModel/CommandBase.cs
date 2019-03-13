using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace StartupConfigurerWin10.ViewModel
{
    class CommandBase : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private readonly Action act;

        public CommandBase(Action _act)
        {
            this.act = _act;
        }

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter) => this.act?.Invoke();
    }
}
