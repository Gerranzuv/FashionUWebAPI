namespace NewAPIProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class products : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        ExpiryDate = c.DateTime(nullable: false),
                        DesignerName = c.String(),
                        ItemCode = c.String(),
                        Type = c.String(),
                        Brand = c.String(),
                        NumberOfItems = c.Int(nullable: false),
                        AvailableColors = c.String(),
                        AvailableSizes = c.String(),
                        Length = c.Int(nullable: false),
                        WaistSize = c.Int(nullable: false),
                        SleeveLength = c.Int(nullable: false),
                        Bust = c.Int(nullable: false),
                        Price = c.Double(nullable: false),
                        Currency = c.String(),
                        CreationDate = c.DateTime(nullable: false),
                        LastModificationDate = c.DateTime(nullable: false),
                        Creator = c.String(),
                        Modifier = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Products");
        }
    }
}
