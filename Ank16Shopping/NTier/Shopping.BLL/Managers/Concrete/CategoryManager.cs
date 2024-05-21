using Shopping.BLL.Managers.Abstract;
using Shopping.DAL.Services.Abstract;
using Shopping.DAL.Services.Concrete;
using Shopping.DTO;
using Shopping.Entities.Concrete;
using Shopping.ViewModel;
using Shopping.ViewModel.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.BLL.Managers.Concrete
{
    public class CategoryManager : Manager<CategoryDto, CategoryViewModel, Category>
    {
        public CategoryManager(CategoryService service) : base(service)
        {
        }

        //public int UserId
        //{
        //    set { base.AppUserId = value; }
        //}
    }
}
