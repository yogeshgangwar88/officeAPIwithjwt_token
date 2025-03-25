using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoLibrary.Interfaces
{
    public interface IProductRepo
    {
        public Task<dynamic> GetProducts();
        public Task<dynamic> GetProductbyid(int id);
        public Task<dynamic> AddProducts(Product product);
        public Task<dynamic> UpdateProducts( Product product);
        public Task<dynamic> DeleteProducts(Product prod);
    }
}
