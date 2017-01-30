using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.IO;
using System.Linq;
using YouiApplicationExcercise.Service;
using YouiApplicationExcercise.Service.Contracts;
using YouiApplicationExcercise.Util;

namespace YouiApplicationExcercise.Tests
{
    [TestClass]
    public class FileParserTests
    {
        private FileParser _fileParser = new FileParser();

        [TestMethod]
        public void Should_Yield_Correct_Number_Of_Lines()
        {
            var parserMock = new Mock<IStringToModelParser<object>>();
            parserMock
                .Setup(p => p.Parse(It.IsAny<string>(), It.IsAny<char>()))
                .Returns(new object());

            var fakeFileData = Enumerable.Range(1, 11).Select(i => i.ToString());
            var fakeFileContent = String.Join(Environment.NewLine, fakeFileData);

            using (var reader = new StreamReader(fakeFileContent.ToStream()))
            {
                var results = _fileParser.ParseFile(reader, parserMock.Object);
                results.Should().HaveCount(10);
            }
        }

        [TestMethod]
        public void Should_Yield_0_Results_If_Single_Empty_Line_On_File()
        {
            var parserMock = new Mock<IStringToModelParser<object>>();
            parserMock
                .Setup(p => p.Parse(It.IsAny<string>(), It.IsAny<char>()))
                .Returns(new object());

            var fakeFileContent = "";

            using (var reader = new StreamReader(fakeFileContent.ToStream()))
            {
                var results = _fileParser.ParseFile(reader, parserMock.Object);
                results.Should().HaveCount(0);
            }
        }
    }
}
