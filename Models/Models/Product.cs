using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [MaxLength(100)]
        [Required]
        public string? ProductName { get; set; }
        [Required]
        public float ProductPrice { get; set; }
        // Foreign key
        public int ProductCategoryId { get; set; }

        [ForeignKey("ProductCategoryId")]
        public ProductCategory ?Category { get; set; }
        //navigation property for many to many ///
        public List<UserProducts> UserProducts { get; set; }
    }
}
