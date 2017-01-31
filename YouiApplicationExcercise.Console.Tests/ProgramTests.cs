using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ninject;
using YouiApplicationExcercise.Service.Contracts;
using YouiApplicationExcercise.Model;
using System.IO;
using System.Collections.Generic;

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
        private Mock<ICustomerFileService> _customerFileServiceMock;

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

        [TestMethod]
        public void Should_Print_Error_Message_If_Exception_While_Opening_Data_File()
        {
            ConfigureMocks();

            _fileSystemMock.Setup(fs => fs.FileExists(It.IsAny<string>())).Returns(true);
            _consoleWriterMock.Setup(c => c.WriteLine(It.IsAny<string>(), It.IsAny<object[]>()));
            _fileSystemMock.Setup(fs => fs.OpenFileReader(It.IsAny<string>(), FileMode.Open)).Throws(new Exception());

            Program.Bootstraper = _bootstraperMock.Object;
            Program.Main(new string[] { @"C:\user\documents\data.csv" });

            _consoleWriterMock.Verify(c => c.WriteLine("An error ocurred while opening the '{0}' file", @"C:\user\documents\data.csv"), Times.Once);
        }

        [TestMethod]
        public void Should_Generate_Name_Frequency_File_With_Default_Name()
        {
            ConfigureMocks();

            _fileSystemMock.Setup(fs => fs.OpenFileReader(It.IsAny<string>(), FileMode.Open));
            _fileSystemMock.Setup(fs => fs.FileExists(It.IsAny<string>())).Returns(true);
            _customerFileServiceMock
                .Setup(m => m.GenerateNameFrequencyFile(It.IsAny<string>(),
                                                        It.IsAny<IEnumerable<NameFrequency>>(),
                                                        It.IsAny<IEnumerable<NameFrequency>>()));

            Program.Bootstraper = _bootstraperMock.Object;
            Program.Main(null);

            _customerFileServiceMock
                .Verify(m => m.GenerateNameFrequencyFile(Program.DefaultNameFrequencyFile,
                                                         It.IsAny<IEnumerable<NameFrequency>>(),
                                                         It.IsAny<IEnumerable<NameFrequency>>()), Times.Once);
        }

        [TestMethod]
        public void Should_Generate_Name_Frequency_File()
        {
            ConfigureMocks();

            _fileSystemMock.Setup(fs => fs.OpenFileReader(It.IsAny<string>(), FileMode.Open));
            _fileSystemMock.Setup(fs => fs.FileExists(It.IsAny<string>())).Returns(true);
            _customerFileServiceMock
                .Setup(m => m.GenerateNameFrequencyFile(It.IsAny<string>(),
                                                        It.IsAny<IEnumerable<NameFrequency>>(),
                                                        It.IsAny<IEnumerable<NameFrequency>>()));

            Program.Bootstraper = _bootstraperMock.Object;
            Program.Main(new string[] { "", @"C:\user\documents\namefrequencies.csv" });

            _customerFileServiceMock
                .Verify(m => m.GenerateNameFrequencyFile(@"C:\user\documents\namefrequencies.csv",
                                                         It.IsAny<IEnumerable<NameFrequency>>(),
                                                         It.IsAny<IEnumerable<NameFrequency>>()), Times.Once);
        }

        [TestMethod]
        public void Should_Generate_Addresses_File_With_Default_Name()
        {
            ConfigureMocks();

            _fileSystemMock.Setup(fs => fs.OpenFileReader(It.IsAny<string>(), FileMode.Open));
            _fileSystemMock.Setup(fs => fs.FileExists(It.IsAny<string>())).Returns(true);
            _customerFileServiceMock
                .Setup(m => m.GenerateAddressesFile(It.IsAny<string>(),
                                                    It.IsAny<IEnumerable<string>>()));

            Program.Bootstraper = _bootstraperMock.Object;
            Program.Main(null);

            _customerFileServiceMock
                .Verify(m => m.GenerateAddressesFile(Program.DefaultAddressesFile,
                                                     It.IsAny<IEnumerable<string>>()), Times.Once);
        }

        [TestMethod]
        public void Should_Generate_Addresses_File()
        {
            ConfigureMocks();

            _fileSystemMock.Setup(fs => fs.OpenFileReader(It.IsAny<string>(), FileMode.Open));
            _fileSystemMock.Setup(fs => fs.FileExists(It.IsAny<string>())).Returns(true);
            _customerFileServiceMock
                .Setup(m => m.GenerateAddressesFile(It.IsAny<string>(),
                                                    It.IsAny<IEnumerable<string>>()));

            Program.Bootstraper = _bootstraperMock.Object;
            Program.Main(new string[] { "", "", @"C:\user\documents\addresses.csv" });

            _customerFileServiceMock
                .Verify(m => m.GenerateAddressesFile(@"C:\user\documents\addresses.csv",
                                                       It.IsAny<IEnumerable<string>>()), Times.Once);
        }

        private void ConfigureMocks()
        {
            _customerServiceMock = new Mock<ICustomerService>();
            _fileParserMock = new Mock<IFileParser>();
            _customerParserMock = new Mock<IStringToModelParser<Customer>>();
            _fileSystemMock = new Mock<IFileSystem>();
            _consoleWriterMock = new Mock<IConsoleWriter>();
            _customerFileServiceMock = new Mock<ICustomerFileService>();

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
                    kernel.Bind<ICustomerFileService>().ToConstant(_customerFileServiceMock.Object);
                });
        }
    }
}
