namespace CRM.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class vs151 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.tbl_POApprovalHistory", "StaffId");
            AddForeignKey("dbo.tbl_POApprovalHistory", "StaffId", "dbo.tbl_Staff", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tbl_POApprovalHistory", "StaffId", "dbo.tbl_Staff");
            DropIndex("dbo.tbl_POApprovalHistory", new[] { "StaffId" });
        }
    }
}
