namespace NewAPIProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fashionRatio : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Companies", "FashionU3DRatio", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Companies", "FashionU3DRatio");
        }
    }
}
