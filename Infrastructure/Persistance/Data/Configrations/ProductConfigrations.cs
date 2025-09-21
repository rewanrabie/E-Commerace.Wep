﻿using DomainLayer.Models.ProductModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Data.Configrations
{
    class ProductConfigrations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            //Relationship

            builder.HasOne(P => P.ProductBrand).WithMany().HasForeignKey(P=>P.BrandId);

            builder.HasOne(P => P.ProductType).WithMany().HasForeignKey(P=>P.TypeId);

            builder.Property(P => P.Price).HasColumnType("decimal(10,2)"); // change from defualt

        }
    }
}
