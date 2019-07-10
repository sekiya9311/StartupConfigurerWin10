using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reactive.Bindings;

using StartupConfigurerWin10.Base;
using StartupConfigurerWin10.Entity;
using StartupConfigurerWin10.Model;

namespace StartupConfigurerWin10.ViewModel
{
    public class MainWindowViewModel
    {
        private IMainWindowModel _model;

        public ReactiveCollection<ShortcutForDisplay> StartupShortcuts { get; }

        public ReactivePropertySlim<IShortcut> SelectedShortcut { get; }

        public ReactiveCommand AddCommand { get; }

        public ReactiveCommand RemoveCommand { get; }

        public ReactiveCommand SaveCommand { get; }

        public MainWindowViewModel(IMainWindowModel model)
        {
            _model = model;


        }

        public MainWindowViewModel() : this(new MainWindowModel()) { }
    }
}
