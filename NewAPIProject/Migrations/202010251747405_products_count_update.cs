namespace NewAPIProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class products_count_update : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ShippingRequests", "count", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ShippingRequests", "count");
        }
    }
}
