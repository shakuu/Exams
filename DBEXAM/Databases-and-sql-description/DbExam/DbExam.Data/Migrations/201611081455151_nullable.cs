namespace DbExam.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nullable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Planets", "FractionId", "dbo.Fractions");
            DropIndex("dbo.Planets", new[] { "FractionId" });
            AlterColumn("dbo.Planets", "FractionId", c => c.Int());
            CreateIndex("dbo.Planets", "FractionId");
            AddForeignKey("dbo.Planets", "FractionId", "dbo.Fractions", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Planets", "FractionId", "dbo.Fractions");
            DropIndex("dbo.Planets", new[] { "FractionId" });
            AlterColumn("dbo.Planets", "FractionId", c => c.Int(nullable: false));
            CreateIndex("dbo.Planets", "FractionId");
            AddForeignKey("dbo.Planets", "FractionId", "dbo.Fractions", "Id", cascadeDelete: true);
        }
    }
}
