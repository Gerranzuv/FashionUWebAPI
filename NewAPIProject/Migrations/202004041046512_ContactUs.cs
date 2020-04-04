namespace NewAPIProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ContactUs : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Contactus",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Text = c.String(),
                        UserId = c.String(maxLength: 128),
                        CreationDate = c.DateTime(nullable: false),
                        LastModificationDate = c.DateTime(nullable: false),
                        Creator = c.String(),
                        Modifier = c.String(),
                        AttachmentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Contactus", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Contactus", new[] { "UserId" });
            DropTable("dbo.Contactus");
        }
    }
}
