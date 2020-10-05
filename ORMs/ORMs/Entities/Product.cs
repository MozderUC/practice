using System;
using System.Collections.Generic;
using System.Text;

namespace ORMs.Entities
{
    public class Product
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public Supplier Supplier { get; set; }

        public Category Category { get; set; }
    }
}
