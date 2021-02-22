namespace NewAPIProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class numOfItems : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Attachments", "NumOfItems", c => c.Int(nullable: false));
            DropColumn("dbo.Products", "NumberOfItems");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "NumberOfItems", c => c.Int(nullable: false));
            DropColumn("dbo.Attachments", "NumOfItems");
        }
    }
}
