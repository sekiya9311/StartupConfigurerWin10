using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartupConfigurerWin10.Entity
{
    /// <summary>
    /// WSHでのショートカット情報を表します。
    /// </summary>
    /// <remarks>
    /// HACK: とりあえず使いそうなやつのみ
    /// </remarks>
    class Shortcut : IShortcut
    {
        /// <summary> ショートカットのアイコンのパスを取得、設定します。 </summary>
        public string IconLocation { get; set; }
        /// <summary> ショートカットのリンク先のパスを取得、設定します。 </summary>
        public string TargetPath { get; set; }
        /// <summary> ショートカットの実行時の引数を取得、設定します。 </summary>
        public string Arguments { get; set; }
        /// <summary> 作業フォルダのパスを取得、設定します。 </summary>
        public string WorkingDirectory { get; set; }
        /// <summary> 実行時のウィンドウスタイルを取得、設定します。 </summary>
        public WindowStyle WindowStyle { get; set; }
        /// <summary> ショートカットのコメントを取得、設定します。 </summary>
        public string Description { get; set; }
    }

    /// <summary>
    /// ウィンドウのスタイルを表します。
    /// </summary>
    enum WindowStyle
    {
        /// <summary> 通常状態を表します。 </summary>
        Normal = 1,
        /// <summary> 最大化状態を表します。 </summary>
        Maximum = 3,
        /// <summary> 最小化状態を表します。 </summary>
        Minimum = 7
    }
}
