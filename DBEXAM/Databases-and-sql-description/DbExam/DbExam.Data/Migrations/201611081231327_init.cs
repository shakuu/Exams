namespace DbExam.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 30),
                        CountryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Countries", t => t.CountryId, cascadeDelete: true)
                .Index(t => t.Name, unique: true)
                .Index(t => t.CountryId);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 30),
                        PlanetId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Planets", t => t.PlanetId, cascadeDelete: true)
                .Index(t => t.Name, unique: true)
                .Index(t => t.PlanetId);
            
            CreateTable(
                "dbo.Planets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 30),
                        FractionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Fractions", t => t.FractionId, cascadeDelete: true)
                .Index(t => t.Name, unique: true)
                .Index(t => t.FractionId);
            
            CreateTable(
                "dbo.Fractions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 30),
                        AlignmentType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true);
            
            CreateTable(
                "dbo.Superheroes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 60),
                        SecretIdentity = c.String(nullable: false, maxLength: 20),
                        CityId = c.Int(nullable: false),
                        AlignmentType = c.Int(nullable: false),
                        Story = c.String(nullable: false, storeType: "ntext"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cities", t => t.CityId, cascadeDelete: true)
                .Index(t => t.SecretIdentity, unique: true)
                .Index(t => t.CityId);
            
            CreateTable(
                "dbo.Powers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 35),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true);
            
            CreateTable(
                "dbo.Relationships",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SuperheroId = c.Int(nullable: false),
                        HasRelationshipWithSuperheroId = c.Int(),
                        RelationshipType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Superheroes", t => t.HasRelationshipWithSuperheroId)
                .ForeignKey("dbo.Superheroes", t => t.SuperheroId, cascadeDelete: true)
                .Index(t => t.SuperheroId)
                .Index(t => t.HasRelationshipWithSuperheroId);
            
            CreateTable(
                "dbo.SuperheroFractions",
                c => new
                    {
                        Superhero_Id = c.Int(nullable: false),
                        Fraction_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Superhero_Id, t.Fraction_Id })
                .ForeignKey("dbo.Superheroes", t => t.Superhero_Id)
                .ForeignKey("dbo.Fractions", t => t.Fraction_Id)
                .Index(t => t.Superhero_Id)
                .Index(t => t.Fraction_Id);
            
            CreateTable(
                "dbo.PowerSuperheroes",
                c => new
                    {
                        Power_Id = c.Int(nullable: false),
                        Superhero_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Power_Id, t.Superhero_Id })
                .ForeignKey("dbo.Powers", t => t.Power_Id)
                .ForeignKey("dbo.Superheroes", t => t.Superhero_Id)
                .Index(t => t.Power_Id)
                .Index(t => t.Superhero_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Relationships", "SuperheroId", "dbo.Superheroes");
            DropForeignKey("dbo.Relationships", "HasRelationshipWithSuperheroId", "dbo.Superheroes");
            DropForeignKey("dbo.PowerSuperheroes", "Superhero_Id", "dbo.Superheroes");
            DropForeignKey("dbo.PowerSuperheroes", "Power_Id", "dbo.Powers");
            DropForeignKey("dbo.SuperheroFractions", "Fraction_Id", "dbo.Fractions");
            DropForeignKey("dbo.SuperheroFractions", "Superhero_Id", "dbo.Superheroes");
            DropForeignKey("dbo.Superheroes", "CityId", "dbo.Cities");
            DropForeignKey("dbo.Planets", "FractionId", "dbo.Fractions");
            DropForeignKey("dbo.Countries", "PlanetId", "dbo.Planets");
            DropForeignKey("dbo.Cities", "CountryId", "dbo.Countries");
            DropIndex("dbo.PowerSuperheroes", new[] { "Superhero_Id" });
            DropIndex("dbo.PowerSuperheroes", new[] { "Power_Id" });
            DropIndex("dbo.SuperheroFractions", new[] { "Fraction_Id" });
            DropIndex("dbo.SuperheroFractions", new[] { "Superhero_Id" });
            DropIndex("dbo.Relationships", new[] { "HasRelationshipWithSuperheroId" });
            DropIndex("dbo.Relationships", new[] { "SuperheroId" });
            DropIndex("dbo.Powers", new[] { "Name" });
            DropIndex("dbo.Superheroes", new[] { "CityId" });
            DropIndex("dbo.Superheroes", new[] { "SecretIdentity" });
            DropIndex("dbo.Fractions", new[] { "Name" });
            DropIndex("dbo.Planets", new[] { "FractionId" });
            DropIndex("dbo.Planets", new[] { "Name" });
            DropIndex("dbo.Countries", new[] { "PlanetId" });
            DropIndex("dbo.Countries", new[] { "Name" });
            DropIndex("dbo.Cities", new[] { "CountryId" });
            DropIndex("dbo.Cities", new[] { "Name" });
            DropTable("dbo.PowerSuperheroes");
            DropTable("dbo.SuperheroFractions");
            DropTable("dbo.Relationships");
            DropTable("dbo.Powers");
            DropTable("dbo.Superheroes");
            DropTable("dbo.Fractions");
            DropTable("dbo.Planets");
            DropTable("dbo.Countries");
            DropTable("dbo.Cities");
        }
    }
}
