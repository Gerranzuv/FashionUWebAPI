namespace NewAPIProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class isWhiteProduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "isProductWhite", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "isProductWhite");
        }
    }
}
