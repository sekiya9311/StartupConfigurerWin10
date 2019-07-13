using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using StartupConfigurerWin10.Entity;

namespace StartupConfigurerWin10.Model
{
    class MainWindowModel : IMainWindowModel
    {
        public static string StartupPath => System.IO.Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.StartMenu),
            "Programs",
            "Startup");

        private readonly ISelectExecuteFileService _selectExecuteFileService;
        private readonly IShortcutService _shortcutService;

        public MainWindowModel(ISelectExecuteFileService selectExecuteFileService, IShortcutService shortcutService)
        {
            _selectExecuteFileService = selectExecuteFileService;
            _shortcutService = shortcutService;
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
                    Description = "Make by StartupConfigureWin10",
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
            _shortcutService.SaveStartup(StartupPath, shortcuts);
        }

        [Obsolete("このコンストラクタはデザイナ用です。", true)]
        public MainWindowModel() { }
    }
}
