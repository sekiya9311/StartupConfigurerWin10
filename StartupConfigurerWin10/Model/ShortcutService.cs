using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using StartupConfigurerWin10.Entity;

namespace StartupConfigurerWin10.Model
{
    class ShortcutService : IShortcutService
    {
        // <summary>
        /// 指定したディレクトリパスのショートカットをシーケンスで返します。
        /// </summary>
        /// <param name="path">ショートカット取得元のパス</param>
        /// <returns>取得したショートカットオブジェクトのシーケンス</returns>
        public IEnumerable<IShortcut> GetShortcuts(string path)
        {
            var shell = new IWshRuntimeLibrary.WshShell();

            var shortcutFiles = System.IO.Directory.EnumerateFiles(path, "*.lnk");
            foreach (var filePath in shortcutFiles)
            {
                if (!(shell.CreateShortcut(filePath) is IWshRuntimeLibrary.IWshShortcut wshShortcut))
                {
                    continue;
                }

                try
                {
                    var myShortcut = ConvertIWshShortcutToIShortcut(wshShortcut);
                    yield return myShortcut;
                }
                finally
                {
                    System.Runtime.InteropServices.Marshal.FinalReleaseComObject(wshShortcut);
                }
            }
        }

        /// <summary>
        /// 引数のショートカットを指定したディレクトリに作成します。
        /// </summary>
        /// <param name="path">ショートカットを保存するパス</param>
        /// <param name="shortcuts">保存対象のショートカット</param>
        public void SaveShortcuts(string path, IEnumerable<IShortcut> shortcuts)
        {
            foreach (var shortcut in shortcuts)
            {
                var fullName = System.IO.Path.Combine(path, System.IO.Path.GetFileName(shortcut.FullName));
                if (System.IO.File.Exists(fullName))
                {
                    System.IO.File.Delete(fullName);
                }
                shortcut.FullName = fullName;

                var wshShortcut = ConvertIShortcutToIWshShortcut(shortcut);
                try
                {
                    wshShortcut.Save();
                }
                finally
                {
                    System.Runtime.InteropServices.Marshal.FinalReleaseComObject(wshShortcut);
                }
            }
        }

        /// <summary>
        /// WSHのIWshShortcutから自作IShortcutのインスタンスを生成し返します。
        /// </summary>
        /// <param name="wshShortcut">コピー対象のWSH版ショートカットオブジェクト</param>
        /// <returns>自作ショートカットオブジェクト</returns>
        private static IShortcut ConvertIWshShortcutToIShortcut(IWshRuntimeLibrary.IWshShortcut wshShortcut)
        {
            var ret = new Shortcut()
            {
                IconLocation = wshShortcut.IconLocation,
                TargetPath = wshShortcut.TargetPath,
                Arguments = wshShortcut.Arguments,
                WorkingDirectory = wshShortcut.WorkingDirectory,
                WindowStyle = (WindowStyle)wshShortcut.WindowStyle,
                Description = wshShortcut.Description,
                FullName = wshShortcut.FullName
            };

            return ret;
        }

        /// <summary>
        /// 自作IShortcutのインスタンスからWSHのIWshShortcutのインスタンスを生成し返します。
        /// </summary>
        /// <param name="shortcut">コピー対象の自作ショートカットオブジェクト</param>
        /// <returns>WSH版ショートカットオブジェクト</returns>
        private static IWshRuntimeLibrary.IWshShortcut ConvertIShortcutToIWshShortcut(IShortcut shortcut)
        {
            var shell = new IWshRuntimeLibrary.WshShell();

            var ret = shell.CreateShortcut(shortcut.FullName) as IWshRuntimeLibrary.IWshShortcut;
            ret.IconLocation = shortcut.IconLocation;
            ret.TargetPath = shortcut.TargetPath;
            ret.Arguments = shortcut.Arguments;
            ret.WorkingDirectory = shortcut.WorkingDirectory;
            ret.WindowStyle = (int)shortcut.WindowStyle;
            ret.Description = shortcut.Description;

            return ret;
        }
    }
}
