namespace NewAPIProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class commit : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ShippingRequests", "photoId", c => c.Int(nullable: false));
            AddColumn("dbo.ShippingRequests", "size", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ShippingRequests", "size");
            DropColumn("dbo.ShippingRequests", "photoId");
        }
    }
}
