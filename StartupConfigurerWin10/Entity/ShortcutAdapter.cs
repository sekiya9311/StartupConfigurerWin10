using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartupConfigurerWin10.Entity
{
    class ShortcutAdapter : IShortcut
    {
        private IWshRuntimeLibrary.IWshShortcut _wshShortcut;

        public ShortcutAdapter(IWshRuntimeLibrary.IWshShortcut wshShortcut)
        {
            if (wshShortcut is null)
            {
                throw new ArgumentNullException(nameof(wshShortcut));
            }
            _wshShortcut = wshShortcut;
        }

        public string IconLocation
        {
            get => _wshShortcut.IconLocation;
            set => _wshShortcut.IconLocation = value;
        }
        public string TargetPath
        {
            get => _wshShortcut.TargetPath;
            set => _wshShortcut.TargetPath = value;
        }
        public string Arguments
        {
            get => _wshShortcut.Arguments;
            set => _wshShortcut.Arguments = value;
        }
        public string WorkingDirectory
        {
            get => _wshShortcut.WorkingDirectory;
            set => _wshShortcut.WorkingDirectory = value;
        }
        public WindowStyle WindowStyle
        {
            get => (WindowStyle)_wshShortcut.WindowStyle;
            set => _wshShortcut.WindowStyle = (int)value;
        }
        public string Description
        {
            get => _wshShortcut.Description;
            set => _wshShortcut.Description = value;
        }
    }
}
