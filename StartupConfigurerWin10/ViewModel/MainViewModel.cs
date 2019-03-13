using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using StartupConfigurerWin10.Model;

namespace StartupConfigurerWin10.ViewModel
{
    class MainViewModel : ViewModelBase
    {
        public ObservableCollection<Shortcut> StartupShortcuts { get; set; }

        private CommandBase _addCommand = null;
        public CommandBase AddCommand =>
            (_addCommand ?? (_addCommand = new CommandBase(Add)));

        private CommandBase _removeCommand = null;
        public CommandBase RemoveCommand =>
            (_removeCommand ?? (_removeCommand = new CommandBase(Remove)));

        private CommandBase _saveCommand = null;
        public CommandBase SaveCommand =>
            (_saveCommand ?? (_saveCommand = new CommandBase(Save)));

        public MainViewModel()
        {
            Initialize();
        }

        private void Initialize()
        {
            // TODO: get current startup shortcuts
            this.StartupShortcuts = new ObservableCollection<Shortcut>();
        }

        private void Add()
        {
            // TODO: impl
        }

        private void Remove()
        {
            // TODO: impl
            // remove selected items
        }

        private void Save()
        {
            // TODO: save startup
        }
    }
}
