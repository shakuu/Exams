using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DbExam.Models
{
    public class Planet : INameable
    {
        private ICollection<Country> countries;
        private ICollection<Fraction> fractions;

        public Planet()
        {
            this.countries = new HashSet<Country>();
            this.fractions = new HashSet<Fraction>();
        }

        public int Id { get; set; }

        [StringLength(30, MinimumLength = 2)]
        [Index(IsUnique = true)]
        public string Name { get; set; }

        public virtual ICollection<Fraction> Fractions
        {
            get
            {
                return this.fractions;
            }

            set
            {
                this.fractions = value;
            }
        }

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