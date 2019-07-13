using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Win32;

namespace StartupConfigurerWin10.Model
{
    class SelectExecuteFileServiceUseDialog : ISelectExecuteFileService
    {
        public string SelectExecuteFile()
        {
            var selectedFiles = SelectUseDialog(false);

            return selectedFiles.DefaultIfEmpty(string.Empty).First();
        }

        public IEnumerable<string> SelectExecuteFiles()
        {
            var selectedFiles = SelectUseDialog(true);

            return selectedFiles;
        }

        private IEnumerable<string> SelectUseDialog(bool multiSelect)
        {
            var dialog = new OpenFileDialog()
            {
                AddExtension = true,
                CheckFileExists = true,
                CheckPathExists = true,
                Multiselect = multiSelect,
                DefaultExt = "実行ファイル|*.exe"
            };
            
            var selected = dialog.ShowDialog() ?? false;
            if (!selected)
            {
                return Enumerable.Empty<string>();
            }
            
            return dialog.FileNames.Clone() as IEnumerable<string>;
        }
    }
}
