using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using StartupConfigurerWin10.Model;
using StartupConfigurerWin10.Entity;

using Moq;
using Xunit;

namespace StartupConfigurerWin10.Tests
{
    public class MainWindowModelTest
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
            },
            new Shortcut()
            {
                Arguments = "arg3",
                Description = "desc3",
                FullName = "full3",
                IconLocation = "icon3",
                TargetPath = "target3",
                WindowStyle = WindowStyle.Minimum,
                WorkingDirectory = "work3"
            }
        };

        [Fact]
        public void GetStartupShortcutsTest()
        {
            var mockSelectExecuteFileService = new Mock<ISelectExecuteFileService>();
            var mockShortcutService = new Mock<IShortcutService>();
            var mockDialogService = new Mock<IDialogService>();

            var model = new MainWindowModel(
                mockSelectExecuteFileService.Object,
                mockShortcutService.Object,
                mockDialogService.Object);

            mockShortcutService
                .Setup(m => m.GetShortcuts(MainWindowModel.StartupPath))
                .Returns(_samples);
            Assert.Equal(_samples, model.GetStartupShortcuts());
        }

        [Fact]
        public void NewStartupShortcutTest()
        {
            var mockSelectExecuteFileService = new Mock<ISelectExecuteFileService>();
            var mockShortcutService = new Mock<IShortcutService>();
            var mockDialogService = new Mock<IDialogService>();

            var model = new MainWindowModel(
                mockSelectExecuteFileService.Object,
                mockShortcutService.Object,
                mockDialogService.Object);

            mockSelectExecuteFileService
                .Setup(m => m.SelectExecuteFiles())
                .Returns(new[] { "hoge" });
            Assert.Equal(
                new[]
                {
                    new Shortcut()
                    {
                        Arguments = "",
                        Description = "Make by StartupConfigurerWin10",
                        FullName = System.IO.Path.Combine(MainWindowModel.StartupPath, "hoge.lnk"),
                        IconLocation = "hoge,0",
                        TargetPath= "hoge",
                        WindowStyle = WindowStyle.Normal,
                        WorkingDirectory = ""
                    }
                },
                model.NewStartupShortcut());
        }

        [Fact]
        public void SaveStartupShortcuts()
        {
            var mockSelectExecuteFileService = new Mock<ISelectExecuteFileService>();
            var mockShortcutService = new Mock<IShortcutService>();
            var mockDialogService = new Mock<IDialogService>();

            var model = new MainWindowModel(
                mockSelectExecuteFileService.Object,
                mockShortcutService.Object,
                mockDialogService.Object);

            model.SaveStartupShortcuts(_samples);

            mockShortcutService.Verify(
                m => m.SaveShortcuts(
                    MainWindowModel.StartupPath,
                    It.Is<IEnumerable<IShortcut>>(
                        a => a.SequenceEqual(_samples))),
                Times.Once());
            mockDialogService.Verify(
                s => s.ShowMessage(It.IsAny<string>(), It.IsAny<string>()),
                Times.Once());
        }

        [Fact]
        public void DeleteStartupShortcutsTest()
        {
            var mockSelectExecuteFileService = new Mock<ISelectExecuteFileService>();
            var mockShortcutService = new Mock<IShortcutService>();
            var mockDialogService = new Mock<IDialogService>();

            var model = new MainWindowModel(
                mockSelectExecuteFileService.Object,
                mockShortcutService.Object,
                mockDialogService.Object);

            model.DeleteStartupShortcuts(_samples);
            mockShortcutService.Verify(
                m => m.DeleteShortcuts(
                    MainWindowModel.StartupPath,
                    It.Is<IEnumerable<IShortcut>>(
                        a => a.SequenceEqual(_samples))),
                Times.Once());
        }
    }
}
