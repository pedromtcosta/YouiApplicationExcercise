using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ninject;
using YouiApplicationExcercise.Service.Contracts;
using YouiApplicationExcercise.Model;
using System.IO;

namespace YouiApplicationExcercise.Console.Tests
{
    [TestClass]
    public class ProgramTests
    {
        private Mock<IBootstraper> _bootstraperMock;
        private Mock<IStringToModelParser<Customer>> _customerParserMock;
        private Mock<ICustomerService> _customerServiceMock;
        private Mock<IFileParser> _fileParserMock;
        private Mock<IFileSystem> _fileSystemMock;
        private Mock<IConsoleWriter> _consoleWriterMock;

        [TestMethod]
        public void Should_Open_Default_Data_File()
        {
            ConfigureMocks();

            _fileSystemMock.Setup(fs => fs.OpenFileReader(It.IsAny<string>(), FileMode.Open));
            _fileSystemMock.Setup(fs => fs.FileExists(It.IsAny<string>())).Returns(true);

            Program.Bootstraper = _bootstraperMock.Object;
            Program.Main(null);

            _fileSystemMock.Verify(fs => fs.OpenFileReader(Program.DefaultDataFile, FileMode.Open), Times.Once);
        }

        [TestMethod]
        public void Should_Open_Data_File()
        {
            ConfigureMocks();

            _fileSystemMock.Setup(fs => fs.OpenFileReader(It.IsAny<string>(), FileMode.Open));
            _fileSystemMock.Setup(fs => fs.FileExists(It.IsAny<string>())).Returns(true);

            Program.Bootstraper = _bootstraperMock.Object;
            Program.Main(new string[] { @"C:\user\documents\data.csv" });

            _fileSystemMock.Verify(fs => fs.OpenFileReader(@"C:\user\documents\data.csv", FileMode.Open), Times.Once);
        }

        [TestMethod]
        public void Should_Print_Message_If_Data_File_Dont_Exists()
        {
            ConfigureMocks();

            _fileSystemMock.Setup(fs => fs.FileExists(It.IsAny<string>())).Returns(false);
            _consoleWriterMock.Setup(c => c.WriteLine(It.IsAny<string>(), It.IsAny<object[]>()));

            Program.Bootstraper = _bootstraperMock.Object;
            Program.Main(new string[] { @"C:\user\documents\data.csv" });

            _consoleWriterMock.Verify(c => c.WriteLine("The specified data file '{0}' does not exists", @"C:\user\documents\data.csv"), Times.Once);
        }

        private void ConfigureMocks()
        {
            _customerServiceMock = new Mock<ICustomerService>();
            _fileParserMock = new Mock<IFileParser>();
            _customerParserMock = new Mock<IStringToModelParser<Customer>>();
            _fileSystemMock = new Mock<IFileSystem>();
            _consoleWriterMock = new Mock<IConsoleWriter>();

            _bootstraperMock = new Mock<IBootstraper>();
            _bootstraperMock
                .Setup(b => b.Start(It.IsAny<IKernel>()))
                .Callback<IKernel>(kernel =>
                {
                    kernel.Bind<ICustomerService>().ToConstant(_customerServiceMock.Object);
                    kernel.Bind<IFileParser>().ToConstant(_fileParserMock.Object);
                    kernel.Bind<IStringToModelParser<Customer>>().ToConstant(_customerParserMock.Object);
                    kernel.Bind<IFileSystem>().ToConstant(_fileSystemMock.Object);
                    kernel.Bind<IConsoleWriter>().ToConstant(_consoleWriterMock.Object);
                });
        }
    }
}
