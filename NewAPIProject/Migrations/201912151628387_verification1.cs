namespace NewAPIProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class verification1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UserVerificationLogs", "ConfirmationDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserVerificationLogs", "ConfirmationDate", c => c.DateTime(nullable: false));
        }
    }
}
