using System;
using System.Collections.Generic;
using System.IO;
using YouiApplicationExcercise.Service.Contracts;

namespace YouiApplicationExcercise.Service
{
    public class FileParser : IFileParser
    {
        public IEnumerable<T> ParseFile<T>(StreamReader reader, IStringToModelParser<T> lineParser, char delimiter = ',', bool hasHeader = true)
        {
            if (!reader.EndOfStream && hasHeader)
                reader.ReadLine();

            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var obj = lineParser.Parse(line, delimiter);
                yield return obj;
            }
        }
    }
}
