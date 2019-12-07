namespace NewAPIProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateUserModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "CreationDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.AspNetUsers", "LastModificationDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "LastModificationDate");
            DropColumn("dbo.AspNetUsers", "CreationDate");
        }
    }
}
