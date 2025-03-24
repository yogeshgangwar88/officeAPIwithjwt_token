using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RepoLibrary.Interfaces;
using RepoLibrary.RepoModels;
using RepoLibrary.Repository;
using ServiceLibrary.Interfaces;
using ServiceLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
//using json

namespace ServiceLibrary.Services
{
    public class LoginService : ILogin
    {
        private readonly ILoginRepo _repo;
        public LoginService(ILoginRepo repo)
        {
           this._repo = repo;
        }
        public async Task<List<Items>> getAll(string username)
        {
            var data =await _repo.getAll();
            List<Items> list = new List<Items>();
            if (data!=null && data.Tables[0].Rows.Count>0)
            {
                for (int i = 0; i < data.Tables[0].Rows.Count; i++)
                {
                    list.Add( new Items() { itemname = data.Tables[0].Rows[i]["ItemName"].ToString(),
                     desc=  data.Tables[0].Rows[i]["Description"].ToString(),
                     price=Convert.ToDouble (data.Tables[0].Rows[i]["Price"])
                    });
                }
            }
            return list;
        }

        public async Task<Login> loginuser(Login model)
        {
            LoginRepoModel modelrepo=new LoginRepoModel();
            modelrepo.username = model.username;
            modelrepo.password = model.password;    
            var data=await _repo.Loginuser(modelrepo);
            Login l=new Login();
            if (data!=null && data.Tables[0].Rows.Count>0)
            {
                l.token = "true";
            }
            //response.statuscode = 200;
            //response.jsondata = Newtonsoft.Json.JsonConvert.SerializeObject(data);// JsonConvert.SerializeObject(new { item = data });
            return l;
        }
    }
}
