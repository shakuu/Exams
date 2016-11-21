using System;
using System.Collections.Generic;
using System.IO;

using DbExam.Data.JsonImporter.JsonParsers.Contracts;

using Newtonsoft.Json;

namespace DbExam.Data.JsonImporter.JsonParsers
{
    public class JsonParser : IJsonParser
    {
        private const string DefaultFileName = "../../../../02. Json-Data/sample-data.json";

        public IEnumerable<T> ParseJsonList<T>(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                fileName = JsonParser.DefaultFileName;
            }

            var json = File.ReadAllText(fileName);
            var superheroes = JsonConvert.DeserializeObject<List<T>>(json);

            return superheroes;
        }

        public T ParseOne<T>(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                fileName = JsonParser.DefaultFileName;
            }

            var json = File.ReadAllText(fileName);
            var data = JsonConvert.DeserializeObject<T>(json);

            return data;
        }
    }
}
