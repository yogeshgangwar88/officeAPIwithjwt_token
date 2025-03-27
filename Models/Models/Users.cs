using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    [Table("Users")]
    public class Users
    {
        [Key]
        public int UserID { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public DateTime? DOB { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public bool Ismarried { get; set; }
        public string? Password { get; set; }
      
        //// Navigation Property (One-to-One)
        public UserFamilyDetails? UserFamilyDetails { get; set; }
        //navigation property for many to many ///
        public List<UserProducts>? UserProducts { get; set; }
    }
}
