using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shopping.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.DAL.DataContext
{
    public class ShoppingDbContext : IdentityDbContext<AppUser,IdentityRole<int>,int>
    {
        public ShoppingDbContext(DbContextOptions<ShoppingDbContext> options)
            :base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //Admin User Add

            //string admin = "Admin";
            //string mail = admin + "@mail.com";

            var hasher = new PasswordHasher<AppUser>();

            builder.Entity<AppUser>()
                   .HasData(new AppUser{
                       Id = 1,
                       Name = "AdminName",
                       Surname = "AdminSurname",
                       UserName = "admin",
                       NormalizedUserName = "ADMIN",
                       Email = "admin@mail.com",
                       NormalizedEmail = "ADMIN@MAIL.COM",
                       BirthDate = new DateOnly(2000,1,1),
                       Gender = Common.Gender.None,
                       EmailConfirmed = true,
                       PhoneNumberConfirmed = true,
                       PhoneNumber = "-",
                       PasswordHash = hasher.HashPassword(null, "Az*123456"),
                       SecurityStamp = Guid.NewGuid().ToString()
                   });


            //Admin Role Add

            builder.Entity<IdentityRole<int>>()
                   .HasData(new IdentityRole<int>
                   {
                       Id = 1,
                       Name = "Admin",
                       NormalizedName = "ADMIN"
                   });

            //User To Role Add

            builder.Entity<IdentityUserRole<int>>()
                   .HasData(new IdentityUserRole<int>
                   {
                       UserId = 1,
                       RoleId = 1
                   });
        }
    }
}
