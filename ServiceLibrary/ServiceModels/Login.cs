﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLibrary.Models
{
    public class Login
    {
        [Required(ErrorMessage ="this is required feild")]
        [MinLength(3,ErrorMessage ="min length is 3")]
        public string ? username { get; set; }
        [Required(ErrorMessage = "this is required feild")]
        public string  ?password { get; set; }
        public string ? token { get; set; }
    }
    public class Items
    {
        public string? itemname { get; set; }
        public double price { get; set; }
        public string ?desc { get; set; }
    }
}
