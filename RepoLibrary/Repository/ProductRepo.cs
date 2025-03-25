using Microsoft.EntityFrameworkCore;
using Models.Models;
using RepoLibrary.DBContext;
using RepoLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoLibrary.Repository
{
    public class ProductRepo : IProductRepo
    {
        private readonly ApplicationDbContext _dbcontext;
        public ProductRepo(ApplicationDbContext dbcontext)
        {
            this._dbcontext = dbcontext;
        }
        public async Task<dynamic> AddProducts(Product product)
        {
            this._dbcontext.Products.Add(product);
            await this._dbcontext.SaveChangesAsync();
            return product;
        }

        public async Task<dynamic> DeleteProducts(Product prod)
        {
            this._dbcontext.Products.Remove(prod);
            await this._dbcontext.SaveChangesAsync();
            return prod;
        }

        public async Task<dynamic> GetProductbyid(int id)
        {
            return await _dbcontext.Products.FindAsync(id);
        }

        public async Task<dynamic> GetProducts()
        {
            ///////////eager loading //////////////////
            //var res1= await this._dbcontext.Products.Include(p=> this._dbcontext.ProductCategory).ToListAsync();
            //return res1;

            //////////////deferd loading /////////////
            var res2 = await this._dbcontext.Products.Join(_dbcontext.ProductCategory,
                p => p.ProductCategoryId,
                c => c.ProductCategoryId,
                (p, c) => new { p.ProductId, p.ProductName, p.ProductPrice, c.productCatName }

                ).ToListAsync();
            // return res2;

            //var res3= await (from p in _dbcontext.Products
            //              join c in _dbcontext.ProductCategory on p.ProductCategoryId equals c.ProductCategoryId
            //              select new
            //              {
            //                  p.ProductId,
            //                  p.ProductName,
            //                  p.ProductPrice,
            //                  CategoryName = c.productCatName
            //              }).ToListAsync();
            //return res3;
            //////////////////////////////////////////////userwise prod list /////////////////
            var userwiseprod =await _dbcontext.Users
                .Join(_dbcontext.UserProducts,
                u => u.UserID,
                up => up.UserID,
                (u, up) => new { u, up })
                .Join(_dbcontext.ProductCategory,
                uup => uup.up.Products.ProductCategoryId,
                pc => pc.ProductCategoryId, (uup, pc) => new
                {
                    username = uup.u.Name,
                    prodname = uup.up.Products.ProductName,
                    category = pc.productCatName
                }).ToListAsync();
            ////////////////////////////////////////// catgorywiselist and grouping ///////////////////////
            var catgorywiselist = await _dbcontext.ProductCategory
                .Join(_dbcontext.Products,
                pc => pc.ProductCategoryId,
                p => p.ProductCategoryId, (pc, p) => new
                {
                    categoryname=pc.productCatName,
                    productname=p.ProductName,
                }).GroupBy(z=>z.categoryname).Select(g => new
                {
                    CategoryName = g.Key,
                    ProductCount = g.Count(),  // Count products in each category
                    Products = g.Select(p => p.productname).ToList()  // List of product names
                }).ToListAsync();


            ///////////////////////////////////////////
            return new { userwise = userwiseprod, catgorywiselist= catgorywiselist };
        }

        public async Task<dynamic> UpdateProducts(Product product)
        {
            //this._dbcontext.Products.Entry(product).State = EntityState.Modified;
            //await this._dbcontext.SaveChangesAsync();
            //or
            this._dbcontext.Products.Update(product);
            await this._dbcontext.SaveChangesAsync();
            return product;
        }
    }
}
