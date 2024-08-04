using ServiceLibrary.Models;
using ServiceLibrary.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLibrary.Interfaces
{
    public interface ILogin
    {
        public Task<Login> loginuser(Login login);
        public Task<List<Items>> getAll(string username);
    }
}
