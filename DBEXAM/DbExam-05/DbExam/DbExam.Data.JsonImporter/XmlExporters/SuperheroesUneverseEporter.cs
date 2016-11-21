using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

using DbExam.Models;
using System.Data.Entity;

namespace DbExam.Data.JsonImporter.XmlExporters
{
    public class SuperheroesUneverseEporter : ISuperheroesUniverseExporter
    {
        private const string AllSuperheroes = "../../../../03. Xml Files/all-superheroes.xml";
        private const string AllSuperheroesByCity = "../../../../03. Xml Files/all-superheroes-city.xml";
        private const string AllSuperheroesByPower = "../../../../03. Xml Files/all-superheroes-power.xml";
        private const string AllSuperheroesDetails = "../../../../03. Xml Files/all-superheroes-details.xml";
        private const string AllFractions = "../../../../03. Xml Files/all-fractions.xml";
        private const string AllFractionsDetails = "../../../../03. Xml Files/all-fractions-details.xml";

        private readonly SuperheroesDbContext context;

        public SuperheroesUneverseEporter(SuperheroesDbContext context)
        {
            this.context = context;
        }

        public string ExportAllSuperheroes()
        {
            var superheroes = context.Superheroes
                .Include(s => s.City)
                .Include(s => s.City.Country)
                .Include(s => s.City.Country.Planet)
                .ToList();

            this.WriteAllSuperHeroes(superheroes, AllSuperheroes);
            return null;
        }

        public string ExportFractionDetails(object fractionId)
        {
            var fraction = this.context.Fractions.FirstOrDefault(f => f.Id == (int)fractionId);
            this.WriteFractionDetailsXml(fraction, AllFractionsDetails);

            return null;
        }

        public string ExportFractions()
        {
            var fractions = this.context.Fractions.Include(f => f.Planets).ToList();
            this.WriteAllFractions(fractions, AllFractions);
            return null;
        }

        public string ExportSuperheroDetails(object superheroId)
        {
            var superhero = this.context.Superheroes.FirstOrDefault(s => s.Id == (int)superheroId);
            this.WriteSuperheroDetailsXml(superhero, AllSuperheroesDetails);
            return null;
        }

        public string ExportSuperheroesByCity(string cityName)
        {
            var superheroes = context.Superheroes
                .Include(s => s.City)
                .Include(s => s.City.Country)
                .Include(s => s.City.Country.Planet)
                .Where(s => s.City.Name == cityName)
                .ToList();

            this.WriteAllSuperHeroes(superheroes, AllSuperheroesByCity);
            return null;
        }

        public string ExportSupperheroesWithPower(string power)
        {
            var superheroes = context.Superheroes
                .Include(s => s.City)
                .Include(s => s.City.Country)
                .Include(s => s.City.Country.Planet)
                .Where(s => s.Powers.Select(p => p.Name).Contains(power))
                .ToList();

            this.WriteAllSuperHeroes(superheroes, AllSuperheroesByPower);
            return null;
        }

        private void WriteFractionDetailsXml(Fraction fraction, string fileName)
        {
            var settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.Encoding = Encoding.UTF8;

            using (var writer = XmlWriter.Create(fileName, settings))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("fraction");
                writer.WriteAttributeString("id", fraction.Id.ToString());
                writer.WriteAttributeString("membersCount", fraction.Superheroes.Count.ToString());

                writer.WriteElementString("name", fraction.Name);

                writer.WriteStartElement("planets");
                if (fraction.Planets != null)
                {
                    foreach (var planet in fraction.Planets)
                    {
                        writer.WriteElementString("planet", planet.Name);
                    }
                }

                writer.WriteEndElement();

                writer.WriteStartElement("members");
                if (fraction.Superheroes != null)
                {
                    foreach (var superhero in fraction.Superheroes)
                    {
                        writer.WriteStartElement("member");
                        writer.WriteAttributeString("id", superhero.Id.ToString());
                        writer.WriteString(superhero.Name);
                        writer.WriteEndElement();
                    }
                }

                writer.WriteEndElement();

                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
        }

        private void WriteAllFractions(IEnumerable<Fraction> fractions, string fileName)
        {
            var settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.Encoding = Encoding.UTF8;

            using (var writer = XmlWriter.Create(fileName, settings))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("fractions");

                foreach (var fraction in fractions)
                {
                    this.GenerateFractionXml(writer, fraction);
                }

                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
        }

        private void GenerateFractionXml(XmlWriter writer, Fraction fraction)
        {
            writer.WriteStartElement("fraction");
            writer.WriteAttributeString("id", fraction.Id.ToString());
            writer.WriteAttributeString("membersCount", fraction.Superheroes.Count.ToString());

            writer.WriteElementString("name", fraction.Name);

            writer.WriteStartElement("planets");
            if (fraction.Planets != null)
            {
                foreach (var planet in fraction.Planets)
                {
                    writer.WriteElementString("planet", planet.Name);
                }
            }

            writer.WriteEndElement();

            writer.WriteEndElement();
        }

        private void WriteSuperheroDetailsXml(Superhero superhero, string fileName)
        {
            var settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.Encoding = Encoding.UTF8;

            using (var writer = XmlWriter.Create(fileName, settings))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("superhero");
                writer.WriteAttributeString("id", superhero.Id.ToString());

                writer.WriteElementString("name", superhero.Name);
                writer.WriteElementString("secretIdentity", superhero.SecretIdentity);
                writer.WriteElementString("alignment", superhero.AlignmentType.ToString());

                writer.WriteStartElement("powers");
                foreach (var power in superhero.Powers)
                {
                    writer.WriteElementString("power", power.Name);
                }

                writer.WriteEndElement();

                writer.WriteStartElement("fractions");
                if (superhero.Fractions != null)
                {
                    foreach (var fraction in superhero.Fractions)
                    {
                        writer.WriteStartElement("fraction");
                        writer.WriteAttributeString("id", fraction.Id.ToString());
                        writer.WriteString(fraction.Name);
                        writer.WriteEndElement();
                    }
                }

                writer.WriteEndElement();

                writer.WriteElementString("city", $"{superhero.City.Name}, {superhero.City.Country.Name}, {superhero.City.Country.Planet.Name}");
                writer.WriteElementString("story", superhero.Story);

                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
        }

        private void WriteAllSuperHeroes(IEnumerable<Superhero> superheroes, string fileName)
        {
            var settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.Encoding = Encoding.UTF8;

            using (var writer = XmlWriter.Create(fileName, settings))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("superheroes");

                foreach (var superhero in superheroes)
                {
                    this.GenerateSuperheroXml(writer, superhero);
                }

                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
        }

        private void GenerateSuperheroXml(XmlWriter writer, Superhero superhero)
        {
            writer.WriteStartElement("superhero");
            writer.WriteAttributeString("id", superhero.Id.ToString());

            writer.WriteElementString("name", superhero.Name);
            writer.WriteElementString("secretIdentity", superhero.SecretIdentity);
            writer.WriteElementString("alignment", superhero.AlignmentType.ToString());

            writer.WriteStartElement("powers");
            foreach (var power in superhero.Powers)
            {
                writer.WriteElementString("power", power.Name);
            }

            writer.WriteEndElement();

            writer.WriteElementString("city", $"{superhero.City.Name}, {superhero.City.Country.Name}, {superhero.City.Country.Planet.Name}");
            writer.WriteEndElement();
        }
    }
}
