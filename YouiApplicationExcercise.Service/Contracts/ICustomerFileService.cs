using System.Collections.Generic;
using YouiApplicationExcercise.Model;

namespace YouiApplicationExcercise.Service.Contracts
{
    public interface ICustomerFileService
    {
        void GenerateNameFrequencyFile(string fileName, IEnumerable<NameFrequency> firstNameFrequencies, IEnumerable<NameFrequency> lastNameFrequencies);
        void GenerateAddressesFile(string fileName, IEnumerable<string> addresses);
    }
}
