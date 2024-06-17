using HepsiAPI.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HepsiAPI.Domain.Entities
{
    public class Category : BaseEntity
    {
        public int ParentId { get; set; }
        public string CategoryName { get; set; }
        public int Priority { get; set; }


        //Navigation Props.
        public ICollection<Product> Products { get; set; }
    }

}
