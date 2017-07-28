namespace CRM.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class vs152 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tbl_TourCalculation", "CurrencyId", c => c.Int());
            CreateIndex("dbo.tbl_TourCalculation", "CurrencyId");
            AddForeignKey("dbo.tbl_TourCalculation", "CurrencyId", "dbo.tbl_Dictionary", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tbl_TourCalculation", "CurrencyId", "dbo.tbl_Dictionary");
            DropIndex("dbo.tbl_TourCalculation", new[] { "CurrencyId" });
            DropColumn("dbo.tbl_TourCalculation", "CurrencyId");
        }
    }
}
