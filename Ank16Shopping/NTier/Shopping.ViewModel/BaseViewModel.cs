using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.ViewModel
{
    public abstract class  BaseViewModel
    {
        public int? Id { get; set; }
        public int? RowNum { get; set; }

        public int? AppUserId { get; set; }
    }
}
