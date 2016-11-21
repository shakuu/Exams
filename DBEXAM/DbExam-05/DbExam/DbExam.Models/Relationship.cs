using System.ComponentModel.DataAnnotations.Schema;

namespace DbExam.Models
{
    public class Relationship
    {
        public int Id { get; set; }

        public int SuperheroId { get; set; }

        public virtual Superhero Superhero { get; set; }
        
        public int? HasRelationshipWithSuperheroId { get; set; }

        
        [ForeignKey("HasRelationshipWithSuperheroId")]
        [InverseProperty("Relationships")]
        public virtual Superhero HasRelationshipWithSuperhero { get; set; }

        public RelationshipType RelationshipType { get; set; }
    }
}