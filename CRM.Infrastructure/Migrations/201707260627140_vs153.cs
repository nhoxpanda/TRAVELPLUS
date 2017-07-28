namespace CRM.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class vs153 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tbl_POApproval", "CustomerName", c => c.String());
            AddColumn("dbo.tbl_POApproval", "PartnerName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.tbl_POApproval", "PartnerName");
            DropColumn("dbo.tbl_POApproval", "CustomerName");
        }
    }
}
