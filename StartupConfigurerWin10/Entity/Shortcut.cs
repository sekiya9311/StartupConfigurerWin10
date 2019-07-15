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
    public class Shortcut : IShortcut
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
        /// <summary> ショートカットファイルのフルパスを取得、設定します。 </summary>
        public string FullName { get; set; }

        public bool Equals(IShortcut other)
        {
            if (IconLocation != other.IconLocation ||
                TargetPath != other.TargetPath ||
                Arguments != other.Arguments ||
                WorkingDirectory != other.WorkingDirectory ||
                WindowStyle != other.WindowStyle ||
                Description != other.Description ||
                FullName != other.FullName)
                return false;

            return true;
        }

        public override bool Equals(object obj)
            => (obj is IShortcut s) && Equals(s);

        public override int GetHashCode()
            => IconLocation.GetHashCode() ^
                TargetPath.GetHashCode() ^
                Arguments.GetHashCode() ^
                WorkingDirectory.GetHashCode() ^
                WindowStyle.GetHashCode() ^
                WindowStyle.GetHashCode() ^
                Description.GetHashCode() ^
                FullName.GetHashCode();
    }

    /// <summary>
    /// ウィンドウのスタイルを表します。
    /// </summary>
    public enum WindowStyle
    {
        /// <summary> 通常状態を表します。 </summary>
        Normal = 1,
        /// <summary> 最大化状態を表します。 </summary>
        Maximum = 3,
        /// <summary> 最小化状態を表します。 </summary>
        Minimum = 7
    }
}
