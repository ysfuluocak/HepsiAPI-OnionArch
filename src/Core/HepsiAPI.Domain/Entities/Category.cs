using HepsiAPI.Domain.Common;

namespace HepsiAPI.Domain.Entities
{
    public class Category : BaseEntity
    {
        public int ParentId { get; set; }
        public string CategoryName { get; set; }
        public int Priority { get; set; }


        //Navigation Props.
        public ICollection<CategoryProduct> CategoryProducts { get; set; }
    }

}
