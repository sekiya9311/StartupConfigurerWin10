using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StartupConfigurerWin10.Entity;

namespace StartupConfigurerWin10.Model
{
    class MainWindowModel : IMainWindowModel
    {
        public ObservableCollection<ShortcutForDisplay> StartupShortcuts { get; private set; }

        public void AddStartup()
        {
            throw new NotImplementedException();
        }

        public void Remove()
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}
