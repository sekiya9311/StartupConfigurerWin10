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
        public MainWindowModel()
        {

        }

        public IEnumerable<IShortcut> GetStartupShortcuts()
        {

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
            }
        }

        public IShortcut NewStartupShortcut()
        {
            throw new NotImplementedException();
        }

        public void RemoveStartupShortcut(IShortcut shortcut)
        {
            throw new NotImplementedException();
        }

        public void SaveStartupShortcuts(IEnumerable<IShortcut> shortcuts)
        {
            throw new NotImplementedException();
        }

        [Obsolete("このコンストラクタはデザイナ用です。", true)]
        public MainWindowModel() { }
    }
}
