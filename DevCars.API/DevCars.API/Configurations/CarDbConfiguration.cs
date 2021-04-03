using DevCars.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevCars.API.Configurations
{
    public class CarDbConfiguration : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            //builder.ToTable("tb_Cars");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Brand)
                   .IsRequired()
                   .HasColumnName("Marca")
                   .HasColumnType("VARCHAR(100)")
                   .HasDefaultValue("PADRÃO")
                   .HasMaxLength(100);
        }
    }
}
