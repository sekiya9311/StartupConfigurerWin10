using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using StartupConfigurerWin10.Entity;

namespace StartupConfigurerWin10.Model
{
    public interface IShortcutService
    {
        /// <summary>
        /// 指定したディレクトリパスのショートカットをシーケンスで返します。
        /// </summary>
        /// <param name="path">ショートカット取得元のパス</param>
        /// <returns>取得したショートカットオブジェクトのシーケンス</returns>
        IEnumerable<IShortcut> GetShortcuts(string path);

        void SaveShortcut(string path, IShortcut shortcut);

        /// <summary>
        /// 引数のショートカットを指定したディレクトリに作成します。
        /// </summary>
        /// <param name="path">ショートカットを保存するパス</param>
        /// <param name="shortcuts">保存対象のショートカット</param>
        void SaveShortcuts(string path, IEnumerable<IShortcut> shortcuts);

        void DeleteShortcut(string path, IShortcut shortcut);

        void DeleteShortcuts(string path, IEnumerable<IShortcut> shortcuts);
    }
}
