using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HepsiAPI.Application.Dtos
{
    public class CategoryDto
    {
        public int ParentId { get; set; }
        public string CategoryName { get; set; }
        public int Priority { get; set; }
    }
}
