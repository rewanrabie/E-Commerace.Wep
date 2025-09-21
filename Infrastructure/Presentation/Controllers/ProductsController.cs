using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Attributes;
using ServicesAbstraction;
using Shared;
using Shared.DataTransferObjects.ProductModuleDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    public class ProductsController(IServiceManger _serviceManger) : ApiBaseController
    {

        //Get all Products
        //Get baseurl/api/product ==> path
        [HttpGet]
        [Cache]
         public async Task<ActionResult<PaginatedResult<ProductDto>>> GetAllProducts([FromQuery]ProductQueryParams queryParams)
        {
            var Products = await _serviceManger.ProductService.GetAllProductsAsync(queryParams);
            return Ok(Products);
        }

        //Get all Product By Id 
        // Get baseurl/api/product/id ==> Path\

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            var Product = await _serviceManger.ProductService.GetProductByIdAsync(id);
            return Ok(Product);
        }
        //Get all Types 
        // Get baseurl/api/products/types

        [HttpGet("type")]
        [Cache]
        public async Task<ActionResult<IEnumerable<TypeDto>>> GetTypes()
        {
            var Types = await _serviceManger.ProductService.GetAllTypesAsync();
            return Ok(Types);
        }

        //Get all Brands 
        // Get baseurl/api/products/brands

        [HttpGet("brands")]
        [Cache]
        public async Task<ActionResult<IEnumerable<BrandDto>>> GetBrands()
        {
            var Brands = await _serviceManger.ProductService.GetAllBrandsAsync();
            return Ok(Brands);
        }
    }
}
