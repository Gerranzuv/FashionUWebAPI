namespace NewAPIProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class shippingRequestUpdate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ShippingRequests", "PaymentId", "dbo.Payments");
            DropIndex("dbo.ShippingRequests", new[] { "PaymentId" });
            AlterColumn("dbo.ShippingRequests", "PaymentId", c => c.Int());
            CreateIndex("dbo.ShippingRequests", "PaymentId");
            AddForeignKey("dbo.ShippingRequests", "PaymentId", "dbo.Payments", "id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ShippingRequests", "PaymentId", "dbo.Payments");
            DropIndex("dbo.ShippingRequests", new[] { "PaymentId" });
            AlterColumn("dbo.ShippingRequests", "PaymentId", c => c.Int(nullable: false));
            CreateIndex("dbo.ShippingRequests", "PaymentId");
            AddForeignKey("dbo.ShippingRequests", "PaymentId", "dbo.Payments", "id", cascadeDelete: true);
        }
    }
}
