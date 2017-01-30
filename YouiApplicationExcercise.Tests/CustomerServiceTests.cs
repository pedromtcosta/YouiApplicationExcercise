using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YouiApplicationExcercise.Model;
using YouiApplicationExcercise.Service;

namespace YouiApplicationExcercise.Tests
{
    [TestClass]
    public class CustomerServiceTests
    {
        private CustomerService _svc = new CustomerService();

        [TestMethod]
        public void Should_Return_Correct_FirstName_Frequency()
        {
            var customers = new List<Customer>
            {
                new Customer { FirstName = "Jimmy" },
                new Customer { FirstName = "Clive" },
                new Customer { FirstName = "James" },
                new Customer { FirstName = "Graham" },
                new Customer { FirstName = "John" },
                new Customer { FirstName = "Clive" },
                new Customer { FirstName = "James" },
                new Customer { FirstName = "Graham" }
            };

            var frequencies = _svc.GetFirstNameFrequency(customers);

            frequencies.Should().HaveCount(5);
            frequencies.First(c => c.Name == "Jimmy").Frequency.Should().Be(1);
            frequencies.First(c => c.Name == "Clive").Frequency.Should().Be(2);
            frequencies.First(c => c.Name == "James").Frequency.Should().Be(2);
            frequencies.First(c => c.Name == "Graham").Frequency.Should().Be(2);
            frequencies.First(c => c.Name == "John").Frequency.Should().Be(1);
        }

        [TestMethod]
        public void Should_Return_Correct_LastName_Frequency()
        {
            var customers = new List<Customer>
            {
                new Customer { LastName = "Smith" },
                new Customer { LastName = "Owen" },
                new Customer { LastName = "Brown" },
                new Customer { LastName = "Howe" },
                new Customer { LastName = "Howe" },
                new Customer { LastName = "Smith" },
                new Customer { LastName = "Owen" },
                new Customer { LastName = "Brown" }
            };

            var frequencies = _svc.GetLastNameFrequency(customers);

            frequencies.Should().HaveCount(4);
            frequencies.First(c => c.Name == "Smith").Frequency.Should().Be(2);
            frequencies.First(c => c.Name == "Owen").Frequency.Should().Be(2);
            frequencies.First(c => c.Name == "Brown").Frequency.Should().Be(2);
            frequencies.First(c => c.Name == "Howe").Frequency.Should().Be(2);
        }

        [TestMethod]
        public void Should_Return_Name_Filled_With_Blank_When_Name_Is_Empty()
        {
            var customers = new List<Customer>
            {
                new Customer { FirstName = "" },
            };

            var frequencies = _svc.GetFirstNameFrequency(customers);

            frequencies.First().Name.Should().Be("(Blank)");
        }

        [TestMethod]
        public void Should_Return_FirstNames_Ordered_By_Frequency()
        {
            var customers = new List<Customer>
            {
                new Customer { FirstName = "Clive" },
                new Customer { FirstName = "Clive" },
                new Customer { FirstName = "Clive" },
                new Customer { FirstName = "James" },
                new Customer { FirstName = "James" },
                new Customer { FirstName = "Graham" },
                new Customer { FirstName = "Jimmy" },
                new Customer { FirstName = "Jimmy" },
                new Customer { FirstName = "Jimmy" },
                new Customer { FirstName = "Jimmy" }
            };

            var frequencies = _svc.GetFirstNameFrequency(customers).ToArray();

            frequencies[0].Name.Should().Be("Jimmy");
            frequencies[1].Name.Should().Be("Clive");
            frequencies[2].Name.Should().Be("James");
            frequencies[3].Name.Should().Be("Graham");
        }

        [TestMethod]
        public void Should_Return_LastNames_Ordered_By_Frequency()
        {
            var customers = new List<Customer>
            {
                new Customer { LastName = "Smith" },
                new Customer { LastName = "Owen" },
                new Customer { LastName = "Owen" },
                new Customer { LastName = "Brown" },
                new Customer { LastName = "Brown" },
                new Customer { LastName = "Brown" },
                new Customer { LastName = "Howe" },
                new Customer { LastName = "Howe" },
                new Customer { LastName = "Howe" },
                new Customer { LastName = "Howe" }
            };

            var frequencies = _svc.GetLastNameFrequency(customers).ToArray();

            frequencies[0].Name.Should().Be("Howe");
            frequencies[1].Name.Should().Be("Brown");
            frequencies[2].Name.Should().Be("Owen");
            frequencies[3].Name.Should().Be("Smith");
        }

        [TestMethod]
        public void Should_Return_FirstNames_Ordered_Alphabetically()
        {
            var customers = new List<Customer>
            {
                new Customer { FirstName = "James" },
                new Customer { FirstName = "Clive" },
                new Customer { FirstName = "Graham" },
                new Customer { FirstName = "Jimmy" }
            };

            var frequencies = _svc.GetFirstNameFrequency(customers).ToArray();

            frequencies[0].Name.Should().Be("Clive");
            frequencies[1].Name.Should().Be("Graham");
            frequencies[2].Name.Should().Be("James");
            frequencies[3].Name.Should().Be("Jimmy");
        }

        [TestMethod]
        public void Should_Return_LastNames_Ordered_Alphabetically()
        {
            var customers = new List<Customer>
            {
                new Customer { LastName = "Smith" },
                new Customer { LastName = "Owen" },
                new Customer { LastName = "Brown" },
                new Customer { LastName = "Howe" },
            };

            var frequencies = _svc.GetLastNameFrequency(customers).ToArray();

            frequencies[0].Name.Should().Be("Brown");
            frequencies[1].Name.Should().Be("Howe");
            frequencies[2].Name.Should().Be("Owen");
            frequencies[3].Name.Should().Be("Smith");
        }

        [TestMethod]
        public void Should_Return_No_Duplicate_Addresses()
        {
            var customers = new List<Customer>
            {
                new Customer { Address = "102 Long Lane" },
                new Customer { Address = "102 Long Lane" },
                new Customer { Address = "65 Ambling Way" }
            };

            var addresses = _svc.GetAddresses(customers);

            addresses.Should().HaveCount(2);
        }

        [TestMethod]
        public void Should_Return_Addresses_Ordered_By_Street_Name_Alphabetically()
        {
            var customers = new List<Customer>
            {
                new Customer { Address = "102 Long Lane" },
                new Customer { Address = "65 Ambling Way" },
                new Customer { Address = "82 Stewart St" }
            };

            var addresses = _svc.GetAddresses(customers).ToArray();

            addresses[0].Should().Be("65 Ambling Way");
            addresses[1].Should().Be("102 Long Lane");
            addresses[2].Should().Be("82 Stewart St");
        }

        [TestMethod]
        public void Should_Return_Blank_If_Address_Is_Empty()
        {
            var customers = new List<Customer>
            {
                new Customer { Address = "" }
            };

            var addresses = _svc.GetAddresses(customers).ToArray();

            addresses[0].Should().Be("(Blank)");
        }
    }
}
