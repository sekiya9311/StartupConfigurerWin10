using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StartupConfigurerWin10
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // TODO: DIしたい...
            var m = new Model.MainWindowModel(
                new Model.SelectExecuteFileServiceUseDialog(),
                new Model.ShortcutService(),
                new Model.DialogServiceWithMessageBox());
            var vm = new ViewModel.MainWindowViewModel(m);

            this.DataContext = vm;
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            (this.DataContext as IDisposable)?.Dispose();
        }
    }
}
