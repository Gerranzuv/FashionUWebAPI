namespace NewAPIProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class companyratio : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Companies", "CompanyRatio", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Companies", "CompanyRatio");
        }
    }
}
