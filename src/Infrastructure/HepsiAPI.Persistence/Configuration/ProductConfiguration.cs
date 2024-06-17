using HepsiAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HepsiAPI.Persistence.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Title).HasMaxLength(255);

            var product1 = new Product()
            {
                Id = 1,
                Title = "Bilgisayar",
                Price = 45000,
                Discount = 0.5m,
                Description = "Oyun Bilgisayari",
                BrandId = 1
            };

            var product2 = new Product()
            {
                Id = 2,
                Title = "Elbise",
                Price = 50000,
                Discount = 0.3m,
                Description = "Abiye",
                BrandId = 3
            };

            builder.HasData(product1, product2);
        }
    }
}
