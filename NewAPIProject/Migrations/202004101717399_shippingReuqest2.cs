namespace NewAPIProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class shippingReuqest2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ShippingRequests", "Email", c => c.String());
            AddColumn("dbo.ShippingRequests", "phoneNumber", c => c.String());
            AddColumn("dbo.ShippingRequests", "Address", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ShippingRequests", "Address");
            DropColumn("dbo.ShippingRequests", "phoneNumber");
            DropColumn("dbo.ShippingRequests", "Email");
        }
    }
}
