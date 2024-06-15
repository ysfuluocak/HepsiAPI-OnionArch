using HepsiAPI.Application.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HepsiAPI.Application.Entities
{
    public class Category : BaseEntity
    {
        public required int ParentId { get; set; }
        public required string CategoryName { get; set; }
        public required int Priority { get; set; }


        //Navigation Props.
        public ICollection<Product> Products { get; set; }
    }

}
