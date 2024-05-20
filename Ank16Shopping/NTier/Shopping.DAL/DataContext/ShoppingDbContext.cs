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
    }
}
