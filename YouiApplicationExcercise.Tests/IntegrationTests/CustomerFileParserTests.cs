using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YouiApplicationExcercise.Model;
using YouiApplicationExcercise.Service;
using YouiApplicationExcercise.Util;

namespace YouiApplicationExcercise.Tests.IntegrationTests
{
    [TestClass]
    public class CustomerFileParserTests
    {
        private FileParser _fileParser = new FileParser();
        private CustomerParser _customerParser = new CustomerParser();

        [TestMethod]
        public void Should_Return_Correct_Customers()
        {
            var header = "FirstName,LastName,Address,PhoneNumber";
            var secondLine = "Jimmy,Smith,102 Long Lane,29384857";
            var thirdLine = "Clive,Owen,65 Ambling Way,31214788";

            var fakeFile = String.Join(Environment.NewLine, header, secondLine, thirdLine);

            var results = _fileParser.ParseFile(new StreamReader(fakeFile.ToStream()), _customerParser).ToList();

            results[0].ShouldBeEquivalentTo(new Customer
            {
                FirstName = "Jimmy",
                LastName = "Smith",
                Address = "102 Long Lane",
                PhoneNumber = "29384857"
            });

            results[1].ShouldBeEquivalentTo(new Customer
            {
                FirstName = "Clive",
                LastName = "Owen",
                Address = "65 Ambling Way",
                PhoneNumber = "31214788"
            });
        }

        [TestMethod]
        public void Should_Return_0_Results_If_File_Have_Only_Header()
        {
            var header = "FirstName,LastName,Address,PhoneNumber";
            var results = _fileParser.ParseFile(new StreamReader(header.ToStream()), _customerParser).ToList();
            results.Should().HaveCount(0);
        }
    }
}
