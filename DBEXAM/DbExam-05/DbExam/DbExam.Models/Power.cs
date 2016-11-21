using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DbExam.Models
{
    public class Power : INameable
    {
        private ICollection<Superhero> superheroes;

        public Power()
        {
            this.superheroes = new HashSet<Superhero>();
        }

        public int Id { get; set; }

        [StringLength(35, MinimumLength = 3)]
        [Index(IsUnique = true)]
        public string Name { get; set; }

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