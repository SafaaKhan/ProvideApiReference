using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProvideApiReference_Models;
using ProvideApiReference_Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvideApiReference_DataAccess.Data
{
    public class Seed
    {
        public static async Task SeedDataAsync(ApplicationDbContext db, 
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager)
        {
            if (!userManager.Users.Any())
            {
                //create roles if they are not created

                    await roleManager.CreateAsync(new ApplicationRole { Name = SD.AdminRole });
                    await roleManager.CreateAsync(new ApplicationRole { Name = SD.ModeratorRole });
                    await roleManager.CreateAsync(new ApplicationRole { Name = SD.MemberRole });


                var adminUser = new ApplicationUser
                {
                    DisplayName = "AdminUser",
                    UserName = "AdminUser",
                    Email = "AdminUser@gmail.com"
                };

                await userManager.CreateAsync(adminUser, "adminUser@123");
                await userManager.AddToRolesAsync(adminUser, new[] { SD.AdminRole, SD.ModeratorRole });

                var moderatorUser = new ApplicationUser
                {
                    DisplayName = "moderatorUser",
                    UserName = "moderatorUser",
                    Email = "moderatorUser@gmail.com"
                };

                await userManager.CreateAsync(moderatorUser, "adminUser@123");
                await userManager.AddToRoleAsync(moderatorUser,  SD.ModeratorRole );

                var users = new List<ApplicationUser>
                {
                    new ApplicationUser { DisplayName = "Bob", UserName = "Bob", Email = "Bob@hotmail.com" },
                    new ApplicationUser { DisplayName = "Tom", UserName = "Tom", Email = "Tom@hotmail.com" },
                    new ApplicationUser { DisplayName = "Jane", UserName = "Jane", Email = "Jane@hotmail.com" },

                };
                
                foreach(var user in users)
                {
                    await userManager.CreateAsync(user, "Test@12test");
                    await userManager.AddToRoleAsync(user, SD.MemberRole);
                }


            }



            //if roles are not created, we will create the admin role as well
            if (db.Activities.Any()) return;

            var activities = new List<Activity>
            {
                new Activity
                {
                    Title = "Past Activity 1",
                    Date = DateTime.Now.AddMonths(-2),
                    Description = "Activity 2 months ago",
                    Category = "drinks",
                    City = "London",
                    Venue = "Pub",
                },
                 new Activity
                {
                    Title = "Past Activity 2",
                    Date = DateTime.Now.AddMonths(-1),
                    Description = "Activity 1 month ago",
                    Category = "culture",
                    City = "Paris",
                    Venue = "Louvre",
                },
                new Activity
                {
                    Title = "Future Activity 1",
                    Date = DateTime.Now.AddMonths(1),
                    Description = "Activity 1 month in future",
                    Category = "culture",
                    City = "London",
                    Venue = "Natural History Museum",
                },
                new Activity
                {
                    Title = "Future Activity 2",
                    Date = DateTime.Now.AddMonths(2),
                    Description = "Activity 2 months in future",
                    Category = "music",
                    City = "London",
                    Venue = "O2 Arena",
                },
                new Activity
                {
                    Title = "Future Activity 3",
                    Date = DateTime.Now.AddMonths(3),
                    Description = "Activity 3 months in future",
                    Category = "drinks",
                    City = "London",
                    Venue = "Another pub",
                },
                new Activity
                {
                    Title = "Future Activity 4",
                    Date = DateTime.Now.AddMonths(4),
                    Description = "Activity 4 months in future",
                    Category = "drinks",
                    City = "London",
                    Venue = "Yet another pub",
                },
                new Activity
                {
                    Title = "Future Activity 5",
                    Date = DateTime.Now.AddMonths(5),
                    Description = "Activity 5 months in future",
                    Category = "drinks",
                    City = "London",
                    Venue = "Just another pub",
                },
                new Activity
                {
                    Title = "Future Activity 6",
                    Date = DateTime.Now.AddMonths(6),
                    Description = "Activity 6 months in future",
                    Category = "music",
                    City = "London",
                    Venue = "Roundhouse Camden",
                },
                new Activity
                {
                    Title = "Future Activity 7",
                    Date = DateTime.Now.AddMonths(7),
                    Description = "Activity 2 months ago",
                    Category = "travel",
                    City = "London",
                    Venue = "Somewhere on the Thames",
                },
                new Activity
                {
                    Title = "Future Activity 8",
                    Date = DateTime.Now.AddMonths(8),
                    Description = "Activity 8 months in future",
                    Category = "film",
                    City = "London",
                    Venue = "Cinema",
                }

            };

            await db.Activities.AddRangeAsync(activities);
            await db.SaveChangesAsync();
        }
    }
}
