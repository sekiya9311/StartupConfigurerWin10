using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using StartupConfigurerWin10;
using StartupConfigurerWin10.Model;
using StartupConfigurerWin10.ViewModel;

using Moq;
using Xunit;

namespace StartupConfigureTestWin10.Tests
{
    public class MainWindowViewModelTest
    {
        [Fact]
        public void Test()
        {
            var modelMock = new Mock<IMainWindowModel>();
            var vm = new MainWindowViewModel(modelMock.Object);

            Assert.Equal(modelMock.Object, vm.Model);

            Assert.True(vm.AddCommand.CanExecute(null));
            vm.AddCommand.Execute(null);
            modelMock.Verify(m => m.AddStartup(), Times.Once);

            Assert.True(vm.RemoveCommand.CanExecute(null));
            vm.RemoveCommand.Execute(null);
            modelMock.Verify(m => m.Remove(), Times.Once);

            Assert.True(vm.SaveCommand.CanExecute(null));
            vm.SaveCommand.Execute(null);
            modelMock.Verify(m => m.Save(), Times.Once);
        }
    }
}
