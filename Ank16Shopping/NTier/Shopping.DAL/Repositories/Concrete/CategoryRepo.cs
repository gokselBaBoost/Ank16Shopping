using Shopping.DAL.DataContext;
using Shopping.DAL.Repositories.Abstract;
using Shopping.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.DAL.Repositories.Concrete
{
    public class CategoryRepo : Repo<Category>
    {
        public CategoryRepo(ShoppingDbContext dbContext) : base(dbContext)
        {
        }
    }
}
