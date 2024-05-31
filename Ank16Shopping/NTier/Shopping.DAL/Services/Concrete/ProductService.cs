using AutoMapper;
using AutoMapper.EquivalencyExpression;
using AutoMapper.Extensions.ExpressionMapping;
using Shopping.DAL.Repositories.Abstract;
using Shopping.DAL.Repositories.Concrete;
using Shopping.DAL.Services.Abstract;
using Shopping.DAL.Services.Profiles;
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
            MapperConfiguration _config = new MapperConfiguration(cfg =>
            {
                cfg.AddExpressionMapping().AddCollectionMappers();
                cfg.AddProfile<ProductProfile>();
            });

            base._mapper = _config.CreateMapper();
        }
    }
}
