using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using StartupConfigurerWin10;
using StartupConfigurerWin10.Entity;
using StartupConfigurerWin10.Model;
using StartupConfigurerWin10.ViewModel;

using Moq;
using Xunit;

namespace StartupConfigureTestWin10.Tests
{
    public class MainWindowViewModelTest
    {
        private readonly IEnumerable<IShortcut> _samples = new[]
        {
            new Shortcut()
            {
                Arguments = "arg1",
                Description = "desc1",
                FullName = "full1",
                IconLocation = "icon1",
                TargetPath = "target1",
                WindowStyle = WindowStyle.Normal,
                WorkingDirectory = "work1"
            },
            new Shortcut()
            {
                Arguments = "arg2",
                Description = "desc2",
                FullName = "full2",
                IconLocation = "icon2",
                TargetPath = "target2",
                WindowStyle = WindowStyle.Maximum,
                WorkingDirectory = "work2"
            }
        };

        [Fact]
        public void Test()
        {
            var modelMock = new Mock<IMainWindowModel>();
            modelMock
                .Setup(m => m.GetStartupShortcuts())
                .Returns(_samples.Take(1));
            modelMock
                .Setup(m => m.NewStartupShortcut())
                .Returns(_samples.Skip(1));

            using (var vm = new MainWindowViewModel(modelMock.Object))
            {
                Assert.Equal(-1, vm.SelectedShortcutIndex.Value);
                Assert.Equal(_samples.Take(1), vm.StartupShortcuts);

                vm.AddCommand.Execute();
                Assert.Equal(_samples, vm.StartupShortcuts);

                vm.SelectedShortcutIndex.Value = 0;
                vm.RemoveCommand.Execute();
                Assert.Equal(_samples.Skip(1), vm.StartupShortcuts);
                Assert.Equal(-1, vm.SelectedShortcutIndex.Value);

                vm.SaveCommand.Execute();
                modelMock.Verify(
                    m => m.DeleteStartupShortcuts(
                        It.Is<IEnumerable<IShortcut>>(
                            a => a.SequenceEqual(_samples.Take(1)))),
                    Times.Once());
                modelMock.Verify(
                    m => m.SaveStartupShortcuts(
                        It.Is<IEnumerable<IShortcut>>(
                            a => a.SequenceEqual(_samples.Skip(1)))),
                    Times.Once());
            }
        }
    }
}
