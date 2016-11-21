using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DbExam.Models
{
    public class Superhero : INameable
    {
        /*Each superhero has name, secret identity, city that protects, alignment ,
         *  story, a list of fractions and a list of powers*/
        private ICollection<Power> powers;
        private ICollection<Fraction> fractions;
        private ICollection<Relationship> relationships;

        public Superhero()
        {
            this.powers = new HashSet<Power>();
            this.fractions = new HashSet<Fraction>();
            this.relationships = new HashSet<Relationship>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 3)]
        [Index(IsUnique = true)]
        public string SecretIdentity { get; set; }

        [Required]
        public int CityId { get; set; }

        public virtual City City { get; set; }

        [Required]
        public AlignmentType AlignmentType { get; set; }

        [Column(TypeName = "ntext")]
        [Required]
        public string Story { get; set; }

        public virtual ICollection<Relationship> Relationships
        {
            get
            {
                return this.relationships;
            }

            set
            {
                this.relationships = value;
            }
        }

        public virtual ICollection<Power> Powers
        {
            get
            {
                return this.powers;
            }
            set { this.powers = value; }
        }

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
    }
}
