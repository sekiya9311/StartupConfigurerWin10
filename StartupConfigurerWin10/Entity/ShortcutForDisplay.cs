using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using StartupConfigurerWin10.Base;

namespace StartupConfigurerWin10.Entity
{
    class ShortcutForDisplay : BindingBase, IShortcut
    {
        private IShortcut _shortcut;

        public ShortcutForDisplay(IShortcut shortcut)
        {
            _shortcut = shortcut;
        }

        public string IconLocation
        {
            get => _shortcut.IconLocation;
            set
            {
                var tmp = _shortcut.IconLocation;
                if (CheckNeedNotifyPropertyChanged(ref tmp, value))
                {
                    _shortcut.IconLocation = tmp;
                }
            }
        }
        public string TargetPath
        {
            get => _shortcut.TargetPath;
            set
            {
                var tmp = _shortcut.TargetPath;
                if (CheckNeedNotifyPropertyChanged(ref tmp, value))
                {
                    _shortcut.TargetPath = tmp;
                }
            }
        }
        public string Arguments
        {
            get => _shortcut.Arguments;
            set
            {
                var tmp = _shortcut.Arguments;
                if (CheckNeedNotifyPropertyChanged(ref tmp, value))
                {
                    _shortcut.Arguments = tmp;
                }
            }
        }
        public string WorkingDirectory
        {
            get => _shortcut.WorkingDirectory;
            set
            {
                var tmp = _shortcut.WorkingDirectory;
                if (CheckNeedNotifyPropertyChanged(ref tmp, value))
                {
                    _shortcut.WorkingDirectory = tmp;
                }
            }
        }
        public WindowStyle WindowStyle
        {
            get => _shortcut.WindowStyle;
            set
            {
                var tmp = _shortcut.WindowStyle;
                if (CheckNeedNotifyPropertyChanged(ref tmp, value))
                {
                    _shortcut.WindowStyle = tmp;
                }
            }
        }
        public string Description
        {
            get => _shortcut.Description;
            set
            {
                var tmp = _shortcut.Description;
                if (CheckNeedNotifyPropertyChanged(ref tmp, value))
                {
                    _shortcut.Description = tmp;
                }
            }
        }
        public string FullName
        {
            get => _shortcut.FullName;
            set
            {
                var tmp = _shortcut.FullName;
                if (CheckNeedNotifyPropertyChanged(ref tmp, value))
                {
                    _shortcut.FullName = tmp;
                }
            }
        }

        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                CheckNeedNotifyPropertyChanged(ref _isSelected, value);
            }
        }
    }
}
