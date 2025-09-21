using Shared;
using Shared.DataTransferObjects.ProductModuleDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesAbstraction
{
    public interface IProductService
    {
        //get all products
        Task<PaginatedResult<ProductDto>> GetAllProductsAsync(ProductQueryParams queryParams);
        //get product by id 
        Task<ProductDto> GetProductByIdAsync(int Id);

        //get all types
        Task<IEnumerable<TypeDto>> GetAllTypesAsync();
        //get all brands
        Task<IEnumerable<BrandDto>> GetAllBrandsAsync();
    }
}
