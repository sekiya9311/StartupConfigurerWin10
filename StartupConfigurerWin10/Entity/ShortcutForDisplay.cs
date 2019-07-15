using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Media.Imaging;
using System.Windows.Interop;
using System.IO;

using Reactive.Bindings;

namespace StartupConfigurerWin10.Entity
{
    public class ShortcutForDisplay : IShortcut, INotifyPropertyChanged
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

        public BitmapSource IconImageSource
        {
            get
            {
                if (!File.Exists(TargetPath))
                {
                    return null;
                }

                using (var icon = Icon.ExtractAssociatedIcon(TargetPath))
                using (var iconBmp = icon.ToBitmap())
                {
                    var handle = iconBmp.GetHbitmap();
                    try
                    {
                        return Imaging.CreateBitmapSourceFromHBitmap(
                            handle,
                            IntPtr.Zero,
                            System.Windows.Int32Rect.Empty,
                            BitmapSizeOptions.FromEmptyOptions());
                    }
                    finally
                    {
                        DeleteObject(handle);
                    }
                }
            }
        }

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

        [DllImport("gdi32.dll", EntryPoint = "DeleteObject")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool DeleteObject([In] IntPtr hObject);
    }
}
