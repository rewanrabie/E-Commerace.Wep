using AutoMapper;
using DomainLayer.Exceptions;
using DomainLayer.InterFaceRepostory_Contracts_;
using DomainLayer.Models.ProductModule;
using Serives.Specifications;
using ServicesAbstraction;
using Shared;
using Shared.DataTransferObjects.ProductModuleDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serives
{
    public class ProductService(IUnitOfWork _unitOfWork, IMapper _mapper) : IProductService
    {
        public async Task<IEnumerable<BrandDto>> GetAllBrandsAsync()
        {
            var Repo =  _unitOfWork.GetRepository<ProductBrand,int>();
            var Brands = await Repo.GetAllAsync();
            var BrandsDto = _mapper.Map<IEnumerable<ProductBrand>, IEnumerable<BrandDto>>(Brands);
            return BrandsDto;
        }

        public async Task<PaginatedResult<ProductDto>> GetAllProductsAsync(ProductQueryParams queryParams)
        {
            var Repo = _unitOfWork.GetRepository<Product, int>();
            var Specifications = new ProductWithBrandAndTypesSpecifications(queryParams);
            var Products = await Repo.GetAllAsync(specifications:Specifications);
            var Data= _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(Products);
            var ProductCount = Data.Count();
            var CountSpec = new ProductCountSpecification(queryParams);
            var TotalCount = await Repo.CountAsync(CountSpec);
            return new PaginatedResult<ProductDto>(queryParams.pageNumber,ProductCount, TotalCount, Data);
        }

        public async Task<IEnumerable<TypeDto>> GetAllTypesAsync()
        {
            var Types = await _unitOfWork.GetRepository<ProductType, int>().GetAllAsync();
            var TypesDto = _mapper.Map<IEnumerable<ProductType>, IEnumerable<TypeDto>>(Types);
            return TypesDto;
        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var Specifications = new ProductWithBrandAndTypesSpecifications(id);
            var Product = await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(Specifications);
            if (Product is null)
            {
                throw new ProductNotFoundException(id);
            }
            return _mapper.Map<Product,ProductDto>(Product);
        }
    }
}
