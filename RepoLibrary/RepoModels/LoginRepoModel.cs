﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoLibrary.RepoModels
{
    public class LoginRepoModel
    {
        public string? username { get; set; }
        public string? password { get; set; }
        public string? token { get; set; }
    }
}
