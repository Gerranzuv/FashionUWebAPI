namespace NewAPIProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class companyUserId : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Companies", new[] { "CompanyUser_Id" });
            DropColumn("dbo.Companies", "CompanyUserId");
            RenameColumn(table: "dbo.Companies", name: "CompanyUser_Id", newName: "CompanyUserId");
            AlterColumn("dbo.Companies", "CompanyUserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Companies", "CompanyUserId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Companies", new[] { "CompanyUserId" });
            AlterColumn("dbo.Companies", "CompanyUserId", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Companies", name: "CompanyUserId", newName: "CompanyUser_Id");
            AddColumn("dbo.Companies", "CompanyUserId", c => c.Int(nullable: false));
            CreateIndex("dbo.Companies", "CompanyUser_Id");
        }
    }
}
