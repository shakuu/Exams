namespace DbExam.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fractions : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Planets", "FractionId", "dbo.Fractions");
            DropIndex("dbo.Planets", new[] { "FractionId" });
            CreateTable(
                "dbo.FractionPlanets",
                c => new
                    {
                        Fraction_Id = c.Int(nullable: false),
                        Planet_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Fraction_Id, t.Planet_Id })
                .ForeignKey("dbo.Fractions", t => t.Fraction_Id)
                .ForeignKey("dbo.Planets", t => t.Planet_Id)
                .Index(t => t.Fraction_Id)
                .Index(t => t.Planet_Id);
            
            DropColumn("dbo.Planets", "FractionId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Planets", "FractionId", c => c.Int());
            DropForeignKey("dbo.FractionPlanets", "Planet_Id", "dbo.Planets");
            DropForeignKey("dbo.FractionPlanets", "Fraction_Id", "dbo.Fractions");
            DropIndex("dbo.FractionPlanets", new[] { "Planet_Id" });
            DropIndex("dbo.FractionPlanets", new[] { "Fraction_Id" });
            DropTable("dbo.FractionPlanets");
            CreateIndex("dbo.Planets", "FractionId");
            AddForeignKey("dbo.Planets", "FractionId", "dbo.Fractions", "Id");
        }
    }
}
