using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using StartupConfigurerWin10.Entity;
using StartupConfigurerWin10.Util;

namespace StartupConfigurerWin10.Model
{
    class MainWindowModel : IMainWindowModel
    {
        public ObservableCollection<ShortcutForDisplay> StartupShortcuts { get; private set; }

        public MainWindowModel()
        {
            Initialize();
        }

        public void AddStartup()
        {
            var ofd = new Microsoft.Win32.OpenFileDialog()
            {
                CheckFileExists = true,
                CheckPathExists = true,
                Multiselect = true,
                Filter = "実行ファイル|*.exe"
            };

            if (!(ofd.ShowDialog() ?? false))
            {
                return;
            }

            var selectFilePathes = ofd.SafeFileNames;
            foreach (var filePath in selectFilePathes)
            {
                var fileNameWithoutExt = System.IO.Path.GetFileNameWithoutExtension(filePath);
                var shortcut = new Shortcut()
                {
                    Arguments = "",
                    Description = "Make by StartupConfigureWin10",
                    FullName = System.IO.Path.Combine(ShortcutUtil.StartupPath, fileNameWithoutExt + ".lnk"),
                    IconLocation = filePath + ",0",
                    TargetPath = filePath,
                    WindowStyle = WindowStyle.Normal,
                    WorkingDirectory = ""
                };
                StartupShortcuts.Add(new ShortcutForDisplay(shortcut));
            }
        }

        public void Remove()
        {
            var selectedFiles = StartupShortcuts
                .Where(f => f.IsSelected)
                .ToArray(); // 遅延してるとやばそうなので
            foreach (var file in selectedFiles)
            {
                StartupShortcuts.Remove(file);
            }
        }

        public void Save()
        {
            ShortcutUtil.SaveStartup(StartupShortcuts);

            Initialize();
        }

        private void Initialize()
        {
            var currentStartupFiles = ShortcutUtil.GetCurrentStartup();
            StartupShortcuts = new ObservableCollection<ShortcutForDisplay>(
                currentStartupFiles.Select(f => new ShortcutForDisplay(f)));
        }
    }
}
