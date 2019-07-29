using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SimpleInjector;
using SimpleInjector.Lifestyles;
using StartupConfigurerWin10.ViewModel;
using StartupConfigurerWin10.Model;

namespace StartupConfigurerWin10
{
    static class Program
    {
        private static App _app;
        private static Container _diContainer;

        [STAThread]
        static void Main()
        {
            _app = new App();
            _app.InitializeComponent();

            _diContainer = Bootstrap();
            try
            {
                RunApplication();
            }
            finally
            {
                _diContainer.Dispose();
            }
        }

        private static Container Bootstrap()
        {
            var container = new Container();

            container.Register<IDialogService, DialogServiceWithMessageBox>(Lifestyle.Singleton);
            container.Register<ISelectExecuteFileService, SelectExecuteFileServiceUseDialog>(Lifestyle.Singleton);
            container.Register<IShortcutService, ShortcutService>(Lifestyle.Singleton);
            container.Register<IMainWindowModel, MainWindowModel>(Lifestyle.Singleton);

            container.Register<MainWindowViewModel>(Lifestyle.Singleton);
            container.Register<MainWindow>(Lifestyle.Singleton);

            container.Verify();
            return container;
        }

        private static void RunApplication()
        {
            var mainWindow = _diContainer.GetInstance<MainWindow>();

            _app.Run(mainWindow);
        }
    }
}
