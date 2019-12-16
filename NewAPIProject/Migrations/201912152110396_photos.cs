namespace NewAPIProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class photos : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Attachments", "Product_id", c => c.Int());
            CreateIndex("dbo.Attachments", "Product_id");
            AddForeignKey("dbo.Attachments", "Product_id", "dbo.Products", "id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Attachments", "Product_id", "dbo.Products");
            DropIndex("dbo.Attachments", new[] { "Product_id" });
            DropColumn("dbo.Attachments", "Product_id");
        }
    }
}
