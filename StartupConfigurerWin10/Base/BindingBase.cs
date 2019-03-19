using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace StartupConfigurerWin10.Base
{
    public class BindingBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string param) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(param));

        protected bool CheckNeedNotifyPropertyChanged<T>(
            ref T target,
            T value,
            [CallerMemberName]string paramName = "")
        {
            if (value == null) return false;
            if (!value.Equals(target))
            {
                target = value;
                NotifyPropertyChanged(paramName);
                return true;
            }
            return false;
        }
    }
}
