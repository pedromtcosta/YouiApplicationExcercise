using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YouiApplicationExcercise.Model;
using YouiApplicationExcercise.Service.Contracts;

namespace YouiApplicationExcercise.Service
{
    public class CustomerFileService : ICustomerFileService
    {
        private IFileSystem _fileSystem;

        public CustomerFileService(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }

        public void GenerateAddressesFile(string fileName, IEnumerable<string> addresses)
        {
            var lines = new List<string>();
            lines.Add("===== Addresses =====");

            foreach (var address in addresses)
            {
                lines.Add(address);
            }

            var fileContent = String.Join(Environment.NewLine, lines);

            _fileSystem.WriteAllTextToFile(fileName, fileContent);
        }

        public void GenerateNameFrequencyFile(string fileName, IEnumerable<NameFrequency> firstNameFrequencies, IEnumerable<NameFrequency> lastNameFrequencies)
        {
            var lines = new List<string>();

            lines.Add("===== First Name Frequency =====");
            foreach (var frequency in firstNameFrequencies)
            {
                lines.Add($"{frequency.Name} - {frequency.Frequency}");
            }

            lines.Add(String.Empty);

            lines.Add("===== Last Name Frequency =====");
            foreach (var frequency in lastNameFrequencies)
            {
                lines.Add($"{frequency.Name} - {frequency.Frequency}");
            }

            var fileContent = String.Join(Environment.NewLine, lines);

            _fileSystem.WriteAllTextToFile(fileName, fileContent);
        }
    }
}
