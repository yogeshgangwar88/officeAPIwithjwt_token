﻿using RepoLibrary.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepoLibrary.RepoModels;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using RepoLibrary.Interfaces;

namespace RepoLibrary.Repository
{
    public class LoginRepo:DbLayer,ILoginRepo
    {
        public LoginRepo(IConfiguration configuration):base(configuration) 
        {
            
        }
        public  async Task< DataSet> Loginuser(LoginRepoModel model)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@userid", model.username);
            param.Add("@password", model.password);
            var result =await Datasetwithsp("Userlogin", param);
            return result;
        }
        public async Task<DataSet> getAll()
        {
            var result = await Datasetwithsp("Getallpro", null);
            return result;
        }
    }
}
