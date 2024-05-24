using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.DTO
{
    public class ProductDto : BaseDto
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public decimal Price { get; set; }
        public short Stock { get; set; }
        public bool IsActive { get; set; }

        public int CategoryId { get; set; }
    }
}
