using DomainLayer.Models.ProductModule;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serives.Specifications
{
    class ProductWithBrandAndTypesSpecifications : BaseSpecifications<Product,int>
    {
        //Get Product With Brand And Type
        public ProductWithBrandAndTypesSpecifications(ProductQueryParams queryParams) 
          :base(P=>(!queryParams.BrandId.HasValue || P.BrandId== queryParams.BrandId) 
          && (!queryParams.TypeId.HasValue || P.TypeId== queryParams.TypeId)
          &&(string.IsNullOrWhiteSpace(queryParams.search) || P.Name.ToLower().Contains(queryParams.search.ToLower())))
        {
            AddInclude(P=>P.ProductBrand);
            AddInclude(P=>P.ProductType);

            switch (queryParams.sort)
            {
                case ProductSortingOptions.NameAsc:
                    AddOrderBy(P => P.Name);
                        break;
                case ProductSortingOptions.NameDesc:
                    AddOrderByDescending(P => P.Name);
                    break;
                case ProductSortingOptions.PriceAsc:
                    AddOrderBy(P=>P.Price);
                    break;
                case ProductSortingOptions.PriceDesc:
                    AddOrderByDescending(P => P.Price);
                    break;
                default:
                    break;
            }

            
            ApplyPagination(queryParams.PageSize,queryParams.pageNumber);
        }


        //get product by id
        public ProductWithBrandAndTypesSpecifications(int id):base(P=>P.Id==id)
        {
            AddInclude(P => P.ProductBrand);
            AddInclude(P => P.ProductType);
        }
    }
}
