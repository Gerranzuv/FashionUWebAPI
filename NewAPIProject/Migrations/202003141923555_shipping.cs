namespace NewAPIProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class shipping : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Payments",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Status = c.String(),
                        productId = c.Int(nullable: false),
                        CompanyId = c.Int(nullable: false),
                        Amount = c.Double(nullable: false),
                        Method = c.String(),
                        currency = c.String(),
                        CreationDate = c.DateTime(nullable: false),
                        LastModificationDate = c.DateTime(nullable: false),
                        Creator = c.String(),
                        Modifier = c.String(),
                        AttachmentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Companies", t => t.CompanyId, cascadeDelete: false)
                .ForeignKey("dbo.Products", t => t.productId, cascadeDelete: false)
                .Index(t => t.productId)
                .Index(t => t.CompanyId);
            
            AddColumn("dbo.Products", "CompanyId", c => c.Int(nullable: false));
            AddColumn("dbo.ShippingRequests", "PaymentId", c => c.Int(nullable: false));
            CreateIndex("dbo.Products", "CompanyId");
            CreateIndex("dbo.ShippingRequests", "PaymentId");
            AddForeignKey("dbo.Products", "CompanyId", "dbo.Companies", "id", cascadeDelete: true);
            AddForeignKey("dbo.ShippingRequests", "PaymentId", "dbo.Payments", "id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ShippingRequests", "PaymentId", "dbo.Payments");
            DropForeignKey("dbo.Payments", "productId", "dbo.Products");
            DropForeignKey("dbo.Payments", "CompanyId", "dbo.Companies");
            DropForeignKey("dbo.Products", "CompanyId", "dbo.Companies");
            DropIndex("dbo.ShippingRequests", new[] { "PaymentId" });
            DropIndex("dbo.Payments", new[] { "CompanyId" });
            DropIndex("dbo.Payments", new[] { "productId" });
            DropIndex("dbo.Products", new[] { "CompanyId" });
            DropColumn("dbo.ShippingRequests", "PaymentId");
            DropColumn("dbo.Products", "CompanyId");
            DropTable("dbo.Payments");
        }
    }
}
