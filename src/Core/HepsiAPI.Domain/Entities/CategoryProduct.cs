using HepsiAPI.Domain.Common;

namespace HepsiAPI.Domain.Entities
{
    public class CategoryProduct : IBaseEntity
    {
        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        public int ProductId { get; set; }
        public Product? Product { get; set; }
    }
}
