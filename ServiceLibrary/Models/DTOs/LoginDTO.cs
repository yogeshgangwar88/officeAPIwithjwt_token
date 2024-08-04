﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLibrary.Models.DTOs
{
    public class LoginDTO
    {
        public string? username { get; set; }
        public string? password { get; set; }
        public string? token { get; set; }
    }
}
