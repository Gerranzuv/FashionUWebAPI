namespace NewAPIProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class numOfItems1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "NumberOfItems", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "NumberOfItems");
        }
    }
}
