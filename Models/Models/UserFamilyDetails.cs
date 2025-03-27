using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class UserFamilyDetails
    {
        [Key]
        public int UserFamilyDetailsid { get; set; }
        [Required]
        [MaxLength(100,ErrorMessage ="Length can not more than 100 char")]
        public string? FatherName { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "Length can not more than 100 char")]
        public string? MotherName { get; set; }
      
        public int UserID { get; set; }
        [ForeignKey("UserID")]
        public Users users { get; set; }
    
    }
}
