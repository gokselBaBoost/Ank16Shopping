using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.ViewModel.Product
{
    public class ProductViewModel : BaseViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public decimal Price { get; set; }
        public short Stock { get; set; }
        public bool IsActive { get; set; }

        public int CategoryId { get; set; }
    }
}
