using AutoWrapper.Wrappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Product.Domain.Interface;
using Product.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductAPI.Controllers
{
    [Route("products")]
    [ApiController]
    public class ProductsController : Controller
    {
        IProductsOp _productOp;

        public ProductsController(IProductsOp productOp)
        {
            _productOp = productOp;
        }

        //[HttpGet]
        //public ApiResponse GetProducts()
        //{
           
        //}

        [HttpGet]
        [Route("{id}")]
        public ApiResponse GetProductById(int Id)
        {
            return new ApiResponse("Success", _productOp.GetProductById(Id));
        }

        [HttpGet]
        public ApiResponse GetProductByCategoryId(int? CategoryId)
        {
            if(CategoryId == null || CategoryId==0)
            {
                return new ApiResponse("Success", _productOp.GetProducts());
            }
            else 
            return new ApiResponse("Success", _productOp.GetProductByCategoryId((int)CategoryId));
        }

        [HttpPost]
        public ApiResponse AddProduct([FromBody] ProducToBeSaved newProduct)
        {
            
                return new ApiResponse("Success", _productOp.AddProduct(newProduct));
        }
        [HttpPut]
        [Route("{ProductId}")]
        public ApiResponse UpdateProduct(int? ProductId,  [FromBody] ProducToBeSaved UpdateProduct)
        {
            if (ProductId == null || ProductId == 0)
            {
                return new ApiResponse("Success", "Product Id is required");
            }
            else
                return new ApiResponse("Success", _productOp.UpdateProduct((int)ProductId, UpdateProduct));
        }
    }
}
