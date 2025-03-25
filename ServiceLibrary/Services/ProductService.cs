using Models.Models;
using RepoLibrary.Interfaces;
using ServiceLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLibrary.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepo _product;
        public ProductService(IProductRepo product)
        {
            this._product = product;
        }
        public async Task<dynamic> AddProducts(Product product)
        {
          return await this._product.AddProducts(product);
        }

        public async Task<dynamic> DeleteProducts(Product prod)
        {
            return await this._product.DeleteProducts(prod);
        }

        public async Task<dynamic> GetProductbyid(int id)
        {
            return await this._product.GetProductbyid(id);
        }

        public async Task<dynamic> GetProducts()
        {
            return await this._product.GetProducts();
        }

        public async Task<dynamic> UpdateProducts(Product product)
        {
            return await this._product.UpdateProducts(product);
        }
    }
}
