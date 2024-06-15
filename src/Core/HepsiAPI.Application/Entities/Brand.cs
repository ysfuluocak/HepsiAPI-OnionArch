using HepsiAPI.Application.Common;

namespace HepsiAPI.Application.Entities
{
    public class Brand : BaseEntity
    {
        public required string BrandName { get; set; }


        //Navigation Props.
        public ICollection<Product> Products { get; set; }

    }

}
