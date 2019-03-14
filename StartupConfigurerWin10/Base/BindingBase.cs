using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace StartupConfigurerWin10.Base
{
    class BindingBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string param) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(param));
    }
}
