using System;
using YouiApplicationExcercise.Model;
using YouiApplicationExcercise.Service.Contracts;

namespace YouiApplicationExcercise.Service
{
    public class CustomerParser : IStringToModelParser<Customer>
    {
        public Customer Parse(string input, char delimiter = ',')
        {
            var splitedInput = input.Split(delimiter);

            if (splitedInput.Length != 4)
            {
                throw new FormatException("The line must have exactly 4 fiels");
            }

            return new Customer
            {
                FirstName = splitedInput[0],
                LastName = splitedInput[1],
                Address = splitedInput[2],
                PhoneNumber = splitedInput[3]
            };
        }
    }
}
