namespace NewAPIProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class contactus10 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Contactus", "RelatedUserId", "dbo.AspNetUsers");
            DropIndex("dbo.Contactus", new[] { "RelatedUserId" });
            DropColumn("dbo.Contactus", "RelatedUserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Contactus", "RelatedUserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Contactus", "RelatedUserId");
            AddForeignKey("dbo.Contactus", "RelatedUserId", "dbo.AspNetUsers", "Id");
        }
    }
}
