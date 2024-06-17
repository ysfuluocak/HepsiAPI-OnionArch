using HepsiAPI.Domain.Common;

namespace HepsiAPI.Domain.Entities
{
    public class Brand : BaseEntity
    {
        public string BrandName { get; set; }


        //Navigation Props.
        public ICollection<Product> Products { get; set; }

    }

}
