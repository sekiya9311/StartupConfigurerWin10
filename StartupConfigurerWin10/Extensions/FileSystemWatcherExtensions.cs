using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartupConfigurerWin10.Extensions
{
    public static class FileSystemWatcherExtensions
    {
        public static IObservable<FileSystemEventArgs> CreatedAsObservable(this FileSystemWatcher watcher)
            => Observable.FromEvent<FileSystemEventHandler, FileSystemEventArgs>(
                h => (s, e) => h(e), h => watcher.Created += h, h => watcher.Created -= h);

        public static IObservable<FileSystemEventArgs> ChangedAsObservable(this FileSystemWatcher watcher)
            => Observable.FromEvent<FileSystemEventHandler, FileSystemEventArgs>(
                h => (s, e) => h(e), h => watcher.Changed += h, h => watcher.Changed -= h);

        public static IObservable<FileSystemEventArgs> DeletedAsObservable(this FileSystemWatcher watcher)
            => Observable.FromEvent<FileSystemEventHandler, FileSystemEventArgs>(
                h => (s, e) => h(e), h => watcher.Deleted += h, h => watcher.Deleted -= h);

        public static IObservable<RenamedEventArgs> RenamedAsObservable(this FileSystemWatcher watcher)
            => Observable.FromEvent<RenamedEventHandler, RenamedEventArgs>(
                h => (s, e) => h(e), h => watcher.Renamed += h, h => watcher.Renamed -= h);
    }
}
