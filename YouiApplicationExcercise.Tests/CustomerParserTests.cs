using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using YouiApplicationExcercise.Service;
using FluentAssertions;
using YouiApplicationExcercise.Model;

namespace YouiApplicationExcercise.Tests
{
    [TestClass]
    public class CustomerParserTests
    {
        private CustomerParser _parser = new CustomerParser();

        [TestMethod]
        public void Should_Parse_String_To_Customer()
        {
            var input = "Jimmy,Smith,102 Long Lane,29384857";
            var customer = _parser.Parse(input);
            customer.ShouldBeEquivalentTo(new Customer
            {
                FirstName = "Jimmy",
                LastName = "Smith",
                Address = "102 Long Lane",
                PhoneNumber = "29384857"
            });
        }

        [TestMethod]
        public void Should_Parse_String_To_Customer_With_Empty_Properties()
        {
            var input = "Jimmy,Smith,102 Long Lane,";
            var customer = _parser.Parse(input);
            customer.ShouldBeEquivalentTo(new Customer
            {
                FirstName = "Jimmy",
                LastName = "Smith",
                Address = "102 Long Lane",
                PhoneNumber = String.Empty
            });
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void Should_Throw_Format_Excetion_If_Fields_Missing()
        {
            var input = "Jimmy,Smith,102 Long Lane";
            var customer = _parser.Parse(input);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void Should_Throw_Format_Excetion_If_Input_Have_More_Fields_Than_Expected()
        {
            var input = "Jimmy,Smith,102 Long Lane,5780540,405754500";
            var customer = _parser.Parse(input);
        }
    }
}
