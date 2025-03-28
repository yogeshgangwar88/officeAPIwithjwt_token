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
        [MinLength(4, ErrorMessage = "Username cannot less than 4 char")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Date of Birth is required.")]
        [DataType(DataType.Date)]
        [CustomValidation(typeof(Users), nameof(ValidateDOB))]
        public DateTime? DOB { get; set; }
        [Required]
        public int Age { get; set; }
        public bool Ismarried { get; set; }
        [Required]
        public string? Password { get; set; }

        //// Navigation Property (One-to-One)
        public UserFamilyDetails? UserFamilyDetails { get; set; }
        //navigation property for many to many ///
        public List<UserProducts>? UserProducts { get; set; }

        public static ValidationResult? ValidateDOB(DateTime? dob, ValidationContext context)
        {
            if (dob.HasValue)
            {
                if (dob.Value > DateTime.Today)
                {
                    return new ValidationResult("Date of Birth cannot be in the future.");
                }
            }
            return ValidationResult.Success;
        }

    }
}
