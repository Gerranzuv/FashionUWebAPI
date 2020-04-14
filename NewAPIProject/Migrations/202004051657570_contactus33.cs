namespace NewAPIProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class contactus33 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Contactus", "Email", c => c.String());
            AddColumn("dbo.Contactus", "phoneNumber", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Contactus", "phoneNumber");
            DropColumn("dbo.Contactus", "Email");
        }
    }
}
