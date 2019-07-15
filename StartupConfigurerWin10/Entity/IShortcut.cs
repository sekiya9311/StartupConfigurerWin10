using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartupConfigurerWin10.Entity
{
    /// <summary>
    /// ショートカットを表します。
    /// </summary>
    public interface IShortcut : IEquatable<IShortcut>
    {
        /// <summary> ショートカットのアイコンのパスを取得、設定します。 </summary>
        string IconLocation { get; set; }
        /// <summary> ショートカットのリンク先のパスを取得、設定します。 </summary>
        string TargetPath { get; set; }
        /// <summary> ショートカットの実行時の引数を取得、設定します。 </summary>
        string Arguments { get; set; }
        /// <summary> 作業フォルダのパスを取得、設定します。 </summary>
        string WorkingDirectory { get; set; }
        /// <summary> 実行時のウィンドウスタイルを取得、設定します。 </summary>
        WindowStyle WindowStyle { get; set; }
        /// <summary> ショートカットのコメントを取得、設定します。 </summary>
        string Description { get; set; }
        /// <summary> ショートカットファイルのフルパスを取得、設定します。 </summary>
        string FullName { get; set; }
    }
}
