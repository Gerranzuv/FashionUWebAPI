namespace NewAPIProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ContactUs1 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Contactus", name: "UserId", newName: "RelatedUserId");
            RenameIndex(table: "dbo.Contactus", name: "IX_UserId", newName: "IX_RelatedUserId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Contactus", name: "IX_RelatedUserId", newName: "IX_UserId");
            RenameColumn(table: "dbo.Contactus", name: "RelatedUserId", newName: "UserId");
        }
    }
}
