using DbExam.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbExam.Data
{
    public class SuperheroesDbContext : DbContext
    {
        public SuperheroesDbContext()
            : base("name=SuperheroesUniverse")
        {

        }

        public virtual IDbSet<Superhero> Superheroes { get; set; }

        public virtual IDbSet<City> Cities { get; set; }

        public virtual IDbSet<Country> Countries { get; set; }

        public virtual IDbSet<Planet> Planets { get; set; }

        public virtual IDbSet<Fraction> Fractions { get; set; }

        public virtual IDbSet<Relationship> Relationships { get; set; }

        public virtual IDbSet<Power> Powers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            base.OnModelCreating(modelBuilder);
        }
    }
}
