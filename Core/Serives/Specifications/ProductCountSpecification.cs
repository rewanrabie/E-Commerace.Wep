using DomainLayer.Models.ProductModule;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serives.Specifications
{
    class ProductCountSpecification : BaseSpecifications<Product,int>
    {
        public ProductCountSpecification(ProductQueryParams queryParams)
            :base(P => (!queryParams.BrandId.HasValue || P.BrandId == queryParams.BrandId)
          && (!queryParams.TypeId.HasValue || P.TypeId == queryParams.TypeId)
          && (string.IsNullOrWhiteSpace(queryParams.search) || P.Name.ToLower().Contains(queryParams.search.ToLower())))
        {       


        }
    }
}
