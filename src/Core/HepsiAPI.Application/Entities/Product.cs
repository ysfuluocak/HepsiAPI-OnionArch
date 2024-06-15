﻿using HepsiAPI.Application.Common;

namespace HepsiAPI.Application.Entities
{
    public class Product : BaseEntity
    {
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required decimal Price { get; set; }
        public required decimal Discount { get; set; }


        //Navigation Props.
        public required int BrandId { get; set; }
        public Brand Brand { get; set; }

        public ICollection<Category> Categories { get; set; }
    }

}
