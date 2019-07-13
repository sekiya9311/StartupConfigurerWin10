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
    public class MainWindowViewModel
    {
        private CompositeDisposable _disposables;
        private IMainWindowModel _model;

        public ObservableCollection<ShortcutForDisplay> StartupShortcuts { get; }

        public ReadOnlyReactiveCollection<ShortcutForDisplay> StartupShortcutsForDisp { get; }

        public ReactivePropertySlim<int> SelectedShortcutIndex { get; } = new ReactivePropertySlim<int>(-1);

        public ReactiveCommand AddCommand { get; } = new ReactiveCommand();

        public ReactiveCommand RemoveCommand { get; } = new ReactiveCommand();

        public ReactiveCommand SaveCommand { get; } = new ReactiveCommand();

        public MainWindowViewModel(IMainWindowModel model)
        {
            if (model is null) return;

            _disposables = new CompositeDisposable();
            _model = model;

            StartupShortcuts = new ObservableCollection<ShortcutForDisplay>(
                _model.GetStartupShortcuts().Select(s => new ShortcutForDisplay(s)));

            StartupShortcutsForDisp = StartupShortcuts
                .ToFilteredReadOnlyObservableCollection(s => !s.Delete)
                .ToReadOnlyReactiveCollection()
                .AddTo(_disposables);

            AddCommand.Subscribe(AddShortcut).AddTo(_disposables);

            RemoveCommand.Subscribe(RemoveShortcut).AddTo(_disposables);

            SaveCommand.Subscribe(SaveShortcuts).AddTo(_disposables);
        }

        private void AddShortcut()
        {
            var newShortcuts = _model.NewStartupShortcut()
                .Select(s => new ShortcutForDisplay(s));
            foreach (var s in newShortcuts)
            {
                StartupShortcuts.Add(s);
            }
        }

        private void RemoveShortcut()
        {
            if (SelectedShortcutIndex.Value == -1)
            {
                return;
            }

            var deleteShortcutDisp = StartupShortcutsForDisp[SelectedShortcutIndex.Value];
            var deleteShortcut = StartupShortcuts.Where(s => s.Equals(deleteShortcutDisp)).First();
            StartupShortcuts.Remove(deleteShortcut);
            deleteShortcut.Delete = true;
            StartupShortcuts.Add(deleteShortcut);

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
