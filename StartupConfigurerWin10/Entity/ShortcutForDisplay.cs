using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartupConfigurerWin10.Entity
{
    public class ShortcutForDisplay : IShortcut
    {
        private readonly IShortcut _shortcut;

        public ShortcutForDisplay(IShortcut shortcut)
        {
            _shortcut = shortcut;
        }

        public string IconLocation { get => _shortcut.IconLocation; set => _shortcut.IconLocation = value; }
        public string TargetPath { get => _shortcut.TargetPath; set => _shortcut.TargetPath = value; }
        public string Arguments { get => _shortcut.Arguments; set => _shortcut.Arguments = value; }
        public string WorkingDirectory { get => _shortcut.WorkingDirectory; set => _shortcut.WorkingDirectory = value; }
        public WindowStyle WindowStyle { get => _shortcut.WindowStyle; set => _shortcut.WindowStyle = value; }
        public string Description { get => _shortcut.Description; set => _shortcut.Description = value; }
        public string FullName { get => _shortcut.FullName; set => _shortcut.FullName = value; }

        public string FileName => System.IO.Path.GetFileNameWithoutExtension(_shortcut.FullName);
    }
}
