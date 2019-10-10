using MarketEngine.Domain.Service.Interfaces;
using MarketEngine.Model.DTO.Requests;
using Microsoft.AspNetCore.Mvc;
using System;

namespace MarketEngine.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService productService;
        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpPost]
        public IActionResult Post([FromBody]CreateProductRequest product)
        {
            try
            {
                productService.Create(product);
                return Ok();
            }
            catch (InvalidOperationException invalidOperationException)
            {
                return BadRequest(invalidOperationException.Message);
            }
        }

        //[HttpGet("{id}")]
        //public IActionResult Get(string id)
        //{
        //    try
        //    {
                
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}
    }
}