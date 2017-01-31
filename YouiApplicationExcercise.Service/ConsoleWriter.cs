using System;
using YouiApplicationExcercise.Service.Contracts;

namespace YouiApplicationExcercise.Service
{
    public class ConsoleWriter : IConsoleWriter
    {
        public void WriteLine(string format, params object[] args)
        {
            Console.WriteLine(format, args);
        }
    }
}
