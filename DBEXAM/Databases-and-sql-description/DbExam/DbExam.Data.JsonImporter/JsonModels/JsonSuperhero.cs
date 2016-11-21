using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbExam.Data.JsonImporter.JsonModels
{
    /*"data": [
    {
      "name": "Batman",
      "secretIdentity": "Bruce Wayne",
      "city": {
        "name": "Gotham",
        "country": "USA",
        "planet": "Earth"
      },
      "alignment": "good",
      "story": "After losing his parents...",
      "powers": [ "Utility belt", "Intelligence", "Martial arts" ],
      "fractions": [ "Justice League", "The Bat Family" ]
    },*/
    public class JsonSuperhero
    {
        public string name { get; set; }

        public string secretIdentity { get; set; }

        public JsonCity city { get; set; }

        public string alignment { get; set; }

        public string story { get; set; }

        public List<string> powers { get; set; }

        public List<string> fractions { get; set; }
    }
}
