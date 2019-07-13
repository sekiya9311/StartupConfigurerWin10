using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Reactive.Bindings;

namespace StartupConfigurerWin10.Entity
{
    public class ShortcutForDisplay : IShortcut, IEquatable<ShortcutForDisplay>, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ShortcutForDisplay(IShortcut shortcut)
        {
            IconLocation = shortcut.IconLocation;
            TargetPath = shortcut.TargetPath;
            Arguments = shortcut.Arguments;
            WorkingDirectory = shortcut.WorkingDirectory;
            WindowStyle = shortcut.WindowStyle;
            Description = shortcut.Description;
            FullName = shortcut.FullName;
        }

        private ReactivePropertySlim<string> _iconLocation = new ReactivePropertySlim<string>();
        public string IconLocation { get => _iconLocation.Value; set => _iconLocation.Value = value; }

        private ReactivePropertySlim<string> _targetPath = new ReactivePropertySlim<string>();
        public string TargetPath { get => _targetPath.Value; set => _targetPath.Value = value; }

        private ReactivePropertySlim<string> _arguments = new ReactivePropertySlim<string>();
        public string Arguments { get => _arguments.Value; set => _arguments.Value = value; }

        private ReactivePropertySlim<string> _workingDirectory = new ReactivePropertySlim<string>();
        public string WorkingDirectory { get => _workingDirectory.Value; set => _workingDirectory.Value = value; }

        private ReactivePropertySlim<WindowStyle> _windowStyle = new ReactivePropertySlim<WindowStyle>();
        public WindowStyle WindowStyle { get => _windowStyle.Value; set => _windowStyle.Value = value; }

        private ReactivePropertySlim<string> _description = new ReactivePropertySlim<string>();
        public string Description { get => _description.Value; set => _description.Value = value; }

        private ReactivePropertySlim<string> _fullName = new ReactivePropertySlim<string>();
        public string FullName { get => _fullName.Value; set => _fullName.Value = value; }

        public string FileName => System.IO.Path.GetFileNameWithoutExtension(FullName);

        private ReactivePropertySlim<bool> _delete = new ReactivePropertySlim<bool>();
        public bool Delete { get => _delete.Value; set => _delete.Value = value; }

        public bool Equals(ShortcutForDisplay other)
        {
            if (IconLocation != other.IconLocation) return false;
            if (TargetPath != other.TargetPath) return false;
            if (Arguments != other.Arguments) return false;
            if (WorkingDirectory != other.WorkingDirectory) return false;
            if (WindowStyle != other.WindowStyle) return false;
            if (Description != other.Description) return false;
            if (FullName != other.FullName) return false;
            if (FileName != other.FileName) return false;

            return true;
        }
    }
}
