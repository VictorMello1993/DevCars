using DevCars.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevCars.API.Configurations
{
    public class OrderDbConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id);

            //Relação 1 - N (Pedido x items de pedido)
            builder.HasMany(o => o.ExtraItems)
                   .WithOne()
                   .HasForeignKey(e => e.IdOrder)
                   .OnDelete(DeleteBehavior.Restrict);

            //Relação 1 - 1 (Carro x pedido)
            builder.HasOne(o => o.Car)
                   .WithOne()
                   .HasForeignKey<Order>(o => o.IdCar) //Especificando a FK em uma classe que possui maior dependência (no caso, o pedido, pois é impossível existir um pedido sem um carro)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
