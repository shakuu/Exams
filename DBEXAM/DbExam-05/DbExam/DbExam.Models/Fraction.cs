using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DbExam.Models
{
    /*Each fraction has a name, alignment, a list of "planet":s that it protects and a list of members(superheroes)*/
    public class Fraction : INameable
    {
        private ICollection<Planet> planets;
        private ICollection<Superhero> superheroes;

        public Fraction()
        {
            this.planets = new HashSet<Planet>();
            this.superheroes = new HashSet<Superhero>();
        }

        public int Id { get; set; }

        [StringLength(30, MinimumLength = 2)]
        [Index(IsUnique = true)]
        public string Name { get; set; }

        public AlignmentType AlignmentType { get; set; }

        public virtual ICollection<Planet> Planets
        {
            get
            {
                return this.planets;
            }

            set
            {
                this.planets = value;
            }
        }

        public virtual ICollection<Superhero> Superheroes
        {
            get
            {
                return this.superheroes;
            }

            set
            {
                this.superheroes = value;
            }
        }
    }
}