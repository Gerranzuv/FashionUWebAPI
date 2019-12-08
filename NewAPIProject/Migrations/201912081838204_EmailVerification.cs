namespace NewAPIProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmailVerification : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EmailLogs",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Sender = c.String(nullable: false),
                        Receiver = c.String(nullable: false),
                        Subject = c.String(nullable: false),
                        Body = c.String(),
                        CreationDate = c.DateTime(nullable: false),
                        LastModificationDate = c.DateTime(nullable: false),
                        Creator = c.String(),
                        Modifier = c.String(),
                        AttachmentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.SystemParameters",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Code = c.String(nullable: false),
                        Value = c.String(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        LastModificationDate = c.DateTime(nullable: false),
                        Creator = c.String(),
                        Modifier = c.String(),
                        AttachmentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.UserVerificationLogs",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        ConfirmationDate = c.DateTime(nullable: false),
                        ExpiryDate = c.DateTime(nullable: false),
                        Code = c.String(),
                        Status = c.String(),
                        Email = c.String(),
                        IsEmailSent = c.Boolean(nullable: false),
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
            DropTable("dbo.UserVerificationLogs");
            DropTable("dbo.SystemParameters");
            DropTable("dbo.EmailLogs");
        }
    }
}
