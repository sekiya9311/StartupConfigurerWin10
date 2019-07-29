using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using StartupConfigurerWin10.Entity;

namespace StartupConfigurerWin10.Model
{
    public class MainWindowModel : IMainWindowModel
    {
        public static string StartupPath => System.IO.Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.StartMenu),
            "Programs",
            "Startup");

        private readonly ISelectExecuteFileService _selectExecuteFileService;
        private readonly IShortcutService _shortcutService;
        private readonly IDialogService _dialogService;

        public MainWindowModel(ISelectExecuteFileService selectExecuteFileService, IShortcutService shortcutService, IDialogService dialogService)
        {
            _selectExecuteFileService = selectExecuteFileService;
            _shortcutService = shortcutService;
            _dialogService = dialogService;
        }

        public IEnumerable<IShortcut> GetStartupShortcuts()
            => _shortcutService.GetShortcuts(StartupPath);

        public IEnumerable<IShortcut> NewStartupShortcut()
        {
            var selectFilePathes = _selectExecuteFileService.SelectExecuteFiles();
            foreach (var filePath in selectFilePathes)
            {
                var fileNameWithoutExt = System.IO.Path.GetFileNameWithoutExtension(filePath);
                var shortcut = new Shortcut()
                {
                    Arguments = "",
                    Description = "Make by StartupConfigurerWin10",
                    FullName = System.IO.Path.Combine(StartupPath, fileNameWithoutExt + ".lnk"),
                    IconLocation = filePath + ",0",
                    TargetPath = filePath,
                    WindowStyle = WindowStyle.Normal,
                    WorkingDirectory = ""
                };

                yield return shortcut;
            }
        }

        public void SaveStartupShortcuts(IEnumerable<IShortcut> shortcuts)
        {
            try
            {
                _shortcutService.SaveShortcuts(StartupPath, shortcuts);
                _dialogService.ShowMessage("保存しました。");
            }
            catch
            {
                throw;
            }
        }

        public void DeleteStartupShortcuts(IEnumerable<IShortcut> shortcuts)
        {
            _shortcutService.DeleteShortcuts(StartupPath, shortcuts);
        }

        public void ShowMessage(string message, string caption)
        {
            _dialogService.ShowMessage(message, caption);
        }
    }
}
