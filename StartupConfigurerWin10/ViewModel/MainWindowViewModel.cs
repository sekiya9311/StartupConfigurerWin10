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
using Reactive.Bindings.Helpers;

using StartupConfigurerWin10.Entity;
using StartupConfigurerWin10.Model;

namespace StartupConfigurerWin10.ViewModel
{
    public class MainWindowViewModel : IDisposable
    {
        private CompositeDisposable _disposables;
        private IMainWindowModel _model;

        public ReadOnlyReactivePropertySlim<List<ShortcutForDisplay>> StartupShortcuts { get; }

        public ReactivePropertySlim<int> SelectedShortcutIndex { get; } = new ReactivePropertySlim<int>(-1);

        public ReactiveCommand AddCommand { get; } = new ReactiveCommand();

        public ReactiveCommand RemoveCommand { get; } = new ReactiveCommand();

        public MainWindowViewModel(IMainWindowModel model)
        {
            if (model is null) return;

            _disposables = new CompositeDisposable();
            _model = model;
            if (_model is IDisposable disposableModel)
            {
                _disposables.Add(disposableModel);
            }

            StartupShortcuts = _model.StartupShortcuts
                .Select(x => x?.Select(i => new ShortcutForDisplay(i)).ToList())
                .ToReadOnlyReactivePropertySlim(new List<ShortcutForDisplay>())
                .AddTo(_disposables);

            AddCommand
                .Subscribe(AddShortcut)
                .AddTo(_disposables);

            RemoveCommand
                .Subscribe(RemoveShortcut)
                .AddTo(_disposables);
        }

        private void AddShortcut()
        {
            _model.NewStartupShortcut();
        }

        private void RemoveShortcut()
        {
            if (SelectedShortcutIndex.Value == -1)
            {
                return;
            }

            var removeShortcut = StartupShortcuts.Value[SelectedShortcutIndex.Value];
            _model.DeleteStartupShortcut(removeShortcut);

            SelectedShortcutIndex.Value = -1;
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}
