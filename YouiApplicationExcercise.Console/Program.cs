using Ninject;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YouiApplicationExcercise.Model;
using YouiApplicationExcercise.Service;
using YouiApplicationExcercise.Service.Contracts;

namespace YouiApplicationExcercise.Console
{
    public class Program
    {
        #region Default Files

        private static string _defaultDataFile = "data.csv";
        public static string DefaultDataFile
        {
            get { return _defaultDataFile; }
            private set { _defaultDataFile = value; }
        }

        private static string _defaultNameFrequencyFile = "namefrequencies.txt";
        public static string DefaultNameFrequencyFile
        {
            get { return _defaultNameFrequencyFile; }
            private set { _defaultNameFrequencyFile = value; }
        }

        private static string _defaultAddressesFile = "addresses.txt";
        public static string DefaultAddressesFile
        {
            get { return _defaultAddressesFile; }
            private set { _defaultAddressesFile = value; }
        }

        #endregion

        private static IBootstraper _bootstraper = new Bootstraper();
        public static IBootstraper Bootstraper
        {
            get { return _bootstraper; }
            set { _bootstraper = value; }
        }

        public static void Main(string[] args)
        {
            var kernel = new StandardKernel();
            Bootstraper.Start(kernel);

            #region Retrieve args

            var dataFile = GetArgOrDefault(args, 0, DefaultDataFile);
            var namesFrequencyFile = GetArgOrDefault(args, 1, DefaultNameFrequencyFile);
            var addressesFile = GetArgOrDefault(args, 2, DefaultAddressesFile);

            #endregion

            #region Retrieve dependencies

            var fileSystem = kernel.Get<IFileSystem>();
            var fileParser = kernel.Get<IFileParser>();
            var customerParser = kernel.Get<IStringToModelParser<Customer>>();
            var customerService = kernel.Get<ICustomerService>();
            var consoleWriter = kernel.Get<IConsoleWriter>();
            var customerFileService = kernel.Get<ICustomerFileService>();

            #endregion

            if (!fileSystem.FileExists(dataFile))
            {
                consoleWriter.WriteLine("The specified data file '{0}' does not exists", dataFile);
                return;
            }

            IEnumerable<Customer> customers = null;
            try
            {
                using (var reader = fileSystem.OpenFileReader(dataFile, FileMode.Open))
                {
                    customers = fileParser.ParseFile(reader, customerParser).ToList();
                }
            }
            catch
            {
                consoleWriter.WriteLine("An error ocurred while opening the '{0}' file", dataFile);
                return;
            }

            var firstNameFrequency = customerService.GetFirstNameFrequency(customers);
            var lastNameFrequency = customerService.GetLastNameFrequency(customers);
            var addresses = customerService.GetAddresses(customers);

            customerFileService.GenerateNameFrequencyFile(namesFrequencyFile, firstNameFrequency, lastNameFrequency);
            customerFileService.GenerateAddressesFile(addressesFile, addresses);

            consoleWriter.WriteLine("Proccess complete.");
        }

        private static string GetArgOrDefault(string[] args, int index, string defaultValue)
        {
            if (args?.Length >= index + 1)
            {
                return args[index];
            }
            else
            {
                return defaultValue;
            }
        }
    }
}
