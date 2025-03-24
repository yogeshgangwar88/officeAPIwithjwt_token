using RepoLibrary.RepoModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoLibrary.Interfaces
{
    public  interface ILoginRepo
    {
        public Task<DataSet> Loginuser(LoginRepoModel model);
        public Task<DataSet> getAll();
    }
}
