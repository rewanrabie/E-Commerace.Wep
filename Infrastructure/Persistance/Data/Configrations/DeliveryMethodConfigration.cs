﻿using DomainLayer.Models.OrderModule;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Data.Configrations
{
    public class DeliveryMethodConfigration : IEntityTypeConfiguration<DeliveryMethod>
    {
        public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
        {
            builder.ToTable("DeliveryMethod");
            builder.Property(D => D.Cost)
                .HasColumnType("decimal(8,2)");
            builder.Property(D => D.ShortName)
               .HasColumnType("varchar(50)");
            builder.Property(D => D.Description)
            .HasColumnType("varchar(100)");
            builder.Property(D => D.DeliveryTime)
            .HasColumnType("varchar(50)");
        }
    }
}
