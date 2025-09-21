using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models.ProductModule
{
    public class Product : BaseEntity<int>
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string PictureUrl { get; set; } = null!;
        public decimal Price { get; set; }
        public ProductBrand ProductBrand { get; set; } // 1--> M (PK from ProdcutBrand)
        public int BrandId { get; set; } //FK
        public ProductType ProductType { get; set; } // 1--> M (PK from ProdcutType)
        public int TypeId { get; set; } //FK

    }
}
