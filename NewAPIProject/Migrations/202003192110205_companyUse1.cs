namespace NewAPIProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class companyUse1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ShippingRequests", "scheduledDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ShippingRequests", "scheduledDate", c => c.DateTime(nullable: false));
        }
    }
}
