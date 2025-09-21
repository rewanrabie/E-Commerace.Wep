﻿using DomainLayer.Models.OrderModule;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Data.Configrations
{
    public class OrderItemConfigrations : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("OrderItems");
            builder.Property(OI => OI.Price).HasColumnType("decimal(8,2)");
            builder.OwnsOne(OI=>OI.Product);


        }
    }
}
