using AutoMapper;
using AutoWrapper.Wrappers;
using Microsoft.AspNetCore.Mvc;
using Product.Domain.Interface;
using Product.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductAPI.Controllers
{
    [Route("category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private ICategoriesOp _categoriesOp;
        
        public CategoryController(ICategoriesOp categoriesOp)//, IMapper mapper)
        {
            _categoriesOp = categoriesOp?? throw new ArgumentNullException(nameof(categoriesOp));

        }

        [HttpGet]
        public ApiResponse GetCategories()
        {
            return new ApiResponse("Success", _categoriesOp.GetCategories());
        }
    }
}
