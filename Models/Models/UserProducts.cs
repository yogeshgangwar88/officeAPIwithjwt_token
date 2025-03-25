using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class UserProducts
    {
        public int UserID { get; set; }
        public Users Users { get; set; }
        public int ProductId { get; set; }
        public Product Products { get; set; }
      
    }
}
