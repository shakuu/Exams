using System.Reflection;

using DbExam.Data.Common.Services;
using DbExam.Data.JsonImporter.Converters;
using DbExam.Data.JsonImporter.JsonModels;
using DbExam.Data.JsonImporter.JsonParsers;
using DbExam.Data.JsonImporter.XmlExporters;

using Ninject;

namespace DbExam.Data.JsonImporter
{
    public class Program
    {
        /// <summary>
        /// Run Update-Database -Verbose in package manager console
        /// with target DbExam.Data first.
        /// </summary>
        public static void Main()
        {
            var jsonParser = new JsonParser();
            var superheroes = jsonParser.ParseOne<JsonDataContainer>(null);
            var converter = new SuperHeroConverter();
            var converted = converter.ConvertToSqlSuperhero(superheroes.data);

            var ninject = new StandardKernel();
            ninject.Load(Assembly.GetExecutingAssembly());

            var superheroService = ninject.Get<SuperheroService>();
            superheroService.AddMany(converted);

            var xmlExporter = ninject.Get<SuperheroesUneverseEporter>();
            xmlExporter.ExportAllSuperheroes();
            xmlExporter.ExportSuperheroDetails(1);
            xmlExporter.ExportSuperheroesByCity("Gotham");
            xmlExporter.ExportSupperheroesWithPower("Acrobatics");
            xmlExporter.ExportFractions();
            xmlExporter.ExportFractionDetails(1);
        }
    }
}
