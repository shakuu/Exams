using System.Collections.Generic;

using DbExam.Data.JsonImporter.Converters.Contracts;
using DbExam.Data.JsonImporter.JsonModels;
using DbExam.Models;
using System;
using System.Linq;

namespace DbExam.Data.JsonImporter.Converters
{
    /*{
"name": "Batman",
"secretIdentity": "Bruce Wayne",
"city": {
"name": "Gotham",
"country": "USA",
"planet": "Earth"
},
"alignment": "good",
"story": "After losing his parents...",
"powers": [
"Utility belt",
"Intelligence",
"Martial arts"
],
"fractions": [
"Justice League",
"The Bat Family"
]
},*/
    public class SuperHeroConverter : ISuperHeroConverter
    {
        public IEnumerable<Superhero> ConvertToSqlSuperhero(IEnumerable<JsonSuperhero> jsonSuperheros)
        {
            var result = new List<Superhero>();
            foreach (var jsonSuperhero in jsonSuperheros)
            {
                var superhero = new Superhero();

                superhero.Name = jsonSuperhero.name;
                superhero.SecretIdentity = jsonSuperhero.secretIdentity;
                superhero.Story = jsonSuperhero.story;
                jsonSuperhero.alignment = jsonSuperhero.alignment[0].ToString().ToUpper() + jsonSuperhero.alignment.Substring(1);
                superhero.AlignmentType = (AlignmentType)Enum.Parse(typeof(AlignmentType), jsonSuperhero.alignment);

                superhero.City = this.ResolveCity(jsonSuperhero.city);
                superhero.Powers = this.ResovlePowers(jsonSuperhero.powers);
                superhero.Fractions = this.ResolveFractions(jsonSuperhero.fractions);

                if (superhero.Fractions != null)
                {
                    foreach (var freaction in superhero.Fractions)
                    {
                        freaction.AlignmentType = superhero.AlignmentType;
                    }
                }

                result.Add(superhero);
            }

            return result;
        }

        private ICollection<Fraction> ResolveFractions(IEnumerable<string> fractionNames)
        {
            if (fractionNames == null)
            {
                return null;
            }

            var powers = new List<Fraction>();
            foreach (var name in fractionNames)
            {
                var nextFraction = new Fraction()
                {
                    Name = name
                };

                powers.Add(nextFraction);
            }

            return powers;
        }

        private ICollection<Power> ResovlePowers(IEnumerable<string> powersNames)
        {
            if (powersNames == null)
            {
                return null;
            }

            var powers = new List<Power>();
            foreach (var name in powersNames)
            {
                var nextPower = new Power()
                {
                    Name = name
                };

                powers.Add(nextPower);
            }

            return powers;
        }

        private City ResolveCity(JsonCity jsonCity)
        {
            var planet = new Planet()
            {
                Name = jsonCity.planet
            };

            var country = new Country()
            {
                Name = jsonCity.country,
                Planet = planet
            };

            var city = new City()
            {
                Name = jsonCity.name,
                Country = country
            };

            return city;
        }
    }
}
