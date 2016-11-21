using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DbExam.Models
{
    public class Planet : INameable
    {
        private ICollection<Country> countries;

        public Planet()
        {
            this.countries = new HashSet<Country>();
        }

        public int Id { get; set; }

        [StringLength(30, MinimumLength = 2)]
        [Index(IsUnique = true)]
        public string Name { get; set; }

        public int? FractionId { get; set; }

        public virtual Fraction Fraction { get; set; }

        public ICollection<Country> Countries
        {
            get
            {
                return this.countries;
            }

            set
            {
                this.countries = value;
            }
        }
    }
}