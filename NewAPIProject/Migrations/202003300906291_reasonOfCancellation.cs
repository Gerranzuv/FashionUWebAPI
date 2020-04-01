namespace NewAPIProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class reasonOfCancellation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ShippingRequests", "CancelationDate", c => c.DateTime());
            AddColumn("dbo.ShippingRequests", "ReasonOfCancellation", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ShippingRequests", "ReasonOfCancellation");
            DropColumn("dbo.ShippingRequests", "CancelationDate");
        }
    }
}
