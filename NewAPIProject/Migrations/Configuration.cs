namespace NewAPIProject.Migrations
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<NewAPIProject.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(NewAPIProject.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            context.SystemParameters.AddOrUpdate(
                new Models.SystemParameter
                {
                    Name = "Is On Production",
                    Code = "is_on_production",
                    Value = "1",
                    CreationDate = DateTime.Now,
                    LastModificationDate = DateTime.Now
                });
            //context.Roles.AddOrUpdate(new IdentityRole()
            //{
            //    Name="Guest"

            //},
            //new IdentityRole()
            //{
            //    Name = "Client"

            //}
            //);
        }
    }
}
