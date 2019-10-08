using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Reactive.Bindings;
using StartupConfigurerWin10.Entity;
using StartupConfigurerWin10.Extensions;

namespace StartupConfigurerWin10.Model
{
    public class MainWindowModel : IMainWindowModel
    {
        public static string StartupPath => Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.StartMenu),
            "Programs",
            "Startup");

        public ReadOnlyReactivePropertySlim<List<IShortcut>> StartupShortcuts { get; }

        private readonly FileSystemWatcher _wathcer;
        private readonly ISelectExecuteFileService _selectExecuteFileService;
        private readonly IShortcutService _shortcutService;
        private readonly IDialogService _dialogService;
        private readonly IOpenExolorerService _openExolorerService;

        public MainWindowModel(
            ISelectExecuteFileService selectExecuteFileService,
            IShortcutService shortcutService,
            IDialogService dialogService,
            IOpenExolorerService openExolorerService)
        {
            _selectExecuteFileService = selectExecuteFileService;
            _shortcutService = shortcutService;
            _dialogService = dialogService;
            _openExolorerService = openExolorerService;

            _wathcer = new FileSystemWatcher(StartupPath, "*.*") { EnableRaisingEvents = true };
            StartupShortcuts = new[]
            {
                _wathcer.CreatedAsObservable(),
                _wathcer.ChangedAsObservable(),
                _wathcer.DeletedAsObservable(),
                _wathcer.RenamedAsObservable()
            }
            .Merge()
            .Select(_ => GetStartupShorcuts())
            .ToReadOnlyReactivePropertySlim(GetStartupShorcuts());
        }

        private List<IShortcut> GetStartupShorcuts()
            => _shortcutService.GetShortcuts(StartupPath).ToList();

        public void NewStartupShortcut()
        {
            var selectFilePathes = _selectExecuteFileService.SelectExecuteFiles();
            foreach (var filePath in selectFilePathes)
            {
                var fileNameWithoutExt = Path.GetFileNameWithoutExtension(filePath);
                var shortcut = new Shortcut()
                {
                    Arguments = "",
                    Description = "Make by StartupConfigurerWin10",
                    FullName = Path.Combine(StartupPath, fileNameWithoutExt + ".lnk"),
                    IconLocation = filePath + ",0",
                    TargetPath = filePath,
                    WindowStyle = WindowStyle.Normal,
                    WorkingDirectory = ""
                };

                _shortcutService.SaveShortcut(StartupPath, shortcut);
            }
        }

        public void DeleteStartupShortcut(IShortcut shortcut)
        {
            _shortcutService.DeleteShortcut(StartupPath, shortcut);
        }

        public void DeleteStartupShortcuts(IEnumerable<IShortcut> shortcuts)
        {
            _shortcutService.DeleteShortcuts(StartupPath, shortcuts);
        }

        public void OpenStartupDirectory()
        {
            _openExolorerService.Open(StartupPath);
        }

        public void ShowMessage(string message, string caption)
        {
            _dialogService.ShowMessage(message, caption);
        }
    }
}
