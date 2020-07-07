namespace NewAPIProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class revoke : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ShippingRequests", "photoId");
            DropColumn("dbo.ShippingRequests", "size");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ShippingRequests", "size", c => c.String());
            AddColumn("dbo.ShippingRequests", "photoId", c => c.Int(nullable: false));
        }
    }
}
