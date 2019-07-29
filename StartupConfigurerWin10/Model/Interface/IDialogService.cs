using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartupConfigurerWin10.Model
{
    public interface IDialogService
    {
        void ShowMessage(string message, string caption = "");
    }
}
