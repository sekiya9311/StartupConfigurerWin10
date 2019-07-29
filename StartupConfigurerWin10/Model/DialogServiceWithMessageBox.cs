using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;

namespace StartupConfigurerWin10.Model
{
    class DialogServiceWithMessageBox : IDialogService
    {
        public void ShowMessage(string message, string caption = "")
        {
            MessageBox.Show(message, caption);
        }
    }
}
