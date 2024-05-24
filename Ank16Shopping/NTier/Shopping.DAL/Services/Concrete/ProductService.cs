using Shopping.DAL.Repositories.Abstract;
using Shopping.DAL.Repositories.Concrete;
using Shopping.DAL.Services.Abstract;
using Shopping.DTO;
using Shopping.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.DAL.Services.Concrete
{
    public class ProductService : Service<Product, ProductDto>
    {
        public ProductService(ProductRepo repo) : base(repo)
        {
        }
    }
}
