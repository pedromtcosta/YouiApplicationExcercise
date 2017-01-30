using System.Collections.Generic;
using YouiApplicationExcercise.Model;

namespace YouiApplicationExcercise.Service.Contracts
{
    public interface ICustomerService
    {
        IEnumerable<string> GetAddresses(IEnumerable<Customer> customers);
        IEnumerable<NameFrequency> GetFirstNameFrequency(IEnumerable<Customer> customers);
        IEnumerable<NameFrequency> GetLastNameFrequency(IEnumerable<Customer> customers);
    }
}