using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbExam.Data.JsonImporter.XmlExporters
{
    public interface ISuperheroesUniverseExporter
    {
        string ExportAllSuperheroes();

        string ExportSupperheroesWithPower(string power);

        string ExportSuperheroDetails(object superheroId);

        string ExportFractions();

        string ExportFractionDetails(object fractionId);

        string ExportSuperheroesByCity(string cityName);
    }
}
