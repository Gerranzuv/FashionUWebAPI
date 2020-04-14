namespace NewAPIProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class isBackgroundWhite : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "IsBackGroundWhite", c => c.Boolean(nullable: true));
            DropColumn("dbo.Products", "isProductWhite");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "isProductWhite", c => c.Boolean(nullable: false));
            DropColumn("dbo.Products", "IsBackGroundWhite");
        }
    }
}
