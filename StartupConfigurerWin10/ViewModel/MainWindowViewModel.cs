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

        public ReadOnlyReactiveCollection<ShortcutForDisplay> StartupShortcutsForDisp { get; }

        public ReactivePropertySlim<int> SelectedShortcutIndex { get; } = new ReactivePropertySlim<int>();

        public ReactiveCommand AddCommand { get; } = new ReactiveCommand();

        public ReactiveCommand RemoveCommand { get; } = new ReactiveCommand();

        public ReactiveCommand SaveCommand { get; } = new ReactiveCommand();

        public MainWindowViewModel(IMainWindowModel model)
        {
            if (model is null) return;

            _disposables = new CompositeDisposable();
            _model = model;

            StartupShortcuts = new ReactiveCollection<ShortcutForDisplay>(
                Observable.ToObservable(
                    _model.GetStartupShortcuts().Select(s => new ShortcutForDisplay(s))))
                .AddTo(_disposables);

            StartupShortcutsForDisp = StartupShortcuts
                .Where(s => !s.Delete)
                .ToReadOnlyReactiveCollection(StartupShortcuts.ToCollectionChanged());

            AddCommand.Subscribe(AddShortcut).AddTo(_disposables);

            RemoveCommand.Subscribe(RemoveShortcut).AddTo(_disposables);

            SaveCommand.Subscribe(SaveShortcuts).AddTo(_disposables);
        }

        private void AddShortcut()
        {
            var newShortcuts = _model.NewStartupShortcut()
                .Select(s => new ShortcutForDisplay(s))
                .ToArray();
            StartupShortcuts.AddRangeOnScheduler(newShortcuts);
        }

        private void RemoveShortcut()
        {
            if (SelectedShortcutIndex.Value == -1)
            {
                return;
            }

            var deleteShortcutDisp = StartupShortcutsForDisp[SelectedShortcutIndex.Value];
            var deleteShortcut = StartupShortcuts.Where(s => s.Equals(deleteShortcutDisp)).First();
            deleteShortcut.Delete = true;
            SelectedShortcutIndex.Value = -1;
        }

        private void SaveShortcuts()
        {
            _model.SaveStartupShortcuts(StartupShortcuts);

        }

        [Obsolete("このコンストラクタはデザイナ用です。", true)]
        public MainWindowViewModel() : this(null) { }
    }
}
