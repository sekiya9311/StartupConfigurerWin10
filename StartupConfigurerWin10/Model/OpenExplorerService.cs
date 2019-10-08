using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartupConfigurerWin10.Model
{
    public class OpenExplorerService : IOpenExolorerService
    {
        public void Open(string path)
        {
            System.Diagnostics.Process.Start("explorer.exe", path);
        }
    }
}
