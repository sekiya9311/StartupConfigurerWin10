using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using StartupConfigurerWin10.Base;

namespace StartupConfigurerWin10.Entity
{
    public class ShortcutForDisplay : BindingBase, IShortcut
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
                if (_shortcut.IconLocation != value)
                {
                    _shortcut.IconLocation = value;
                    NotifyPropertyChanged(nameof(IconLocation));
                }
            }
        }
        public string TargetPath
        {
            get => _shortcut.TargetPath;
            set
            {
                if (_shortcut.TargetPath != value)
                {
                    _shortcut.TargetPath = value;
                    NotifyPropertyChanged(nameof(TargetPath));
                }
            }
        }
        public string Arguments
        {
            get => _shortcut.Arguments;
            set
            {
                if (_shortcut.Arguments != value)
                {
                    _shortcut.Arguments = value;
                    NotifyPropertyChanged(nameof(Arguments));
                }
            }
        }
        public string WorkingDirectory
        {
            get => _shortcut.WorkingDirectory;
            set
            {
                if (_shortcut.WorkingDirectory != value)
                {
                    _shortcut.WorkingDirectory = value;
                    NotifyPropertyChanged(nameof(WorkingDirectory));
                }
            }
        }
        public WindowStyle WindowStyle
        {
            get => _shortcut.WindowStyle;
            set
            {
                if (_shortcut.WindowStyle != value)
                {
                    _shortcut.WindowStyle = value;
                    NotifyPropertyChanged(nameof(WindowStyle));
                }
            }
        }
        public string Description
        {
            get => _shortcut.Description;
            set
            {
                if (_shortcut.Description != value)
                {
                    _shortcut.Description = value;
                    NotifyPropertyChanged(nameof(Description));
                }
            }
        }
        public string FullName
        {
            get => _shortcut.FullName;
            set
            {
                if (_shortcut.FullName != value)
                {
                    _shortcut.FullName = value;
                    NotifyPropertyChanged(nameof(FullName));
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
