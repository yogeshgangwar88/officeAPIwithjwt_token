using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Models;
using ServiceLibrary.Interfaces;

namespace officeapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _product;
        public ProductsController(IProductService product)
        {
            this._product = product;
        }
        [Route("addproduct")]
        [HttpPost]
        public async Task<IActionResult> AddProduct( [FromBody]Product product)
        {
          var result= await this._product.AddProducts(product);
            return Ok(result);
        }
        
        [Route("updateproduct/{id}")]
        [HttpPut]
        public async Task<IActionResult> updateproduct( int id,[FromBody] Product product)
        {
            
            if (id==product.ProductId)
            {
                Product existpro = await this._product.GetProductbyid(id);
                if (existpro != null)
                {
                    existpro.ProductName=product.ProductName;
                    existpro.ProductPrice = product.ProductPrice;
                    existpro.ProductCategoryId = product.ProductCategoryId;
                    var result = await this._product.UpdateProducts(existpro);
                    return Ok(result);
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return BadRequest("Item not found");
            }
            
        }

        [Route("getproductbyid/{id}")]
        [HttpGet]
        public async Task<IActionResult> getproductbyid(int id)
        {
            var result = await this._product.GetProductbyid(id);
            if (result!=null)
                return Ok(result);
            else
                return NotFound();
        }
        [Route("getproduct")]
        [HttpGet]
        public async Task<IActionResult> getproduct()
        {
            var result = await this._product.GetProducts();
            return Ok(result);
        }

        [Route("deleteproduct/{id}")]
        [HttpDelete]
        public async Task<IActionResult> deleteproduct(int id)
        {
            var existpro = await this._product.GetProductbyid(id);
            if (existpro != null)
               {
                var result = await this._product.DeleteProducts(existpro);
                return Ok(result);
                }
            else
                return NotFound();
            
        }
    }
}
