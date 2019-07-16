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

namespace StartupConfigurerWin10.Tests
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
        public void ConstructorTest()
        {
            var modelMock = new Mock<IMainWindowModel>();
            modelMock
                .Setup(m => m.GetStartupShortcuts())
                .Returns(_samples);

            var vm = new MainWindowViewModel(modelMock.Object);

            Assert.Equal(-1, vm.SelectedShortcutIndex.Value);
            Assert.Equal(_samples, vm.StartupShortcuts.OfType<IShortcut>());
        }

        [Fact]
        public void AddCommandTest()
        {
            var modelMock = new Mock<IMainWindowModel>();
            modelMock
                .Setup(m => m.NewStartupShortcut())
                .Returns(_samples);

            var vm = new MainWindowViewModel(modelMock.Object);
            vm.AddCommand.Execute(null);

            Assert.Equal(_samples, vm.StartupShortcuts);
        }

        [Fact]
        public void RemoveCommandTest()
        {
            var modelMock = new Mock<IMainWindowModel>();
            modelMock
                .Setup(m => m.GetStartupShortcuts())
                .Returns(_samples);

            var vm = new MainWindowViewModel(modelMock.Object);
            vm.SelectedShortcutIndex.Subscribe(val =>
            {
                // RemoveCommand で -1 に書き換えられる
                if (val == -1) return;

                vm.RemoveCommand.Execute(null);

                Assert.Equal(_samples.Skip(1), vm.StartupShortcuts);
                Assert.Equal(-1, vm.SelectedShortcutIndex.Value);
            });

            vm.SelectedShortcutIndex.Value = 0;
        }

        [Fact]
        public void SaveCommandTest()
        {
            var modelMock = new Mock<IMainWindowModel>();
            modelMock
                .Setup(m => m.GetStartupShortcuts())
                .Returns(_samples);

            var vm = new MainWindowViewModel(modelMock.Object);
            // 削除 -> 保存

            vm.SelectedShortcutIndex.Subscribe(val =>
            {
                // RemoveCommand で -1 に書き換えられる
                if (val == -1) return;

                vm.RemoveCommand.Execute(null);
                vm.SaveCommand.Execute(null);

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
            });

            vm.SelectedShortcutIndex.Value = 0;
        }
    }
}
