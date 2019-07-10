using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

using StartupConfigurerWin10.Entity;

namespace StartupConfigurerWin10.Model
{
    public interface IMainWindowModel
    {
        IEnumerable<IShortcut> GetStartupShortcuts();
        IShortcut NewStartupShortcut();
        void RemoveStartupShortcut(IShortcut shortcut);
        void SaveStartupShortcuts(IEnumerable<IShortcut> shortcuts);
    }
}
