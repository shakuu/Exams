using System.Collections.Generic;

using DbExam.Data.JsonImporter.JsonModels;
using DbExam.Models;

namespace DbExam.Data.JsonImporter.Converters.Contracts
{
    public interface ISuperHeroConverter
    {
        IEnumerable<Superhero> ConvertToSqlSuperhero(IEnumerable<JsonSuperhero> jsonSuperheros);
    }
}
