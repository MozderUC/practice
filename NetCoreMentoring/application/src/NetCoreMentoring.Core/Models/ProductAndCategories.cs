using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreMentoring.Core.Models
{
    public class ProductAndCategories
    {
        public Product Product { get; set; }

        public IEnumerable<Category> Categories { get; set; }
    }
}
