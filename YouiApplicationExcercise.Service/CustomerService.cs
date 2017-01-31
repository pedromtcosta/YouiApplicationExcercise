using System;
using System.Collections.Generic;
using System.Linq;
using YouiApplicationExcercise.Model;
using YouiApplicationExcercise.Service.Contracts;

namespace YouiApplicationExcercise.Service
{
    public class CustomerService : ICustomerService
    {
        public IEnumerable<NameFrequency> GetFirstNameFrequency(IEnumerable<Customer> customers)
        {
            return GetNameFrequency(customers, c => c.FirstName);
        }

        public IEnumerable<NameFrequency> GetLastNameFrequency(IEnumerable<Customer> customers)
        {
            return GetNameFrequency(customers, c => c.LastName);
        }

        public IEnumerable<string> GetAddresses(IEnumerable<Customer> customers)
        {
            var addresses = customers
                                .OrderBy(c => c.Address.Substring(c.Address.IndexOf(" ") + 1))
                                .Select(c => !String.IsNullOrEmpty(c.Address) ? c.Address : "(Blank)")
                                .Distinct();

            return addresses;
        }

        private IEnumerable<NameFrequency> GetNameFrequency(IEnumerable<Customer> customers, Func<Customer, string> keySelector)
        {
            var frequencies = customers
                                .GroupBy(keySelector)
                                .Select(g => new NameFrequency
                                {
                                    Name = !String.IsNullOrWhiteSpace(g.Key) ? g.Key : "(Blank)",
                                    Frequency = g.Count()
                                });

            return frequencies.OrderBy(f => f.Name == "(Blank)" ? 1 : 2).ThenByDescending(f => f.Frequency).ThenBy(f => f.Name);
        }
    }
}
