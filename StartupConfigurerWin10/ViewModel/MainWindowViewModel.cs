using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using StartupConfigurerWin10.Base;
using StartupConfigurerWin10.Model;

namespace StartupConfigurerWin10.ViewModel
{
    class MainWindowViewModel : BindingBase
    {
        public IMainWindowModel Model { get; }

        public CommandBase AddCommand { get; }

        public CommandBase RemoveCommand { get; }

        public CommandBase SaveCommand { get; }

        public MainWindowViewModel(IMainWindowModel model)
        {
            this.Model = model;
            this.AddCommand = new CommandBase(this.Model.AddStartup);
            this.RemoveCommand = new CommandBase(this.Model.Remove);
            this.SaveCommand = new CommandBase(this.Model.Save);
        }
    }
}
