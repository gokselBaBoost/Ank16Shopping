using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.ViewModel
{
    public abstract class ResponseModel
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
    }
}
