using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reactive.Linq;
using System.Reactive.Disposables;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

using StartupConfigurerWin10.Entity;
using StartupConfigurerWin10.Model;

namespace StartupConfigurerWin10.ViewModel
{
    public class MainWindowViewModel
    {
        private CompositeDisposable _disposables;
        private IMainWindowModel _model;

        public ReactiveCollection<ShortcutForDisplay> StartupShortcuts { get; }

        public ReactivePropertySlim<IShortcut> SelectedShortcut { get; } = new ReactivePropertySlim<IShortcut>();

        public ReactiveCommand AddCommand { get; } = new ReactiveCommand();

        public ReactiveCommand RemoveCommand { get; } = new ReactiveCommand();

        public ReactiveCommand SaveCommand { get; } = new ReactiveCommand();

        public MainWindowViewModel(IMainWindowModel model)
        {
            _disposables = new CompositeDisposable();
            _model = model;

            StartupShortcuts = new ReactiveCollection<ShortcutForDisplay>(
                Observable.ToObservable(
                    _model.GetStartupShortcuts().Select(s => new ShortcutForDisplay(s))))
                .AddTo(_disposables);

            AddCommand.Subscribe(AddShortcut).AddTo(_disposables);

            RemoveCommand.Subscribe(RemoveShortcut).AddTo(_disposables);

            SaveCommand.Subscribe(SaveShortcuts).AddTo(_disposables);
        }

        private void AddShortcut()
        {

        }

        private void RemoveShortcut()
        {

        }

        private void SaveShortcuts()
        {

        }

        public MainWindowViewModel() : this(new MainWindowModel()) { }
    }
}
