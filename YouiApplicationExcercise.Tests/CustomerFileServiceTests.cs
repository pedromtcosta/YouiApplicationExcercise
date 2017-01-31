using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using YouiApplicationExcercise.Model;
using YouiApplicationExcercise.Service;
using YouiApplicationExcercise.Service.Contracts;

namespace YouiApplicationExcercise.Tests
{
    [TestClass]
    public class CustomerFileServiceTests
    {
        private Mock<IFileSystem> _fileSystemMock;
        private CustomerFileService _svc;

        [TestInitialize]
        public void Setup()
        {
            _fileSystemMock = new Mock<IFileSystem>();

            _svc = new CustomerFileService(_fileSystemMock.Object);
        }

        [TestMethod]
        public void Should_Create_Name_Frequency_File_With_Correct_Content()
        {
            _fileSystemMock
                .Setup(fs => fs.WriteAllTextToFile(It.IsAny<string>(), It.IsAny<string>()));

            var firstNameFrequencies = new List<NameFrequency>
            {
                new NameFrequency { Name = "Jimmy", Frequency = 3 },
                new NameFrequency { Name = "Clive", Frequency = 2 }
            };

            var lastNameFrequencies = new List<NameFrequency>
            {
                new NameFrequency { Name = "Smith", Frequency = 3 },
                new NameFrequency { Name = "Owen", Frequency = 2 }
            };

            var expectedFileContent = String.Join(Environment.NewLine,
                "===== First Name Frequency =====",
                "Jimmy - 3",
                "Clive - 2",
                "",
                "===== Last Name Frequency =====",
                "Smith - 3",
                "Owen - 2");

            _svc.GenerateNameFrequencyFile("names.txt", firstNameFrequencies, lastNameFrequencies);

            _fileSystemMock.Verify(fs => fs.WriteAllTextToFile("names.txt", expectedFileContent));
        }

        [TestMethod]
        public void Should_Create_Addresses_File_With_Correct_Content()
        {
            _fileSystemMock
                .Setup(fs => fs.WriteAllTextToFile(It.IsAny<string>(), It.IsAny<string>()));

            var addresses = new string[]
            {
                "65 Ambling Way",
                "102 Long Lane",
                "82 Stewart St"
            };

            var expectedFileContent = String.Join(Environment.NewLine,
                "===== Addresses =====",
                "65 Ambling Way",
                "102 Long Lane",
                "82 Stewart St");

            _svc.GenerateAddressesFile("addresses.txt", addresses);

            _fileSystemMock.Verify(fs => fs.WriteAllTextToFile("addresses.txt", expectedFileContent));
        }
    }
}
