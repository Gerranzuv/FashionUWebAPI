namespace NewAPIProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class products1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "FabricType", c => c.String());
            AddColumn("dbo.Products", "torsoLength", c => c.Double(nullable: false));
            AddColumn("dbo.Products", "legLength", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "legLength");
            DropColumn("dbo.Products", "torsoLength");
            DropColumn("dbo.Products", "FabricType");
        }
    }
}
