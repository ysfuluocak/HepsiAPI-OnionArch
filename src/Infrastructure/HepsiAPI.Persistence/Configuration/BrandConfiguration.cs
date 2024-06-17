using HepsiAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HepsiAPI.Persistence.Configuration
{
    public class BrandConfiguration : IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> builder)
        {
            builder.Property(b => b.BrandName).HasMaxLength(255);

            var brand1 = new Brand()
            {
                Id = 1,
                BrandName = "Asus"
            };

            var brand2 = new Brand()
            {
                Id = 2,
                BrandName = "Hp"
            };

            var brand3 = new Brand()
            {
                Id = 3,
                BrandName = "Mango"
            };

            builder.HasData(brand1, brand2, brand3);
        }
    }
}
