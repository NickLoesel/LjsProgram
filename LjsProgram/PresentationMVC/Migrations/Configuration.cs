namespace PresentationMVC.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using PresentationMVC.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<PresentationMVC.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(PresentationMVC.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);

            const string admin = "roxanne@cateringbyljs.com";
            const string defaultPassword = "P@ssw0rd";

            context.Roles.AddOrUpdate(r => r.Name, new IdentityRole() {Name = "Admin" });
            context.Roles.AddOrUpdate(r => r.Name, new IdentityRole() { Name = "Manager" });
            context.Roles.AddOrUpdate(r => r.Name, new IdentityRole() { Name = "Employee" });

            context.SaveChanges();

            if (!context.Users.Any(u => u.UserName == admin))
            {
                var user = new ApplicationUser()
                {
                    UserName = admin,
                    Email = admin,
                    FirstName = "Roxanne",
                    LastName = "Braeden"

                };

                IdentityResult result = userManager.Create(user, defaultPassword);
                context.SaveChanges();

                if (result.Succeeded)
                {
                    userManager.AddToRole(user.Id, "Admin");
                    context.SaveChanges();
                }
            }
        }
    }
}
