using HepsiAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HepsiAPI.Persistence.Configuration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(c => c.CategoryName).HasMaxLength(255);

            var category1 = new Category()
            {
                Id = 1,
                CategoryName = "Elektronik",
                ParentId = 0,
                Priority = 1,
            };

            var category2 = new Category()
            {
                Id = 2,
                CategoryName = "Moda",
                ParentId = 0,
                Priority = 2,
            };

            var category3 = new Category()
            {
                Id = 3,
                CategoryName = "Bilgisayar",
                ParentId = 1,
                Priority = 1,
            };

            

            var category4 = new Category()
            {
                Id = 4,
                CategoryName = "Kadin",
                ParentId = 2,
                Priority = 1,
            };

            builder.HasData(category1, category2, category3, category4);
        }
    }
}
