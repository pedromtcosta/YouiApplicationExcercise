using System.Collections.Generic;
using System.IO;

namespace YouiApplicationExcercise.Service.Contracts
{
    public interface IFileParser
    {
        IEnumerable<T> ParseFile<T>(StreamReader reader, IStringToModelParser<T> lineParser, char delimiter = ',', bool hasHeader = true);
    }
}
