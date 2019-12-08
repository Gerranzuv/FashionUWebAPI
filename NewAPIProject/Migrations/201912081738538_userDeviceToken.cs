namespace NewAPIProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userDeviceToken : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UsersDeviceTokens",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        token = c.String(),
                        UserId = c.String(),
                        CreationDate = c.DateTime(nullable: false),
                        LastModificationDate = c.DateTime(nullable: false),
                        Creator = c.String(),
                        Modifier = c.String(),
                        AttachmentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UsersDeviceTokens");
        }
    }
}
