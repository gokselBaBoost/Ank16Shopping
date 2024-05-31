using Shopping.BLL.Managers.Abstract;
using Shopping.DAL.Services.Abstract;
using Shopping.DAL.Services.Concrete;
using Shopping.DTO;
using Shopping.Entities.Concrete;
using Shopping.ViewModel.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.BLL.Managers.Concrete
{
    public class ProductManager : Manager<ProductDto, ProductViewModel, Product>
    {
        public ProductManager(ProductService service) : base(service)
        {
        }
    }
}
