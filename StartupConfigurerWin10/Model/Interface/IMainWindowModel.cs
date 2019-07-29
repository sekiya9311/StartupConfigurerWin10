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
        IEnumerable<IShortcut> NewStartupShortcut();
        void SaveStartupShortcuts(IEnumerable<IShortcut> shortcuts);
        void DeleteStartupShortcuts(IEnumerable<IShortcut> shortcuts);
        void ShowMessage(string message, string caption);
    }
}
