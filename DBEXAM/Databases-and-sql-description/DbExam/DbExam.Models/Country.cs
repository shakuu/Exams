using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DbExam.Models
{
    public class Country : INameable
    {
        private ICollection<City> cities;

        public Country()
        {
            this.cities = new HashSet<City>();
        }

        public int Id { get; set; }

        [StringLength(30, MinimumLength = 2)]
        [Index(IsUnique = true)]
        public string Name { get; set; }

        public int PlanetId { get; set; }

        public virtual Planet Planet { get; set; }

        public virtual ICollection<City> Cities
        {
            get
            {
                return this.cities;
            }

            set
            {
                this.cities = value;
            }
        }
    }
}