namespace NewAPIProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class companyUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "companyUser", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "companyUser");
        }
    }
}
