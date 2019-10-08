using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

using StartupConfigurerWin10.Entity;
using Reactive.Bindings;

namespace StartupConfigurerWin10.Model
{
    public interface IMainWindowModel
    {
        ReadOnlyReactivePropertySlim<List<IShortcut>> StartupShortcuts { get; }
        void NewStartupShortcut();
        void DeleteStartupShortcut(IShortcut shortcut);
        void DeleteStartupShortcuts(IEnumerable<IShortcut> shortcuts);
        void ShowMessage(string message, string caption);
    }
}
