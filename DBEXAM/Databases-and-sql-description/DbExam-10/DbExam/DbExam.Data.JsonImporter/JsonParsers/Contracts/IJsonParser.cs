using System.Collections.Generic;

namespace DbExam.Data.JsonImporter.JsonParsers.Contracts
{
    public interface IJsonParser
    {
        IEnumerable<T> ParseJsonList<T>(string fileName);

        T ParseOne<T>(string fileName);
    }
}
