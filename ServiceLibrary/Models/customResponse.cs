using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLibrary.Models
{
    public class customResponse<T>
    {
        public int statuscode { get; set; }
        public string ?message { get; set; }
        public List<T> ?data { get; set; }
        public string jsondata { get; set; }
    }
}
