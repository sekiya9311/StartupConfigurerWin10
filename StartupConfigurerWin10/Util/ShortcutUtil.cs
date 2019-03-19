using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using StartupConfigurerWin10.Entity;

// TODO: 例外処理とか解放処理とかは後でがんばろー

namespace StartupConfigurerWin10.Util
{
    class ShortcutUtil
    {
        public static string StartupPath => System.IO.Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.StartMenu),
            "Programs",
            "Startup");

        /// <summary>
        /// 現在のスタートアップショートカットをシーケンスで返します。
        /// </summary>
        /// <returns>現在のスタートアップショートカットのシーケンス</returns>
        public static IEnumerable<IShortcut> GetCurrentStartup()
        {
            var shell = new IWshRuntimeLibrary.WshShell();

            var startupFiles = System.IO.Directory.EnumerateFiles(StartupPath, "*.lnk");
            foreach (var filePath in startupFiles)
            {
                if (!(shell.CreateShortcut(filePath) is IWshRuntimeLibrary.IWshShortcut wshShortcut))
                {
                    continue;
                }
                
                yield return ConvertIWshShortcutToIShortcut(wshShortcut);
            }
        }

        /// <summary>
        /// 引数のショートカットをスタートアップフォルダに作成します。
        /// </summary>
        /// <param name="shortcuts">スタートアップフォルダ保存対象のショートカット</param>
        public static void SaveStartup(IEnumerable<IShortcut> shortcuts)
        {
            var curStartupFiles = System.IO.Directory.EnumerateFiles(StartupPath);
            foreach (var filePath in curStartupFiles)
            {
                System.IO.File.Delete(filePath);
            }

            foreach (var shortcut in shortcuts)
            {
                var wshShortcut = ConvertIShortcutToIWshShortcut(shortcut);
                wshShortcut.Save();
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(wshShortcut);
            }
        }

        /// <summary>
        /// WSHのIWshShortcutから自作IShortcutのインスタンスを生成し返します。
        /// </summary>
        /// <param name="wshShortcut">コピー対象のWSH版ショートカットオブジェクト</param>
        /// <returns>自作ショートカットオブジェクト</returns>
        public static IShortcut ConvertIWshShortcutToIShortcut(IWshRuntimeLibrary.IWshShortcut wshShortcut)
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

            System.Runtime.InteropServices.Marshal.FinalReleaseComObject(wshShortcut);

            return ret;
        }

        /// <summary>
        /// 自作IShortcutのインスタンスからWSHのIWshShortcutのインスタンスを生成し返します。
        /// </summary>
        /// <param name="shortcut">コピー対象の自作ショートカットオブジェクト</param>
        /// <returns>WSH版ショートカットオブジェクト</returns>
        public static IWshRuntimeLibrary.IWshShortcut ConvertIShortcutToIWshShortcut(IShortcut shortcut)
        {
            var shell = new IWshRuntimeLibrary.WshShell();

            var ret = shell.CreateShortcut(shortcut.FullName) as IWshRuntimeLibrary.IWshShortcut;
            return ret;
        }
    }
}
